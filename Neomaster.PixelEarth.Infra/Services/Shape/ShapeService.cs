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
  private readonly IShaderService _shaderService;

  private int _colorVaoId;
  private int _colorBaoId;
  private int _textureVaoId;
  private int _textureBaoId;

  public ShapeService(
    RenderSettings renderSettings,
    WindowSettings windowSettings,
    IMouseService mouseService,
    IShaderService shaderService)
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
    _shaderService = shaderService;
  }

  public ShapeState DrawRectangle(
    float x,
    float y,
    float width,
    float height,
    ColorShapeOptions? shapeOptions = null)
  {
    return DrawRectangle(
      new S.Vector2(x, y),
      new S.Vector2(x + width, y + height),
      shapeOptions);
  }

  public ShapeState DrawRectangle(
    S.Vector2 topLeft,
    S.Vector2 bottomRight,
    ColorShapeOptions? shapeOptions = null)
  {
    shapeOptions ??= PresentationConsts.Shape.ColorDefaultOptions;

    var areaMouseState = _mouseService.GetRectangleMouseState(topLeft, bottomRight);
    shapeOptions = shapeOptions.Value.SetHovered(areaMouseState.IsIn);

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
    ColorShapeOptions? shapeOptions = null)
  {
    EnsureShadersInitialized();
    EnsureBuffersInitialized();

    shapeOptions ??= PresentationConsts.Shape.ColorDefaultOptions;

    var vertices = new float[]
    {
        a.X, a.Y,
        b.X, b.Y,
        c.X, c.Y,
    };

    GL.BindBuffer(BufferTarget.ArrayBuffer, _colorBaoId);
    GL.BufferData(
      BufferTarget.ArrayBuffer,
      vertices.Length * sizeof(float),
      vertices,
      BufferUsageHint.DynamicDraw);

    shapeOptions.Value.UseWithProgram();
    _positionProjection.BindMatrix4(_shaderService.ColorShaderProgramInfo.Id);

    shapeOptions.Value.CullFaces.Apply();
    GL.FrontFace(_renderSettings.WindingOrder.ToGlType());

    GL.BindVertexArray(_colorVaoId);
    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

    GL.BindVertexArray(0);
    GL.UseProgram(0);
  }

  public void DrawTextureTriangle(
    S.Vector2 a,
    S.Vector2 b,
    S.Vector2 c,
    S.Vector2 uvA,
    S.Vector2 uvB,
    S.Vector2 uvC,
    TextureShapeOptions? shapeOptions = null)
  {
    EnsureShadersInitialized();
    EnsureBuffersInitialized();

    shapeOptions ??= PresentationConsts.Shape.TextureDefaultOptions;

    var vertices = new float[]
    {
        a.X, a.Y, uvA.X, uvA.Y,
        b.X, b.Y, uvB.X, uvB.Y,
        c.X, c.Y, uvC.X, uvC.Y,
    };

    GL.BindBuffer(BufferTarget.ArrayBuffer, _textureBaoId);
    GL.BufferData(
      BufferTarget.ArrayBuffer,
      vertices.Length * sizeof(float),
      vertices,
      BufferUsageHint.DynamicDraw);

    GL.ActiveTexture(TextureUnit.Texture0);
    GL.BindTexture(TextureTarget.Texture2D, 1); // TODO: get from args

    shapeOptions.Value.UseWithProgram();
    _positionProjection.BindMatrix4(_shaderService.TextureShaderProgramInfo.Id);

    GL.BindVertexArray(_textureVaoId);
    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

    GL.BindVertexArray(0);
    GL.UseProgram(0);
  }

  public void InitializeBuffers()
  {
    _colorVaoId = GL.GenVertexArray();
    _colorBaoId = GL.GenBuffer();
    GL.BindVertexArray(_colorVaoId);
    GL.BindBuffer(BufferTarget.ArrayBuffer, _colorBaoId);
    GL.EnableVertexAttribArray(0);
    GL.VertexAttribPointer(
      index: 0,
      size: 2,
      type: VertexAttribPointerType.Float,
      normalized: false,
      stride: 2 * sizeof(float),
      offset: 0);
    GL.BindVertexArray(0);

    _textureVaoId = GL.GenVertexArray();
    _textureBaoId = GL.GenBuffer();
    GL.BindVertexArray(_textureVaoId);
    GL.BindBuffer(BufferTarget.ArrayBuffer, _textureBaoId);
    GL.EnableVertexAttribArray(0);
    GL.VertexAttribPointer(
        index: 0,
        size: 2,
        type: VertexAttribPointerType.Float,
        normalized: false,
        stride: 4 * sizeof(float),
        offset: 0);
    GL.EnableVertexAttribArray(1);
    GL.VertexAttribPointer(
        index: 1,
        size: 2,
        type: VertexAttribPointerType.Float,
        normalized: false,
        stride: 4 * sizeof(float),
        offset: 2 * sizeof(float));
    GL.BindVertexArray(0);
  }

  [Conditional("DEBUG")]
  private void EnsureBuffersInitialized()
  {
    if (_colorVaoId == 0
      || _colorBaoId == 0
      || _textureVaoId == 0
      || _textureBaoId == 0)
    {
      throw new InvalidOperationException(
        "Buffers not initialized. Call InitializeBuffers() before drawing shapes.");
    }
  }

  [Conditional("DEBUG")]
  private void EnsureShadersInitialized()
  {
    if (!_shaderService.ShadersInitialized())
    {
      throw new InvalidOperationException(
        "Shaders not initialized. Call InitializeShaders() before drawing shapes.");
    }
  }
}
