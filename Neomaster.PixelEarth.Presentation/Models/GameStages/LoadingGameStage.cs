using Microsoft.Extensions.DependencyInjection;
using Neomaster.PixelEarth.App;
using static Neomaster.PixelEarth.App.AppConsts;

namespace Neomaster.PixelEarth.Presentation;

public sealed class LoadingGameStage : BaseGameStage
{
  private readonly Textures _textures;
  private readonly IMainMenuService _mainMenuService;
  private readonly ITextureService _textureService;
  private readonly IUIService _uiService;

  private TextureButton _buttonPlay;
  private TextureButton _buttonExit;
  private bool _requiresStart = true;

  public LoadingGameStage(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    _textures = serviceProvider.GetRequiredService<Textures>();
    _mainMenuService = serviceProvider.GetRequiredService<IMainMenuService>();
    _textureService = serviceProvider.GetRequiredService<ITextureService>();
    _uiService = serviceProvider.GetRequiredService<IUIService>();
  }

  public TextureButton ButtonPlay => _buttonPlay;
  public TextureButton ButtonExit => _buttonExit;

  protected override bool RequiresStart()
  {
    return _requiresStart;
  }

  protected override void OnUpdate(UpdateEventArgs? e = null)
  {
    _textureService.Load(_textures[TextureGroupName.Test]);

    _buttonPlay = _uiService.CreateTextureButton(100f, 25f, 500f, 500f);
    _buttonExit = _uiService.CreateTextureButton(190f, 70f, 500f, 500f);

    _mainMenuService.Create(
      [
        new MainMenuItemDef(() => { }),
        new MainMenuItemDef(() => { }),
      ]);

    _requiresStart = false;
  }
}
