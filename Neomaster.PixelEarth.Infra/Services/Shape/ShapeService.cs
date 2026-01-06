using System.Diagnostics;
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
  private readonly IMouseService _mouseService;

  private int _vaoId;
  private int _baoId;
  private int _textureVaoId;
  private int _textureBaoId;

  public ShapeService(
    RenderSettings renderSettings,
    WindowSettings windowSettings,
    IMouseService mouseService)
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

    _mouseService = mouseService;
  }

  public ShapeState DrawRectangle(
    float x,
    float y,
    float width,
    float height,
    ShapeOptions? shapeOptions = null)
  {
    return DrawRectangle(
      new S.Vector2(x, y),
      new S.Vector2(x + width, y + height),
      shapeOptions);
  }

  public ShapeState DrawRectangle(
    S.Vector2 topLeft,
    S.Vector2 bottomRight,
    ShapeOptions? shapeOptions = null)
  {
    shapeOptions ??= PresentationConsts.Shape.DefaultOptions;

    var areaMouseState = _mouseService.GetRectangleMouseState(topLeft, bottomRight);
    shapeOptions = shapeOptions.Value.IsHovered(areaMouseState.IsIn);

    var bottomLeft = new S.Vector2(topLeft.X, bottomRight.Y);
    var topRight = new S.Vector2(bottomRight.X, topLeft.Y);
    DrawTriangle(topLeft, bottomLeft, bottomRight, shapeOptions);
    DrawTriangle(topLeft, bottomRight, topRight, shapeOptions);

    return new(areaMouseState.IsIn, areaMouseState.LeftPressed);
  }

  public void DrawTriangle(
    S.Vector2 a,
    S.Vector2 b,
    S.Vector2 c,
    ShapeOptions? shapeOptions = null)
  {
    EnsureBuffersInitialized();

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

    shapeOptions.Value.UseWithProgram();
    _positionProjection.BindMatrix4(shapeOptions.Value.FillShaderProgramId);

    shapeOptions.Value.CullFaces.Apply();
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

  [Conditional("DEBUG")]
  private void EnsureBuffersInitialized()
  {
    if (_vaoId == 0
      || _baoId == 0
      || _textureVaoId == 0
      || _textureBaoId == 0)
    {
      throw new InvalidOperationException(
        "Buffers not initialized. Call InitializeBuffers() before drawing shapes.");
    }
  }
}
