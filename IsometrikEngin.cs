using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MrX.Game
{
    internal class IsometrikEngin : GameWindow
    {
        internal Dictionary<string, Texture> Textures = [];
        private Dictionary<string, Scene> Screens = [];
        private string ActiveScreen = "Loding";
        internal IsometrikEngin(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Input.Mouse = MouseState;
            Input.Keyboard = KeyboardState;
            Screens.Add(ActiveScreen, new Scene(this, Ui: true, false));
            SetActiveScreen(ActiveScreen);
        }
        public override void Run()
        {
            base.Run();
        }
        protected override void OnLoad()
        {
            /*Get Width And Height*/
            int[] Viwe = new int[4]; GL.GetInteger(GetIndexedPName.Viewport, 00, Viwe); Input.Width = Viwe[2]; Input.Height = Viwe[3];
            /*Set VSync*/
            CursorState = CursorState.Normal;       
            VSync = VSyncMode.On;
            base.OnLoad();
            GL.Enable(EnableCap.Blend); 
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            /*Get Width And Height*/
            Input.Width = e.Width; Input.Height = e.Height;
            base.OnResize(e);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            /*Show FPS*/
            Title = $"(Vsync: {VSync}) FPS: {1f / args.Time:0}";
            /*Clear Last Frame*/
            GL.ClearDepth(1); GL.ClearColor(0.3f, 0.3f, 1f, 1f); GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            /*Render Scene*/
            Screens[ActiveScreen].OnRenderFrame();
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
        protected override void OnUnload()
        {
            Screens[ActiveScreen].OnUnLoad();
            base.OnUnload();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            Input.Mouse = MouseState;
            Input.Keyboard = KeyboardState;
            Input.FPS = (int)(1f / args.Time);
            Screens[ActiveScreen].OnUpdateFrame(args);
            base.OnUpdateFrame(args);
        }
        internal bool GetScreen(string name, out Scene? scene) => Screens.TryGetValue(name, out scene);
        internal bool AddScreen(string name, Scene scene) => Screens.TryAdd(name, scene);
        internal bool SetActiveScreen(string name) { if (Screens.ContainsKey(name)) { Screens[ActiveScreen].OnUnLoad(); ActiveScreen = name; Screens[name].OnLoad(); return true; } return false; }
        internal bool AddTexture(string name, Texture texture) => Textures.TryAdd(name, texture);
        internal bool GetTexture(string name, out Texture? texture) => Textures.TryGetValue(name, out texture);
        internal bool SetActiveTexture(string name, TextureUnit unit = TextureUnit.Texture0) { if (Textures.ContainsKey(name)) { Textures[name].Bind(unit); } return false; }

    }
}
