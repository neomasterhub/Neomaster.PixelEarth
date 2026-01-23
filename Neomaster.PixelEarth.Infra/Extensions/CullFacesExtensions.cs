using Neomaster.PixelEarth.App;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra.Extensions;

public static class CullFacesExtensions
{
  private static CullFaces _cullFaces = CullFaces.Undefined;

  public static void Apply(this CullFaces cullFaces)
  {
    if (_cullFaces == cullFaces)
    {
      return;
    }

    _cullFaces = cullFaces;

    switch (cullFaces)
    {
      case CullFaces.None:
        GL.Disable(EnableCap.CullFace);
        break;
      case CullFaces.Front:
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(TriangleFace.Front);
        break;
      case CullFaces.Back:
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(TriangleFace.Back);
        break;
      case CullFaces.FrontAndBack:
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(TriangleFace.FrontAndBack);
        break;
      default: throw cullFaces.ArgumentOutOfRangeException();
    }
  }
}
