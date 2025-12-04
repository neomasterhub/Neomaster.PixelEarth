#version 330 core

out vec4 FragColor;
uniform vec4 uFillNormal;
uniform vec4 uFillHovered;
uniform vec4 uFillSelected;
uniform bool uIsHovered;
uniform bool uIsSelected;

void main()
{
  if (uIsSelected)
  {
    FragColor = uFillSelected;
    return;
  }

  if (uIsHovered)
  {
    FragColor = uFillHovered;
    return;
  }

  FragColor = uFillNormal;
}
