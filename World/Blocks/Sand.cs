﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.World;
using OpenTK.Mathematics;

namespace MrX.Game.World.Blocks
{
    public class Sand : Block
    {
        public Sand(Vector3 position) : base(position )
        {
            minx = (1f / 10f) * 8;
            maxx = (1f / 10f) * 9;
            miny = (1f / 10f) * 2;
            maxy = (1f / 10f) * 1;
            BuildUV();
            BuildFaces();
        }
    }
}
