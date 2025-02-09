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
    public class UI
    {
        internal readonly Scene scene;
        private bool Loaded = false;
        ShaderProgram programUi => Engin.UIShaderProgram;
        VAO chunkVAO = new VAO();
        VBO chunkVertexVBO = new VBO(), chunkUVVBO = new VBO();
        IBO chunkIBO = new IBO();
        public bool Update = true;
        public Dictionary<string, Element> Elements = [];
        List<Vector2> vertexs = [];
        List<Vector4> uvs = [];
        List<uint> ibos = [];
        //Texture texture;
        public List<Vector2> Vertexs = new() { (0.5f, 0f), (0, 0), (0, 0.5f), (-0.5f, 0), (0, 0), (0, -0.5f) };
        List<Vector4> UVs
        {
            get => Enumerable.Range(0, Vertexs.Count).Select(p => new Vector4(1, 0, 0.5f, 001f)).ToList();
        }
        List<uint> IBOs { get => Enumerable.Range(0, Vertexs.Count).Select(p => (uint)p).ToList(); }
        public UI(Scene scene)
        {
            this.scene = scene;
        }
        internal void OnLoad()
        {
            if (Loaded) return;
            chunkVAO.Bind();
            chunkVertexVBO.Bind();
            chunkVAO.LinkToVAO(0, 2, chunkVertexVBO);
            chunkUVVBO.Bind();
            chunkVAO.LinkToVAO(1, 4, chunkUVVBO);
            Console.WriteLine(GL.GetError());
            programUi.Bind();
            chunkIBO.Bind();
            Loaded = true;
            var t = new Span<Vector2>(Vertexs.ToArray());
            Random.Shared.Shuffle<Vector2>(t);
            Vertexs = t.ToArray().ToList();

        }
        internal void OnUnLoad()
        {
            if (!Loaded) return;
            Loaded = false;
        }
        internal void OnRenderFrame()
        {
            if (!Loaded) OnLoad();
            chunkVAO.Bind();
            chunkVertexVBO.Bind();
            chunkUVVBO.Bind();
            programUi.Bind();
            chunkIBO.Bind();
            GL.Disable(EnableCap.DepthTest);
            GL.DrawElements(PrimitiveType.Triangles, Vertexs.Count, DrawElementsType.UnsignedInt, 0);
        }
        internal void OnUpdateFrame(FrameEventArgs args)
        {
            if (!Loaded) OnLoad();
            chunkVAO.Bind();
            chunkVertexVBO.Bind();
            chunkUVVBO.Bind();
            programUi.Bind();
            chunkIBO.Bind();
            chunkVertexVBO.BindData(Vertexs);
            chunkUVVBO.BindData(UVs);
            chunkIBO.BindData(IBOs);


        }
    }
}
