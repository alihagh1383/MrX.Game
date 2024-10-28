using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MrX.Game
{

    public class Engine
    {
        private System.Threading.Timer RenderTimer;
        public TimeSpan RenderPeriodTime;
        public Engine() : this(1) { }
        public Engine(double RenderPeriodTimeFromSecond)
        {
            this.RenderPeriodTime = TimeSpan.FromSeconds(RenderPeriodTimeFromSecond);
            RenderTimer = new System.Threading.Timer((s) => GameObjects.ForEach(p => p.Render()));

        }
        public List<GameObjects> GameObjects = new List<GameObjects>();
        public void StartUpdate() => GameObjects.ForEach(p => p.EnableUpdate());
        public void StopUpdate() => GameObjects.ForEach(p => p.DisableUpdate());
        //public void StartRender() => GameObjects.ForEach(p => p.EnableRender());
        //public void StopRender() => GameObjects.ForEach(p => p.DisableRender());
        public void FirstRender() => GameObjects.ForEach(p => p.Render());
        public void StartRender() => RenderTimer.Change((RenderPeriodTime + TimeSpan.FromSeconds(0.1)), (RenderPeriodTime));
        public void StopRender() => RenderTimer.Change(0, 0);



    }
}
