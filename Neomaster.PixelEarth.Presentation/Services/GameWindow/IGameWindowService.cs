namespace Neomaster.PixelEarth.Presentation;

public interface IGameWindowService
{
  void OnLoad();
  void OnRender(RenderEventArgs e);
  void OnUpdate(UpdateEventArgs e);
  void Run();
  void RenderMenu();
}
