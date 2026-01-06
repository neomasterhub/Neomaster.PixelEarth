namespace Neomaster.PixelEarth.Presentation;

public interface IGameWindowService
{
  void OnLoad();
  void OnExit(ExitEventArgs e);
  void OnRender(RenderEventArgs e);
  void OnUpdate(UpdateEventArgs e);
  void UpdateMouseState(MouseStateEventArgs e);
  void Run();
}
