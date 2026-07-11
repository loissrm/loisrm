using System.Text.Json.Serialization;

namespace TuPortfolio.Models;

public class Proyecto
{
    [JsonPropertyName("titulo")]
    public string Titulo { get; set; } = string.Empty;

    [JsonPropertyName("descripcion")]
    public string Descripcion { get; set; } = string.Empty;

    [JsonPropertyName("anio")]
    public string Anio { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = "#";

    [JsonPropertyName("imagen")]
    public string Imagen { get; set; } = string.Empty;

    [JsonPropertyName("tecnologias")]
    public List<TechTag> Tecnologias { get; set; } = new();
}

public class TechTag
{
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("icono")]
    public string Icono { get; set; } = string.Empty; // ej. "ti-brand-csharp"

    [JsonPropertyName("colorFondo")]
    public string ColorFondo { get; set; } = "#EEEDFE";

    [JsonPropertyName("colorTexto")]
    public string ColorTexto { get; set; } = "#3C3489";
}
