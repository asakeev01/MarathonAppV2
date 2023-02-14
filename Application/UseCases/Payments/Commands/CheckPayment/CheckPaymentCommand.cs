﻿using System;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using Domain.Common.Contracts;
using Domain.Common.Helpers;
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
        Console.WriteLine("Entered");
        var paymentRequest = cmd.PaymentDto.Adapt<CheckPaymentDto>();
        var application = await _unit.ApplicationRepository.GetFirstOrDefaultAsync(x => x.Id.ToString() == cmd.PaymentDto.pg_order_id);
        var xml = "";
        var response = new PaymentResponse();
        Console.WriteLine(application.Id);
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
        using (var stringwriter = new Utf8StringWriter())
        {
            var serializer = new XmlSerializer(response.GetType());
            serializer.Serialize(stringwriter, response);
            xml = stringwriter.ToString();
        }
        //using (var memStm = new MemoryStream())
        //using (var xw = XmlWriter.Create(memStm))
        //{
        //    var serializer = new XmlSerializer(typeof(PaymentResponse));
        //    serializer.Serialize(xw, response);
        //    xml = memStm.ToString();
        //}
        Console.WriteLine(xml);
        return xml;       
    }
}

