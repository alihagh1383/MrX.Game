using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;

namespace MrX.Game.Isometrik
{
    public class World
    {
        internal readonly Scene scene;
        internal Camera Camera;
        private bool Loaded = false;
        public World(Scene scene)
        {
            this.scene = scene;
            Camera = new();
        }
        internal void OnLoad()
        {
            if (Loaded) return;
            Loaded = true;
        }
        internal void OnUnLoad()
        {
            if (!Loaded) return;
            Loaded = false;
        }
        internal void OnRenderFrame()
        {
            if (!Loaded) OnLoad();
        }
        internal void OnUpdateFrame(FrameEventArgs args)
        {
            if (!Loaded) OnLoad();
        }
    }
}
