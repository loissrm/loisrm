namespace Portfolio.Models;

public class TimelineItem
{
    public string Titulo { get; set; } = string.Empty;      // titutlo
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
}

