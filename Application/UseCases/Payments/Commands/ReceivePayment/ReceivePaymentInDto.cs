using System;
using Core.Common.Bases;
using Domain.Services.Models;

namespace Core.UseCases.Payments.Commands.ReceivePayment;

public record ReceivePaymentInDto : BaseDto<ReceivePaymentInDto, ReceivePaymentDto>
{
    //public string pg_order_id { get; set; }
    //public int pg_payment_id { get; set; }
    //public string pg_amount { get; set; }
    //public string? pg_currency { get; set; }
    //public string? pg_need_email_notification { get; set; }
    //public string? pg_need_phone_notification { get; set; }
    //public string? pg_net_amount { get; set; }
    //public string? pg_ps_amount { get; set; }
    //public string? pg_ps_full_amount { get; set; }
    //public string? pg_ps_currency { get; set; }
    //public string pg_description { get; set; }
    //public int pg_result { get; set; }
    //public string pg_payment_date { get; set; }
    //public int pg_can_reject { get; set; }
    //public string pg_user_phone { get; set; }
    //public string pg_user_contact_email { get; set; }
    //public string? pg_testing_mode { get; set; }
    //public int? pg_captured { get; set; }
    //public string? pg_card_id { get; set; }
    //public string? pg_card_pan { get; set; }
    //public string pg_salt { get; set; }
    //public string pg_sig { get; set; }
    //public string pg_payment_method { get; set; }
    //public string? pg_card_exp { get; set; }
    //public string? pg_card_owner { get; set; }
    //public string? pg_card_brand { get; set; }

    public string? pg_order_id { get; set; }
    public int? pg_payment_id { get; set; }
    public string? pg_amount { get; set; }
    public string? pg_currency { get; set; }
    public string? pg_need_email_notification { get; set; }
    public string? pg_need_phone_notification { get; set; }
    public string? pg_net_amount { get; set; }
    public string? pg_ps_amount { get; set; }
    public string? pg_ps_full_amount { get; set; }
    public string? pg_ps_currency { get; set; }
    public string? pg_description { get; set; }
    public int? pg_result { get; set; }
    public string? pg_payment_date { get; set; }
    public int? pg_can_reject { get; set; }
    public string? pg_user_phone { get; set; }
    public string? pg_user_contact_email { get; set; }
    public string? pg_testing_mode { get; set; }
    public int? pg_captured { get; set; }
    public string? pg_card_id { get; set; }
    public string? pg_card_pan { get; set; }
    public string? pg_salt { get; set; }
    public string? pg_sig { get; set; }
    public string? pg_payment_method { get; set; }
    public string? pg_card_exp { get; set; }
    public string? pg_card_owner { get; set; }
    public string? pg_card_brand { get; set; }
}

