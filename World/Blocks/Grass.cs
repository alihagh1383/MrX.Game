using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.World;
using OpenTK.Mathematics;

namespace MrX.Game.World.Blocks
{
    public class Grass : Block
    {

        public Grass(Vector3 position) : base(position )
        {
            var p = Random.Shared.Next(1, 4);
            minx = (1f / 10f) * 9;
            maxx = (1f / 10f) * 10;
            miny = (1f / 10f) * (p + 1);
            maxy = (1f / 10f) * p;
            BuildUV();
            BuildFaces();
        }

    }
}
