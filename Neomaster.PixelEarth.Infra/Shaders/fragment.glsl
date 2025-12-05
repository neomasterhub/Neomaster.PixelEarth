#version 330 core

out vec4 FragColor;
uniform vec4 uFillNormal;
uniform vec4 uFillHovered;
uniform bool uIsHovered;

void main()
{
  FragColor = uIsHovered ? uFillHovered : uFillNormal;
}
