using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;

namespace Portfolio.Services;

public class LocalizationService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;

    // Diccionario global (Menú, Footer, Home)
    private Dictionary<string, string> _traducciones = new();

    // Diccionario aislado solo para el Proyecto activo
    private Dictionary<string, string> _traduccionesProyecto = new();
    private string? _proyectoActual = null;

    public event Action? OnIdiomaCambiado;

    public string IdiomaActual { get; private set; } = "gl";

    private const string STORAGE_KEY = "idioma_preferido";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public LocalizationService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task InicializarAsync()
    {
        try
        {
            var idiomaGuardado = await _js.InvokeAsync<string?>("localStorage.getItem", STORAGE_KEY);
            if (!string.IsNullOrWhiteSpace(idiomaGuardado))
            {
                IdiomaActual = idiomaGuardado;
            }
        }
        catch { }

        await CargarDiccionarioTraduccionesAsync();
    }

    public async Task CambiarIdiomaAsync(string nuevoIdioma)
    {
        if (string.IsNullOrWhiteSpace(nuevoIdioma)) return;

        IdiomaActual = nuevoIdioma;

        try
        {
            await _js.InvokeVoidAsync("localStorage.setItem", STORAGE_KEY, nuevoIdioma);
        }
        catch { }

        // 1. Recargamos traducciones globales
        await CargarDiccionarioTraduccionesAsync();

        // 2. Si hay un proyecto abierto, recargamos sus traducciones aisladas
        if (!string.IsNullOrEmpty(_proyectoActual))
        {
            await CargarTraduccionesProyectoAsync(_proyectoActual);
        }

        OnIdiomaCambiado?.Invoke();
    }

    private async Task CargarDiccionarioTraduccionesAsync()
    {
        try
        {
            var ruta = $"i18n/{IdiomaActual}.json?v={DateTime.UtcNow.Ticks}";
            var resultado = await _http.GetFromJsonAsync<Dictionary<string, string>>(ruta, JsonOptions);
            _traducciones = resultado ?? new Dictionary<string, string>();
        }
        catch
        {
            _traducciones = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Carga el JSON de un proyecto específico en un diccionario totalmente independiente.
    /// </summary>
    public async Task CargarTraduccionesProyectoAsync(string nombreProyecto)
    {
        _proyectoActual = nombreProyecto;

        try
        {
            var ruta = $"data/Proyectos/{nombreProyecto}/{IdiomaActual}.json?v={DateTime.UtcNow.Ticks}";
            var resultado = await _http.GetFromJsonAsync<Dictionary<string, string>>(ruta, JsonOptions);
            _traduccionesProyecto = resultado ?? new Dictionary<string, string>();
        }
        catch
        {
            _traduccionesProyecto = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Limpia las traducciones del proyecto al salir de la página (Opcional)
    /// </summary>
    public void LimpiarProyectoActual()
    {
        _proyectoActual = null;
        _traduccionesProyecto.Clear();
    }

    // Traducción Global (Menú, Home, Footer...)
    public string T(string clave)
    {
        return _traducciones.TryGetValue(clave, out var valor) ? valor : clave;
    }

    // Traducción de Proyecto (Aislada e independiente)
    public string TP(string clave)
    {
        return _traduccionesProyecto.TryGetValue(clave, out var valor) ? valor : clave;
    }

    public async Task<T?> ObtenerDatosAsync<T>(string nombreArchivo)
    {
        try
        {
            var ruta = $"data/{nombreArchivo}.{IdiomaActual}.json?v={DateTime.UtcNow.Ticks}";
            return await _http.GetFromJsonAsync<T>(ruta, JsonOptions);
        }
        catch
        {
            return default;
        }
    }
}