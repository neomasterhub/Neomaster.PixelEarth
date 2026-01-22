namespace Neomaster.PixelEarth.App;

public enum Blending
{
  Undefined,

  /// <summary>
  /// src * alpha + dst * (1 - alpha)
  /// </summary>
  Alpha,

  /// <summary>
  /// src * 1 + dst * 0
  /// </summary>
  Replace,
}
