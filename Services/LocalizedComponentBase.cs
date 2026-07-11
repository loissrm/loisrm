using Microsoft.AspNetCore.Components;

namespace Portfolio.Services;

public abstract class LocalizedComponentBase : ComponentBase, IDisposable
{
    [Inject]
    protected LocalizationService Loc { get; set; } = default!;

    protected override void OnInitialized()
    {
        Loc.OnIdiomaCambiado += StateHasChanged;
    }

    public void Dispose()
    {
        Loc.OnIdiomaCambiado -= StateHasChanged;
    }
}