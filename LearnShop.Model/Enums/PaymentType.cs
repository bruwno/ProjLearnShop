using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LearnShop.Model.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentType
{
    [EnumMember(Value = "pix")]
    [Description("Pix")]
    Pix,
    
    [EnumMember(Value = "boleto")]
    [Description("Boleto")]
    Boleto,
    
    [EnumMember(Value = "cartao")]
    [Description("Cartão de Crédito")]
    Cartao
}