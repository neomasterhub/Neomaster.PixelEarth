namespace Neomaster.PixelEarth.Presentation;

public record ImageInfo
{
  public string FilePath { get; init; }
  public int Width { get; init; }
  public int Height { get; init; }
  public long Size { get; init; }
  public byte[] Bytes { get; init; }
}
