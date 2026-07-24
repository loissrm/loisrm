namespace Portfolio.Models;
using System.Text.Json.Serialization;

public class TimelineItem
{
    public string Titulo { get; set; } = string.Empty;      // titulo
    public string Subtitulo { get; set; } = string.Empty;   // empresa o institución
    public string FechaInicio { get; set; } = string.Empty;
    public string FechaFin { get; set; } = string.Empty;    // "Actualidad" si sigue en curso
    public string Descripcion { get; set; } = string.Empty;
}

public class EducacionComplementariaItem
{
    public string Titulo { get; set; } = string.Empty;
    public string Entidad { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;    // opcional, enlace al certificado
    public string Imagen { get; set; } = string.Empty; // opcional, imagen del certificado/logo
    public List<string> Aptitudes { get; set; } = new(); // habilidades/competencias adquiridas en el curso
}

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

    [JsonPropertyName("colorHex")]
    public string colorHex { get; set; } = string.Empty; // el <svg>...</svg> completo, pegado directamente
}

