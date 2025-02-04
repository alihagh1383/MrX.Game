using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MrX.Game.Graphics
{
    public class GUI
    {
        VAO chunkVAO;
        VBO chunkVertexVBO, chunkUVVBO;
        IBO chunkIBO;
        Texture texture;
        public GUI()
        {
            
            chunkVAO = new VAO();
            chunkVAO.Bind();
            chunkVertexVBO = new VBO(vectors);
            chunkVertexVBO.Bind();
            chunkVAO.LinkToVAO(0, 2, chunkVertexVBO);
            chunkUVVBO = new VBO(Enumerable.Range(0, vectors.Count).Select(p => new Vector3(0, 0, 0)).ToList());
            chunkUVVBO.Bind();
            chunkVAO.LinkToVAO(1, 3, chunkUVVBO);
            chunkIBO = new IBO(Enumerable.Range(0, vectors.Count).Select(p => (uint)p).ToList());
            texture = new Texture(@"TileSet.png");
            var err = GL.GetError();
            Console.WriteLine(err);
        }
        List<Vector2> vectors = new() { (0, .5f), (.5f, 0), (0, 0), (-.5f, 0), (0, -.5f), (0, 0) };
        public void Render(ShaderProgram program)
        {
            program.Bind();
            chunkVAO.Bind();
            chunkIBO.Bind();
            texture.Bind();
            GL.DrawElements(PrimitiveType.Lines, vectors.Count, DrawElementsType.UnsignedInt, 0);
         
        
        }
    }
}
