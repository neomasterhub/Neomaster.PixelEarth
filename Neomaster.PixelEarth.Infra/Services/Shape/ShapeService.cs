using System.Diagnostics;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Infra.Extensions;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using S = System.Numerics;

namespace Neomaster.PixelEarth.Infra;

public class ShapeService : IShapeService
{
  private readonly RenderSettings _renderSettings;
  private readonly ColorShapeOptions _colorShapeOptions;
  private readonly TextureShapeOptions _textureShapeOptions;
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
    ColorShapeOptions colorShapeOptions,
    TextureShapeOptions textureShapeOptions,
    IMouseService mouseService,
    IShaderService shaderService)
  {
    _renderSettings = renderSettings;
    _colorShapeOptions = colorShapeOptions;
    _textureShapeOptions = textureShapeOptions;

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

  public ShapeState DrawColorRectangle(
    float x,
    float y,
    float width,
    float height,
    ColorShapeOptions? shapeOptions = null)
  {
    return DrawColorRectangle(
      new S.Vector2(x, y),
      new S.Vector2(x + width, y + height),
      shapeOptions);
  }

  public ShapeState DrawColorRectangle(
    S.Vector2 topLeft,
    S.Vector2 bottomRight,
    ColorShapeOptions? shapeOptions = null)
  {
    shapeOptions ??= _colorShapeOptions;

    var areaMouseState = _mouseService.GetRectangleMouseState(topLeft, bottomRight);
    shapeOptions = shapeOptions.Value.SetHovered(areaMouseState.IsIn);

    var bottomLeft = new S.Vector2(topLeft.X, bottomRight.Y);
    var topRight = new S.Vector2(bottomRight.X, topLeft.Y);
    DrawColorTriangle(topLeft, bottomLeft, bottomRight, shapeOptions);
    DrawColorTriangle(topLeft, bottomRight, topRight, shapeOptions);

    return new(areaMouseState.IsIn, areaMouseState.LeftPressed);
  }

  public ShapeState DrawTextureRectangle(
    float x,
    float y,
    float width,
    float height,
    float uvX,
    float uvY,
    float uvWidth,
    float uvHeight,
    TextureShapeOptions? shapeOptions = null)
  {
    return DrawTextureRectangle(
      new S.Vector2(x, y),
      new S.Vector2(x + width, y + height),
      new S.Vector2(uvX, uvY),
      new S.Vector2(uvX + uvWidth, uvY + uvHeight),
      shapeOptions);
  }

  public ShapeState DrawTextureRectangle(
    S.Vector2 topLeft,
    S.Vector2 bottomRight,
    S.Vector2 uvTopLeft,
    S.Vector2 uvBottomRight,
    TextureShapeOptions? shapeOptions = null)
  {
    shapeOptions ??= _textureShapeOptions;

    var areaMouseState = _mouseService.GetRectangleMouseState(topLeft, bottomRight);
    shapeOptions = shapeOptions.Value.SetHovered(areaMouseState.IsIn);

    var bottomLeft = new S.Vector2(topLeft.X, bottomRight.Y);
    var topRight = new S.Vector2(bottomRight.X, topLeft.Y);
    var uvBottomLeft = new S.Vector2(uvTopLeft.X, uvBottomRight.Y);
    var uvTopRight = new S.Vector2(uvBottomRight.X, uvTopLeft.Y);
    DrawTextureTriangle(topLeft, bottomLeft, bottomRight, uvTopLeft, uvBottomLeft, uvBottomRight, shapeOptions);
    DrawTextureTriangle(topLeft, bottomRight, topRight, uvTopLeft, uvBottomRight, uvTopRight, shapeOptions);

    return new(areaMouseState.IsIn, areaMouseState.LeftPressed);
  }

  public void DrawColorTriangle(
    S.Vector2 a,
    S.Vector2 b,
    S.Vector2 c,
    ColorShapeOptions? shapeOptions = null)
  {
    EnsureShadersInitialized();
    EnsureBuffersInitialized();

    shapeOptions ??= _colorShapeOptions;

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

    shapeOptions.Value.UseWithShaderProgram(_shaderService.ColorShaderProgramInfo);
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

    shapeOptions ??= _textureShapeOptions;

    var vertices = new float[]
    {
      a.X, a.Y, uvA.X, 1f - uvA.Y,
      b.X, b.Y, uvB.X, 1f - uvB.Y,
      c.X, c.Y, uvC.X, 1f - uvC.Y,
    };

    GL.BindBuffer(BufferTarget.ArrayBuffer, _textureBaoId);
    GL.BufferData(
      BufferTarget.ArrayBuffer,
      vertices.Length * sizeof(float),
      vertices,
      BufferUsageHint.DynamicDraw);

    GL.ActiveTexture(TextureUnit.Texture0);
    GL.BindTexture(TextureTarget.Texture2D, shapeOptions.Value.CurrentTextureId);

    shapeOptions.Value.UseWithShaderProgram(_shaderService.TextureShaderProgramInfo);
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
