#version 330 core
layout (location = 0) in vec2 aPosition; // vertex coordinates
layout (location = 1) in vec3 aTexCoord; // texture coordinates
out vec3 color;
void main() 
{
	gl_Position = vec4(aPosition,0,1);
	color = aTexCoord;
}