namespace Neomaster.PixelEarth.App;

public interface IGameWindowService
{
  void Run();
  void Exit();
  void OnLoad();
  void OnExit(ExitEventArgs e);
  void OnRender(RenderEventArgs e);
  void OnUpdate(UpdateEventArgs e);
  void UpdateMouseState(MouseStateEventArgs e);
  bool IsKeyUp(ConsoleKey key);
  bool IsKeyDown(ConsoleKey key);
}
