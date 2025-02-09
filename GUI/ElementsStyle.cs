using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace MrX.Game.GUI
{
    public class ElementsStyle
    {
        public string TextureName = string.Empty;
        public bool UseTexture = true;
        public PositionInTexture ActivePosition;
        public PositionInTexture PositionDefault;
        public PositionInTexture PositionHover;
        public PositionInTexture PositionDisable;
        public Vector3 ActiveColor;
        public Vector3 ColorDefault;
        public Vector3 ColorHover;
        public Vector3 ColorDisable;
    }
}
