using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace MrX.Game.GUI
{
    public class Element(ElementsStyle Style)
    {
        public required UI UI { get; set; }
        public bool IsHover = false;
        public bool IsDisable = false;
        public List<uint> IBOs { get; set; } = [0, 1, 2, 2, 3, 0];
        public Vector2 Position { get; set; }
        public PositionHorizontal PositionHorizontal { get; set; } = PositionHorizontal.Center;
        public PositionVertical PositionVertical { get; set; } = PositionVertical.Center;
        public float WHITE = 0.5f, HEIGHT = 0.5f;
        public List<Vector2> Vertexs
        {
            get
            {
                float left = 0, right = 0, top = 0, bottom = 0;
                if (PositionHorizontal == PositionHorizontal.Left) { left = Position.X; right = Position.X + WHITE; }
                else if (PositionHorizontal == PositionHorizontal.Center) { left = Position.X + (WHITE / 2); right = Position.X + (WHITE / 2); }
                else if (PositionHorizontal == PositionHorizontal.Right) { left = Position.X; right = Position.X - (WHITE); }
                if (PositionVertical == PositionVertical.Top) { top = Position.Y; bottom = Position.Y - HEIGHT; }
                else if (PositionVertical == PositionVertical.Center) { top = Position.Y - (HEIGHT / 2); bottom = Position.Y - (HEIGHT / 2); }
                else if (PositionVertical == PositionVertical.Bottom) { top = Position.Y + (HEIGHT); bottom = Position.Y; }
                return new() { (top, left), (top, right), (bottom, right), (bottom, left) };
            }
        }
        public List<Vector4> UVs
        {
            get
            {
                if (Style.UseTexture)
                {
                    return new()
                    {
                        (0,Style.ActivePosition.minx,Style.ActivePosition.miny,0),
                        (0,Style.ActivePosition.minx,Style.ActivePosition.maxy,0),
                        (0,Style.ActivePosition.maxx,Style.ActivePosition.maxy,0),
                        (0,Style.ActivePosition.maxx,Style.ActivePosition.miny,0),
                    };
                }
                else
                {
                    return new() 
                    {
                        (1, Style.ActiveColor.X, Style.ActiveColor.Y, Style.ActiveColor.Z),
                        (1, Style.ActiveColor.X, Style.ActiveColor.Y, Style.ActiveColor.Z),
                        (1, Style.ActiveColor.X, Style.ActiveColor.Y, Style.ActiveColor.Z),
                        (1, Style.ActiveColor.X, Style.ActiveColor.Y, Style.ActiveColor.Z)
                    };
                }
            }
        }
        public void Update() => UI.Update = true;
        public delegate void OnHover(Element e);
        public delegate void OnClick(Element e);
        public event OnHover? onHover;
        public event OnClick? onClick;
        public void Hover() => onHover?.Invoke(this);
        public void Click() => onClick?.Invoke(this);
    }
}
