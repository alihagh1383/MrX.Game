using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Timers;

namespace MrX.Game
{
    public class GameObjects
    {
        private System.Threading.Timer UpdateTimer;
        public event EventHandler<GameObjects> UpdateJob;
        public TimeSpan UpdatePeriodTime;
        //private System.Threading.Timer RenderTimer;
        public event EventHandler<GameObjects> RenderJob;
        //public TimeSpan RenderPeriodTime;
        public object State = 0;
        public bool ChangeState = true;
        public required List<GameObjects> GameObjectsList;
        public GameObjects(double UpdatePeriodTimeFromSecond, double RenderPeriodTimeFromSecond)
        {
            RenderJob = (s, e) => { };
            UpdateJob = (s, e) => { };
            //this.RenderPeriodTime = TimeSpan.FromSeconds(RenderPeriodTimeFromSecond);
            this.UpdatePeriodTime = TimeSpan.FromSeconds(UpdatePeriodTimeFromSecond);
            //RenderTimer = new System.Threading.Timer((s) => RenderJob?.Invoke(this, this));
            UpdateTimer = new System.Threading.Timer((s) => { ChangeState = true; UpdateJob?.Invoke(this, this); });
        }
        public GameObjects() : this(1, 1) { }
        public GameObjects(double UpdatePeriodTime) : this(UpdatePeriodTime, 1) { }
        public void EnableUpdate() => UpdateTimer.Change((UpdatePeriodTime), (UpdatePeriodTime));
        public void DisableUpdate() => UpdateTimer.Change(0, 0);
        //public void EnableRender() => RenderTimer.Change((RenderPeriodTime+TimeSpan.FromSeconds(0.1)), (RenderPeriodTime));
        //public void DisableRender() => RenderTimer.Change(0, 0);
        public void Render() { if (ChangeState) { ChangeState = false; RenderJob?.Invoke(this, this); } }
    }
}
