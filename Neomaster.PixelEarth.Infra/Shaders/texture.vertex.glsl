#version 330 core

layout(location = 0) in vec2 aPosition;
layout(location = 1) in vec2 aUV;
uniform mat4 uProjection;
out vec2 vUV;

void main()
{
  gl_Position = uProjection * vec4(aPosition, 0.0, 1.0);
  vUV = aUV;
}
