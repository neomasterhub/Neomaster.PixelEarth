namespace Neomaster.PixelEarth.Infra;

public class ShaderException : Exception
{
  public ShaderException(
    string message,
    string log,
    IDictionary<string, string> details = null)
    : base(message)
  {
    Log = log;

    if (details == null)
    {
      return;
    }

    foreach (var (key, value) in details)
    {
      Data.Add(key, value);
    }
  }

  public string Log { get; }
}
