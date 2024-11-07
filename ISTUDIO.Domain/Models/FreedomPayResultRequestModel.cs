using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ISTUDIO.Domain.Models;

public class FreedomPayResultRequestModel
{
    [FromForm( Name = "pg_order_id")]
    public string? PgOrderId { get; set; }

    [FromForm(Name = "pg_payment_id")]
    public int? PgPaymentId { get; set; }

    [FromForm(Name = "pg_amount")]
    public string? PgAmount { get; set; }

    [FromForm(Name = "pg_currency")]
    public string? PgCurrency { get; set; }

    [FromForm(Name = "pg_net_amount")]
    public string? PgNetAmount { get; set; }

    [FromForm(Name = "pg_ps_amount")]
    public string? PgPsAmount { get; set; }

    [FromForm(Name = "pg_ps_full_amount")]
    public string? PgPsFullAmount { get; set; }

    [FromForm(Name = "pg_ps_currency")]
    public string? PgPsCurrency { get; set; }

    [FromForm(Name = "pg_description")]
    public string? PgDescription { get; set; }

    [FromForm(Name = "pg_result")]
    public int? PgResult { get; set; }

    [FromForm(Name = "pg_payment_date")]
    public string? PgPaymentDate { get; set; }

    [FromForm(Name = "pg_can_reject")]
    public int? PgCanReject { get; set; }

    [FromForm(Name = "pg_user_phone")]
    public string? PgUserPhone { get; set; }

    [FromForm(Name = "pg_need_phone_notification")]
    public int? PgNeedPhoneNotification { get; set; }

    [FromForm(Name = "pg_user_contact_email")]
    public string? PgUserContactEmail { get; set; }

    [FromForm(Name = "pg_need_email_notification")]
    public int? PgNeedEmailNotification { get; set; }

    [FromForm(Name = "pg_testing_mode")]
    public int? PgTestingMode { get; set; }

    [FromForm(Name = "pg_payment_method")]
    public string? PgPaymentMethod { get; set; }

    [FromForm(Name = "pg_reference")]
    public string? PgReference { get; set; }

    [FromForm(Name = "pg_captured")]
    public int? PgCaptured { get; set; }

    [FromForm(Name = "pg_card_id")]
    public int? PgCardId { get; set; }

    [FromForm(Name = "pg_card_token")]
    public string? PgCardToken { get; set; }

    [FromForm(Name = "pg_card_pan")]
    public string? PgCardPan { get; set; }

    [FromForm(Name = "pg_discount_percent")]
    public string? PgDiscountPercent { get; set; }

    [FromForm(Name = "pg_discount_amount")]
    public string? PgDiscountAmount { get; set; }

    [FromForm(Name = "pg_card_exp")]
    public string? PgCardExp { get; set; }

    [FromForm(Name = "pg_card_owner")]
    public string? PgCardOwner { get; set; }

    [FromForm(Name = "pg_card_brand")]
    public string? PgCardBrand { get; set; }

    [FromForm(Name = "pg_auth_code")]
    public string PgAuthCode { get; set; }

    [FromForm(Name = "pg_salt")]
    public string? PgSalt { get; set; }
    [FromForm(Name = "pg_sig")]
    public string? PgSig { get; set; }
}
