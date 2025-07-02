using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LearnShop.Model.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
    [EnumMember(Value = "algoritmos")]
    [Description("Algoritmos")]
    Algoritmos,

    [EnumMember(Value = "bancoDeDados")]
    [Description("Banco de Dados")]
    BancoDeDados,

    [EnumMember(Value = "desenvolvimentoWeb")]
    [Description("Desenvolvimento Web")]
    DesenvolvimentoWeb,

    [EnumMember(Value = "desenvolvimentoMobile")]
    [Description("Desenvolvimento Mobile")]
    DesenvolvimentoMobile,

    [EnumMember(Value = "engenhariaDeSoftware")]
    [Description("Engenharia de Software")]
    EngenhariaDeSoftware,
}