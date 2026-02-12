using System.Diagnostics;
using Neomaster.PixelEarth.App;
using Neomaster.PixelEarth.Infra.Extensions;
using Neomaster.PixelEarth.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Neomaster.PixelEarth.Infra;

public class ShapeService : IShapeService
{
  private readonly IShaderService _shaderService;
  private readonly RenderSettings _renderSettings;
  private readonly ColorShapeOptions _colorShapeOptions;
  private readonly TextureShapeOptions _textureShapeOptions;
  private readonly ShaderProgramArg<Matrix4> _positionProjection;

  private int _colorVaoId;
  private int _colorBaoId;
  private int _textureVaoId;
  private int _textureBaoId;

  public ShapeService(
    IShaderService shaderService,
    RenderSettings renderSettings,
    WindowSettings windowSettings,
    ColorShapeOptions colorShapeOptions,
    TextureShapeOptions textureShapeOptions)
  {
    _shaderService = shaderService;
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
  }

  public void DrawTextureRectangle(Rectangle rectangle, TextureShapeOptions? shapeOptions = null)
  {
    var so = shapeOptions ?? _textureShapeOptions;
    DrawTextureTriangle(rectangle.GetTriangle_BL(), so.FromRectangleForTriangle_BL());
    DrawTextureTriangle(rectangle.GetTriangle_TR(), so.FromRectangleForTriangle_TR());
  }

  public void DrawTextureTriangle(Triangle triangle, TextureShapeOptions? shapeOptions = null)
  {
    EnsureShadersInitialized();
    EnsureBuffersInitialized();

    var so = shapeOptions ?? _textureShapeOptions;
    var currentShapeState = so.GetCurrentState();
    var uvA = currentShapeState.UV[0];
    var uvB = currentShapeState.UV[1];
    var uvC = currentShapeState.UV[2];

    var vertices = new float[]
    {
      triangle.A.X, triangle.A.Y, uvA.X, 1f - uvA.Y,
      triangle.B.X, triangle.B.Y, uvB.X, 1f - uvB.Y,
      triangle.C.X, triangle.C.Y, uvC.X, 1f - uvC.Y,
    };

    GL.BindBuffer(BufferTarget.ArrayBuffer, _textureBaoId);
    GL.BufferData(
      BufferTarget.ArrayBuffer,
      vertices.Length * sizeof(float),
      vertices,
      BufferUsageHint.DynamicDraw);

    GL.ActiveTexture(TextureUnit.Texture0);
    GL.BindTexture(TextureTarget.Texture2D, currentShapeState.TextureId);

    so.UseWithShaderProgram(_shaderService.TextureShaderProgramInfo);
    _positionProjection.BindMatrix4(_shaderService.TextureShaderProgramInfo.Id);

    GL.BindVertexArray(_textureVaoId);
    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

    GL.BindVertexArray(0);
    GL.UseProgram(0);
  }

  public void DrawColorRectangle(Rectangle rectangle, ColorShapeOptions? shapeOptions = null)
  {
    DrawColorTriangle(rectangle.GetTriangle_BL(), shapeOptions);
    DrawColorTriangle(rectangle.GetTriangle_TR(), shapeOptions);
  }

  public void DrawColorTriangle(Triangle triangle, ColorShapeOptions? shapeOptions = null)
  {
    EnsureShadersInitialized();
    EnsureBuffersInitialized();

    var so = shapeOptions ?? _colorShapeOptions;
    var vertices = new float[]
    {
      triangle.A.X, triangle.A.Y,
      triangle.B.X, triangle.B.Y,
      triangle.C.X, triangle.C.Y,
    };

    GL.BindBuffer(BufferTarget.ArrayBuffer, _colorBaoId);
    GL.BufferData(
      BufferTarget.ArrayBuffer,
      vertices.Length * sizeof(float),
      vertices,
      BufferUsageHint.DynamicDraw);

    so.UseWithShaderProgram(_shaderService.ColorShaderProgramInfo);
    _positionProjection.BindMatrix4(_shaderService.ColorShaderProgramInfo.Id);

    so.CullFaces.Apply();
    GL.FrontFace(_renderSettings.WindingOrder.ToGlType());

    GL.BindVertexArray(_colorVaoId);
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
