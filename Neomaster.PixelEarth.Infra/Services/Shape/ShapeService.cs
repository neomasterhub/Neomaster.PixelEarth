using Neomaster.PixelEarth.Infra.Extensions;
using Neomaster.PixelEarth.Presentation;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using S = System.Numerics;

namespace Neomaster.PixelEarth.Infra;

public class ShapeService : IShapeService
{
  private readonly RenderSettings _renderSettings;
  private readonly ShaderProgramArg<Matrix4> _positionProjection;

  private int _vaoId;
  private int _baoId;

  public ShapeService(
    RenderSettings renderSettings,
    WindowSettings windowSettings)
  {
    _renderSettings = renderSettings;

    _positionProjection = new ShaderProgramArg<Matrix4>(
      "uProjection",
      Matrix4.CreateOrthographicOffCenter(
        0,
        windowSettings.Width,
        windowSettings.Height,
        0,
        -1f,
        1f));
  }

  public void DrawRectangle(
    float x,
    float y,
    float width,
    float height,
    ShapeOptions shapeOptions = null)
  {
    DrawRectangle(
      new S.Vector2(x, y),
      new S.Vector2(x + width, y + height),
      shapeOptions);
  }

  public void DrawRectangle(
    S.Vector2 topLeft,
    S.Vector2 bottomRight,
    ShapeOptions shapeOptions = null)
  {
    var bottomLeft = new S.Vector2(topLeft.X, bottomRight.Y);
    var topRight = new S.Vector2(bottomRight.X, topLeft.Y);
    DrawTriangle(topLeft, bottomLeft, bottomRight, shapeOptions);
    DrawTriangle(topLeft, bottomRight, topRight, shapeOptions);
  }

  public void DrawTriangle(
    S.Vector2 a,
    S.Vector2 b,
    S.Vector2 c,
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
    _positionProjection.BindMatrix4(shapeOptions.ShaderProgramId);

    shapeOptions.CullFaces.Apply();
    GL.FrontFace(_renderSettings.WindingOrder.ToGlType());

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
