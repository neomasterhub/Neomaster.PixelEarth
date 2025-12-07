using System;

namespace Neomaster.PixelEarth.Utils;

public static class MathX
{
  public static int FittableCount(
    float maxLength,
    float elementLength,
    float gap)
  {
    // L = N * (l + g) - g
    // N = floor((L + g) / (l + g))
    return (int)MathF.Floor((maxLength + gap) / (elementLength + gap));
  }

  public static float FittableLength(
    int fittableCount,
    float elementLength,
    float gap)
  {
    return (fittableCount * (elementLength + gap)) - gap;
  }
}
