#version 440 core
layout (location = 0) in vec2 aPosition; // vertex coordinates
layout (location = 1) in vec4 aTexCoord; // texture coordinates
out vec4 texCoord;
void main() 
{
	gl_Position = vec4(aPosition,0,1);
	texCoord = aTexCoord;
}