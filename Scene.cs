using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.Graphics;
using MrX.Game.GUI;
using MrX.Game.Isometrik;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;

namespace MrX.Game
{
    public class Scene(string name, bool IsUI = false, bool IsWorld = false)
    {
        public string Name { get { return name; } }
        public Dictionary<string, object> Data = [];
        internal Dictionary<string, Texture> Textures = [];
        public UI? UI;
        public World? World;
        private bool Loaded = false;
        private bool Active = false;
        public delegate void UpdateFrame(Scene scene);
        public event UpdateFrame? OnUpdate;
        public delegate void RanderFrame(Scene scene);
        public event RanderFrame? OnRander;
        public delegate void BackgroundJob(Scene scene);
        public event BackgroundJob? Job;
        internal void OnLoad()
        {
            if (Loaded) return;
            if (IsUI) UI = new UI(this);
            if (IsWorld) World = new World(this);
            World?.OnLoad();
            UI?.OnLoad();
            Task.Run(() => { Job?.Invoke(this); });
            Loaded = true;

        }
        internal void Disable() => Active = false;
        internal void Enable() => Active = true;
        internal void OnUnLoad()
        {
            if (!Loaded) return;
            World?.OnUnLoad();
            UI?.OnUnLoad();
            foreach (var item in Textures.Values)
            {
                item.Unbind();
            }
            Loaded = false;
        }
        internal void OnRenderFrame()
        {
            if (!Active) return;
            if (!Loaded) OnLoad();
            World?.OnRenderFrame();
            UI?.OnRenderFrame();
            OnRander?.Invoke(this);
        }
        internal void OnUpdateFrame(FrameEventArgs args)
        {
            if (!Active) return;
            if (!Loaded) OnLoad();
            World?.OnUpdateFrame(args);
            UI?.OnUpdateFrame(args);
            OnUpdate?.Invoke(this);
        }
        internal bool AddTexture(string name, Texture texture) => Textures.TryAdd(name, texture);
        internal bool GetTexture(string name, out Texture? texture) => Textures.TryGetValue(name, out texture);
        internal bool SetActiveTexture(string name, TextureUnit unit = TextureUnit.Texture0) { if (Textures.ContainsKey(name)) { Textures[name].Bind(unit); } return false; }

    }
}
