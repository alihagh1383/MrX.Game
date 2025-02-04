using MrX.Game.Graphics;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.World.Blocks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Collections;

namespace MrX.Game.World
{
    public class Chunk
    {
        public List<Vector3> chunkVerts
        {
            get => GetBlocks().Where(p => p is not null).SelectMany(v => v.Verts).ToList();
        }
        public List<Vector2> chunkUVs
        {
            get => GetBlocks().Where(p => p is not null).SelectMany(v => v.UVs).ToList();
        }
        public List<uint> chunkIndices
        {
            get
            {
                uint count = 0;
                List<uint> result = new();
                foreach (var v in GetBlocks())
                {
                    if (v is null) continue;
                    var vi = v.Indices.Select(p => p + count);
                    result.AddRange(vi);
                    count = (uint)vi.Max() + 1;
                }
                return result;
            }
        }
        public const int SIZE = 5;
        public const int HEIGHT = 1;
        public Vector3 position;
        VAO chunkVAO;
        VBO chunkVertexVBO, chunkUVVBO;
        IBO chunkIBO;
        Texture texture;
        public bool Update = true, BeRender = true;
        int seed;
        public Chunk(Vector3 postition, int seed = 150)
        {
            this.seed = seed;

            this.position = postition * new Vector3(SIZE, HEIGHT, SIZE);
            //if (position.X < 0) this.position.X -= 1;
            //if (position.Y < 0) this.position.Y -= 1;
            //if (position.Z < 0) this.position.Z -= 1;
            GenBlocks();
            BuildChunk();
        }
        public void GenBlocks()
        {
            var nois = new WorldGenerator(seed);
            Block block;
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < HEIGHT; j++)
                    for (int k = 0; k < SIZE; k++)
                    {
                        float
                            x = (float)(i + position.X - (position.Y * HEIGHT)),
                            y = ((float)j + position.Y),
                            z = ((float)k + position.Z - (position.Y * HEIGHT));
                        block = nois.BlockCategoryAtPosition(new(x, y, z));
                        block.RenderFase[Faces.TOP] = true;
                        Statics.Blocks[$"{x}:{y}:{z}"] = block;
                    }
        }
        public IEnumerable<Block> GetBlocks()
        {
            for (int i = 0; i < SIZE; i++)
                for (int j = 0; j < HEIGHT; j++)
                    for (int k = 0; k < SIZE; k++)
                    {
                        float
                            x = (float)(i + position.X - (position.Y * HEIGHT)),
                            y = ((float)j + position.Y),
                            z = ((float)k + position.Z - (position.Y * HEIGHT));
                        yield return Statics.Blocks[$"{x}:{y}:{z}"];
                    }
        }
        public void BuildChunk()
        {
            chunkVAO = new VAO();
            chunkVAO.Bind();
            chunkVertexVBO = new VBO(chunkVerts);
            chunkVertexVBO.Bind();
            chunkVAO.LinkToVAO(0, 3, chunkVertexVBO);
            chunkUVVBO = new VBO(chunkUVs);
            chunkUVVBO.Bind();
            chunkVAO.LinkToVAO(1, 2, chunkUVVBO);
            chunkIBO = new IBO(chunkIndices);
            texture = new Texture(@"TileSet.png");
        }
        public void Render(ShaderProgram program)
        {
            if (!BeRender) { return; }
            chunkVAO.Bind();
            if (Update) { chunkUVVBO.Update(chunkUVs); Update = false; }
            chunkIBO.Bind();
            texture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, chunkIndices.Count, DrawElementsType.UnsignedInt, 0);
            texture.Unbind();
            chunkIBO.Unbind();
            chunkVAO.Unbind();

        }
        public void Delete()
        {
            chunkVAO.Delete();
            chunkVertexVBO.Delete();
            chunkUVVBO.Delete();
            chunkIBO.Delete();
            texture.Delete();
        }
    }
}
