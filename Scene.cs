using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.GUI;
using MrX.Game.Isometrik;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;

namespace MrX.Game
{
    internal class Scene
    {
        internal IsometrikEngin IsometrikEngin { get; }
        internal UI? UI;
        internal World? World;
        private bool Loaded = false;
        internal Scene(IsometrikEngin isometrikEngin, bool Ui = false, bool Isometrik = false)
        {
            IsometrikEngin = isometrikEngin;
            if (Ui) UI = new UI(this);
            if (Isometrik) World = new World(this);
        }
        internal void OnLoad()
        {
            if (Loaded) return;
            World?.OnLoad();
            UI?.OnLoad();
            Loaded = true;

        }
        internal void OnUnLoad()
        {
            if (!Loaded) return;
            World?.OnUnLoad();
            UI?.OnUnLoad();
            Loaded = false;
        }
        internal void OnRenderFrame()
        {
            if (!Loaded) throw new Exception("Load First");
            World?.OnRenderFrame();
            UI?.OnRenderFrame();
        }
        internal void OnUpdateFrame(FrameEventArgs args)
        {
            if (!Loaded) throw new Exception("Load First");
            World?.OnUpdateFrame(args);
            UI?.OnUpdateFrame(args);
        }
    }
}
