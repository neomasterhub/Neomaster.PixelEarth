namespace Neomaster.PixelEarth.Presentation;

public struct ShaderProgramArg<TValue>
  where TValue : struct
{
  public string Name;
  public TValue Value;

  public ShaderProgramArg(string name, TValue value)
  {
    Name = name;
    Value = value;
  }
}
