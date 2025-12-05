namespace Neomaster.PixelEarth.Infra;

public class IntIdGenerator : IIdGenerator<int>
{
  private static int _current = 0;

  public int Next()
  {
    return Interlocked.Increment(ref _current);
  }
}
