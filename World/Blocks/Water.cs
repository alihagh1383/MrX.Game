using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace MrX.Game.World.Blocks
{
    public class Water : Block
    {
        public Water(Vector3 position) : base(position)
        {
            minx = (1f / 10f) * 6;
            maxx = (1f / 10f) * 7;
            miny = (1f / 10f) * 2;
            maxy = (1f / 10f) * 1;
            BuildUV();
            BuildFaces();
        }
    }
}
