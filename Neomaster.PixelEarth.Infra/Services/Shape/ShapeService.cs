using System.Numerics;
using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;

namespace Neomaster.PixelEarth.Infra;

public class ShapeService : IShapeService
{
  private int _vaoId;
  private int _baoId;

  public void DrawQuad(
    float x,
    float y,
    float width,
    float height,
    ShapeOptions shapeOptions = null)
  {
    DrawQuad(
      new Vector2(x, y),
      new Vector2(x + width, y - height),
      shapeOptions);
  }

  public void DrawQuad(
    Vector2 topLeft,
    Vector2 bottomRight,
    ShapeOptions shapeOptions = null)
  {
    var bottomLeft = new Vector2(topLeft.X, bottomRight.Y);
    var topRight = new Vector2(bottomRight.X, topLeft.Y);
    DrawTriangle(topLeft, bottomLeft, bottomRight, shapeOptions);
    DrawTriangle(topLeft, bottomRight, topRight, shapeOptions);
  }

  public void DrawTriangle(
    Vector2 a,
    Vector2 b,
    Vector2 c,
    ShapeOptions shapeOptions = null)
  {
    shapeOptions ??= PresentationConsts.Shape.DefaultOptions;

    var vertices = new float[]
    {
        a.X, a.Y,
        b.X, b.Y,
        c.X, c.Y,
    };

    GL.BindBuffer(BufferTarget.ArrayBuffer, _baoId);
    GL.BufferData(
      BufferTarget.ArrayBuffer,
      vertices.Length * sizeof(float),
      vertices,
      BufferUsageHint.DynamicDraw);

    shapeOptions.UseWithProgram();

    GL.BindVertexArray(_vaoId);
    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

    GL.BindVertexArray(0);
    GL.UseProgram(0);
  }

  public void InitializeBuffers()
  {
    _vaoId = GL.GenVertexArray();
    _baoId = GL.GenBuffer();

    GL.BindVertexArray(_vaoId);
    GL.BindBuffer(BufferTarget.ArrayBuffer, _baoId);

    GL.EnableVertexAttribArray(0);
    GL.VertexAttribPointer(
      index: 0,
      size: 2,
      type: VertexAttribPointerType.Float,
      normalized: false,
      stride: 2 * sizeof(float),
      offset: 0);
    GL.BindVertexArray(0);
  }
}
