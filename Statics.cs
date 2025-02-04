using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.World;
using OpenTK.Mathematics;

namespace MrX.Game
{
    public static class Statics
    {
        static public float
            CameraSPEED = 1f,
            CameraZOME = 1f;
        static public Vector3
            CameraPosition = new Vector3(-2000,1000, -2000),
            CameraLockAT = new Vector3(20, -10, 20),
            CameraUP = Vector3.UnitY,
            CameraLockTarget = new Vector3(0, 0, 0);
        static public int
            SCREENWIDTH,
            SCREENHEIGHT;
        public static Dictionary<string, Block> Blocks = new();
        public static Dictionary<string, Chunk> Chunks = new(); 
    }
}
