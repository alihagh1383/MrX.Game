using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrX.Game.World
{
    public enum Faces
    {
        TOP,
    }

    public struct FaceData
    {
        public List<Vector3> vertices;
        public List<Vector2> uv;
    }

    public struct FaceDataRaw
    {
        public static readonly Dictionary<Faces, List<Vector3>> rawVertexData = new Dictionary<Faces, List<Vector3>>
        {
            {Faces.TOP, new List<Vector3>()
            {
                new Vector3(-.5f, .5f, .5f), // topleft vert
                new Vector3(.5f, .5f, -.5f), // bottomleft vert
                new Vector3(.5f, -.5f, -.5f), // bottomright vert
                new Vector3(-.5f, -.5f, .5f), // topright vert
            } },

        };
    }

}
