namespace Portfolio.Models;

/// <summary>Un punto en la línea del tiempo. Se usa tanto para Experiencia como para Educación,
/// así solo hace falta un componente Timeline para ambas secciones.</summary>
public class TimelineItem
{
    public string Titulo { get; set; } = string.Empty;      // puesto o título del grado
    public string Subtitulo { get; set; } = string.Empty;   // empresa o institución
    public string FechaInicio { get; set; } = string.Empty;
    public string FechaFin { get; set; } = string.Empty;    // "Actualidad" si sigue en curso
    public string Descripcion { get; set; } = string.Empty;
}

/// <summary>Curso, certificado o formación puntual sin rango de fechas.</summary>
public class EducacionComplementariaItem
{
    public string Titulo { get; set; } = string.Empty;
    public string Entidad { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;    // opcional, enlace al certificado
    public string Imagen { get; set; } = string.Empty; // opcional, imagen del certificado/logo
}

public class HabilidadCategoria
{
    public string Categoria { get; set; } = string.Empty;
    public string ColorName { get; set; } = "kw"; // kw | str | fn | type | comment
    public List<Habilidad> Habilidades { get; set; } = new();
}

public class Habilidad
{
    public string Nombre { get; set; } = string.Empty;
    public int Nivel { get; set; } = 50; // 0-100, para la barra de progreso
}