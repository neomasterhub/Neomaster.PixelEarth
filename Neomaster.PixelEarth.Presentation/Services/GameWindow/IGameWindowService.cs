namespace Neomaster.PixelEarth.Presentation;

public interface IGameWindowService
{
  void OnLoad();
  void OnRender(RenderEventArgs e);
  void OnUpdate(UpdateEventArgs e);
  void OnExit(ExitEventArgs e);
  void UpdateMouseState(MouseStateEventArgs e);
  void Run();
  void RenderMenu();
}
