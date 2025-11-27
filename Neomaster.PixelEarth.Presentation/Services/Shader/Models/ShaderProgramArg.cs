namespace Neomaster.PixelEarth.Presentation;

public record ShaderProgramArg<TValue>
{
  public ShaderProgramArg()
  {
  }

  public ShaderProgramArg(string name, TValue value)
  {
    Name = name;
    Value = value;
  }

  public string Name { get; set; }
  public TValue Value { get; set; }
}
