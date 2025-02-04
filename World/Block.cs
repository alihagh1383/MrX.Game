using MrX.Game.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrX.Game.World
{
    public class Block
    {
        public Vector3 position;
        public List<Vector3> Verts
        {
            get => RenderFase.Where(p => p.Value).SelectMany(p => faces[p.Key].vertices).ToList();
        }
        public List<Vector2> UVs
        {
            get => RenderFase.Where(p => p.Value).SelectMany(p => faces[p.Key].uv).ToList();

        }
        public List<uint> Indices
        {
            get
            {
                List<uint> result = new();
                uint indexCount = 0;
                foreach (var p in RenderFase.Where(p => p.Value))
                {
                    result.Add(0 + indexCount);
                    result.Add(1 + indexCount);
                    result.Add(2 + indexCount);
                    result.Add(2 + indexCount);
                    result.Add(3 + indexCount);
                    result.Add(0 + indexCount);
                    indexCount += 4;
                }
                return result;
            }
        }
        public Dictionary<Faces, FaceData> faces;
        public Dictionary<Faces, bool> RenderFase = new() {/* { Faces.BACK, false }, { Faces.FRONT, false }, { Faces.BOTTOM, false },*/ { Faces.TOP, false },/* { Faces.LEFT, false }, { Faces.RIGHT, false }, */};
        public float minx, maxx, miny, maxy;
        public List<Vector2> dirtUV;
        public bool Update = false;
        public string ChunkPos;
        public Block(Vector3 position)
        {
            this.position = position;
            ChunkPos = $"{(int)position.X / 5}:{(int)position.Y / 5}:{(int)position.Z / 5}";
            BuildUV();
            BuildFaces();
        }
        public void BuildUV()
        {
            dirtUV = new List<Vector2> { new(miny, maxx), new(maxy, maxx), new(maxy, minx), new(miny, minx), };
        }
        public void BuildFaces()
        {
            faces = new Dictionary<Faces, FaceData>
            {
                {Faces.TOP, new FaceData {
                    vertices = (FaceDataRaw.rawVertexData[Faces.TOP]).Select(p=>p+position).ToList(),
                    uv = dirtUV
                } },
            };
        }
        public void Enable()
        {
            Statics.Chunks[ChunkPos].Update = true;
            dirtUV = new List<Vector2>
            {
                new Vector2(0, 0),
                new Vector2(0, 0),
                new Vector2(0, 0),
                new Vector2(0, 0),
            };
            BuildFaces();
        }
        public void Disable()
        {
            Update = true;
            BuildUV();
            BuildFaces();
        }
    }
}
