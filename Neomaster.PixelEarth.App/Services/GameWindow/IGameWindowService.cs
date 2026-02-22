namespace Neomaster.PixelEarth.App;

public interface IGameWindowService
{
  bool IsKeyDown(ConsoleKey key);
  void OnLoad();
  void OnExit(ExitEventArgs e);
  void OnRender(RenderEventArgs e);
  void OnUpdate(UpdateEventArgs e);
  void UpdateMouseState(MouseStateEventArgs e);
  void Run();
  void Exit();
}
