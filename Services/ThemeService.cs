using Microsoft.JSInterop;

namespace Portfolio.Services;

public class ThemeService
{
    private readonly IJSRuntime _js;
    private const string THEME_KEY = "tema_preferido";

    public bool EsOscuro { get; private set; } = false;
    public event Action? OnTemaCambiado;

    public ThemeService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InicializarAsync()
    {
        try
        {
            var guardado = await _js.InvokeAsync<string?>("localStorage.getItem", THEME_KEY);
            if (!string.IsNullOrWhiteSpace(guardado))
            {
                EsOscuro = guardado == "Oscuro";
            }
        }
        catch { }
    }

    public async Task CambiarTemaAsync(bool isDark)
    {
        EsOscuro = isDark;
        try
        {
            await _js.InvokeVoidAsync("localStorage.setItem", THEME_KEY, isDark ? "Oscuro" : "Luz");
        }
        catch { }

        OnTemaCambiado?.Invoke();
    }

    public string ObtenerClaseCss() => EsOscuro ? "dark-mode" : string.Empty;
}