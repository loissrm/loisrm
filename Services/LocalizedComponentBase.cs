using Microsoft.AspNetCore.Components;

namespace Portfolio.Services;

/// <summary>
/// Cualquier componente que muestre texto traducido con Loc.T(...) debe heredar de esta clase
/// (con @inherits LocalizedComponentBase) para que se re-renderice automáticamente
/// cuando el usuario cambia de idioma.
/// </summary>
public abstract class LocalizedComponentBase : ComponentBase, IDisposable
{
    [Inject]
    protected LocalizationService Loc { get; set; } = default!;

    protected override void OnInitialized()
    {
        Loc.OnIdiomaCambiado += StateHasChanged;
    }

    public virtual void Dispose()
    {
        Loc.OnIdiomaCambiado -= StateHasChanged;
    }
}