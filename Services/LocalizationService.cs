using System.Collections.Generic;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace Portfolio.Services;

public class LocalizationService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private Dictionary<string, string> _traducciones = new();

    public string IdiomaActual { get; private set; } = "gl";

    /// <summary>Se dispara cuando cambia el idioma, para que los componentes se re-rendericen.</summary>
    public event Action? OnIdiomaCambiado;

    public LocalizationService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    /// <summary>Llamar una vez al arrancar la app, en MainLayout.</summary>
    public async Task InicializarAsync()
    {
        var guardado = await _js.InvokeAsync<string?>("localStorage.getItem", "idioma");
        var idioma = guardado ?? "es";
        await CambiarIdiomaAsync(idioma, guardarEnStorage: false);
    }

    public async Task CambiarIdiomaAsync(string idioma, bool guardarEnStorage = true)
    {
        _traducciones = await _http.GetFromJsonAsync<Dictionary<string, string>>($"i18n/{idioma}.json")
                         ?? new Dictionary<string, string>();
        IdiomaActual = idioma;

        if (guardarEnStorage)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "idioma", idioma);
        }

        OnIdiomaCambiado?.Invoke();
    }

    /// <summary>Traduce una clave. Si no existe, devuelve la propia clave (para detectar faltantes).</summary>
    public string T(string clave)
    {
        return _traducciones.TryGetValue(clave, out var valor) ? valor : clave;
    }
}
