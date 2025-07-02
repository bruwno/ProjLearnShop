using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LearnShop.Model.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    [EnumMember(Value = "admin")]
    [Description("Administrador")]
    Admin,
    
    [EnumMember(Value = "cliente")]
    [Description("Cliente")]
    Cliente
}