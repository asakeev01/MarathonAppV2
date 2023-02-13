using System;
using System.Net;
using System.Xml.Serialization;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using Domain.Services.Models;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

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
    private readonly ILogger<CheckPaymentHandler> _logger;

    public CheckPaymentHandler(ILogger<CheckPaymentHandler> logger, IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService, IPaymentService paymentService)
    {
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
        _paymentService = paymentService;
        _logger = logger;
    }

    public async Task<string> Handle(CheckPaymentCommand cmd, CancellationToken cancellationToken)
    {
        var paymentRequest = cmd.PaymentDto.Adapt<CheckPaymentDto>();
        var application = await _unit.ApplicationRepository.GetFirstOrDefaultAsync(x => x.Id.ToString() == cmd.PaymentDto.pg_order_id);
        var xml = "";
        var response = new PaymentResponse();
        if (application != null)
        {
            response.pg_description = "Payment may continue";
            response.pg_salt = "Random";
            response.pg_status = "ok";
            Console.WriteLine("Permited");
            _logger.LogInformation("Permitted");
        }

        else
        {
            response.pg_description = "Payment may not continue";
            response.pg_salt = "Random";
            response.pg_status = "rejected";
            Console.WriteLine("Stopped");
            _logger.LogInformation("Stopped");
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

