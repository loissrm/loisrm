using Microsoft.AspNetCore.Components;
using Portfolio.Services;

namespace Portfolio.Services;

public abstract class LocalizedComponentBase : ComponentBase, IDisposable
{
    [Inject] 
    protected LocalizationService Loc { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Loc.OnIdiomaCambiado += ManejarCambioDeIdiomaInternal;
    }

    private async void ManejarCambioDeIdiomaInternal()
    {
        // 1. Vuelve a cargar los datos en el idioma seleccionado
        await CargarDatosAsync();
        
        // 2. Refresca la vista en pantalla
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await CargarDatosAsync();
    }

    /// <summary>
    /// Sobrescribe este método en cualquier componente (.razor) que necesite cargar JSONs.
    /// </summary>
    protected virtual Task CargarDatosAsync()
    {
        return Task.CompletedTask;
    }

    public virtual void Dispose()
    {
        Loc.OnIdiomaCambiado -= ManejarCambioDeIdiomaInternal;
    }
}