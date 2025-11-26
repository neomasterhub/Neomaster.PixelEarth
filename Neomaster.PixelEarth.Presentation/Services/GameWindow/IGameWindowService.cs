namespace Neomaster.PixelEarth.Presentation;

public interface IGameWindowService
{
  void OnRender(RenderEventArgs e);
  void OnUpdate(UpdateEventArgs e);
  void Run();
}
