using System.Numerics;

namespace Neomaster.PixelEarth.Infra;

public class ColorService
{
  public static Vector4 HexToVector4(string hex)
  {
    if (hex.Length != 8)
    {
      throw new ArgumentException("Hex color must be 8 digits (RRGGBBAA).");
    }

    return new Vector4(
      Convert.ToByte(hex[0..2], 16) / 255f,
      Convert.ToByte(hex[2..4], 16) / 255f,
      Convert.ToByte(hex[4..6], 16) / 255f,
      Convert.ToByte(hex[6..8], 16) / 255f);
  }
}
