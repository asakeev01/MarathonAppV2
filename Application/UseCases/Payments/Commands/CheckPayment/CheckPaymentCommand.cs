using System;
using System.Net;
using System.Xml.Serialization;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using Domain.Services.Models;
using Mapster;
using MediatR;

namespace Core.UseCases.Payments.Commands.CheckPayment;

public class CheckPaymentCommand : IRequest<string>
{
    public CheckPaymentInDto PaymentDto { get; set; }
}

public class CheckPaymentHandler : IRequestHandler<CheckPaymentCommand, string>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;
    private readonly IEmailService _emailService;
    private readonly IPaymentService _paymentService;

    public CheckPaymentHandler(IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService, IPaymentService paymentService)
    {
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
        _paymentService = paymentService;
    }

    public async Task<string> Handle(CheckPaymentCommand cmd, CancellationToken cancellationToken)
    {
        var paymentRequest = cmd.PaymentDto.Adapt<CheckPaymentDto>();
        var application = await _unit.ApplicationRepository.FirstAsync(x => x.Id.ToString() == cmd.PaymentDto.pg_order_id);
        var xml = "";
        var response = new PaymentResponse();
        if (application != null)
        {
            response.pg_description = "Payment may not continue";
            response.pg_salt = "Random";
            response.pg_status = "rejected";
        }

        else
        {
            response.pg_description = "Payment may continue";
            response.pg_salt = "Random";
            response.pg_status = "ok";
        }
        response = _paymentService.CreateResponseSignature(response);
        using (var stringwriter = new System.IO.StringWriter())
        {
            var serializer = new XmlSerializer(response.GetType());
            serializer.Serialize(stringwriter, response);
            xml = stringwriter.ToString();
        }
        return xml;       
    }
}

