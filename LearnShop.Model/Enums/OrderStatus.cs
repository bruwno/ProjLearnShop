using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LearnShop.Model.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    [EnumMember(Value = "concluido")]
    [Description("Concluído")]
    Concluido,
    
    [EnumMember(Value = "pendente")]
    [Description("Pendente")]
    Pendente,
}