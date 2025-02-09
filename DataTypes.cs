using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrX.Game
{


    /// <summary>
    /// len of image is 1 
    /// </summary>
    /// <example>
    /// maxx = 5f/10f;
    /// minx = 4f/10f;
    /// </example>
    public struct PositionInTexture
    {
        public float maxx, maxy, minx, miny;
    }
    public enum PositionHorizontal
    {
        Left, Center, Right
    }
    public enum PositionVertical
    {
        Top, Bottom, Center
    }
}
