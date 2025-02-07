using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace MrX.Game.GUI
{
    internal class UI
    {
        internal readonly Scene scene;
        private bool Loaded = false;
        ShaderProgram programUi;
        VAO chunkVAO;
        VBO chunkVertexVBO, chunkUVVBO;
        IBO chunkIBO;
        //Texture texture;
        List<Vector2> Vertexs = new() { (0.5f, 0f), (0, 0), (0, 0.5f), (-0.5f, 0), (0, 0), (0, -0.5f) };
        List<Vector4> UVs { get => Enumerable.Range(0, Vertexs.Count).Select(p => new Vector4(
            1,
            Random.Shared.NextSingle(),
            Random.Shared.NextSingle(),
            Random.Shared.NextSingle()
            )).ToList(); }
        List<uint> IBOs { get => Enumerable.Range(0, Vertexs.Count).Select(p => (uint)p).ToList(); }
        public UI(Scene scene)
        {
            this.scene = scene;
        }
        internal void OnLoad()
        {
            if (Loaded) return;
            programUi = new ShaderProgram(Path.Combine("GUI", "GUI.vert"), Path.Combine("GUI", "GUI.frog"));
            chunkVAO = new VAO();
            chunkVAO.Bind();
            chunkVertexVBO = new VBO();
            chunkVertexVBO.Bind();
            chunkVAO.LinkToVAO(0, 2, chunkVertexVBO);
            chunkUVVBO = new VBO();
            chunkUVVBO.Bind();
            chunkVAO.LinkToVAO(1, 4, chunkUVVBO);
            chunkIBO = new IBO();
            //texture = new Texture(@"TileSet.png");
            Console.WriteLine(GL.GetError());
            Loaded = true;
            programUi.Bind();
            chunkVAO.Bind();
            chunkIBO.Bind();
            //var t = new Span<Vector2>(Vertexs.ToArray());
            //Random.Shared.Shuffle<Vector2>(t);
            //Vertexs = t.ToArray().ToList();
          

        }
        internal void OnUnLoad()
        {
            if (!Loaded) return;
            Loaded = false;
        }
        internal void OnRenderFrame()
        {
            if (!Loaded) throw new Exception("Load First");
            GL.Disable(EnableCap.DepthTest);

            //texture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, Vertexs.Count, DrawElementsType.UnsignedInt, 0);
        }
        internal void OnUpdateFrame(FrameEventArgs args)
        {
            if (!Loaded) throw new Exception("Load First");
            chunkVertexVBO.BindData(Vertexs);
            chunkUVVBO.BindData(UVs);
            chunkIBO.BindData(IBOs);
        
        }
    }
}
