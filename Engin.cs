using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.Graphics;
using MrX.Game.GUI;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MrX.Game
{
    public static class Engin
    {
        static public GameWindow Window = new(GameWindowSettings.Default, NativeWindowSettings.Default);
        static public Dictionary<string, ElementsStyle> ElementStyles = [];
        static public Dictionary<string, TextStyle> TextStyles = [];
        static public Dictionary<string, Texture> Textures = [];
        static private Dictionary<string, Scene> Screens = [];
        static private string ActiveScreen = string.Empty;
        static public ShaderProgram WorldShaderProgram;
        static public ShaderProgram UIShaderProgram;
        static public bool AddScreen(Scene scene) => Screens.TryAdd(scene.Name, scene);
        static public bool SetActiveScreen(string name)
        {
            if (Screens.ContainsKey(name))
            {
                Screens[ActiveScreen].Disable();
                ActiveScreen = name;
                Screens[name].Enable();
                return true;
            }
            return false;
        }
        static public bool SetActiveScreen(Scene screen) => SetActiveScreen(screen.Name);
        static public bool AddTexture(string name, Texture texture) => Textures.TryAdd(name, texture);
        static public bool GetTexture(string name, out Texture? texture) => Textures.TryGetValue(name, out texture);
        static public bool SetActiveTexture(string name, TextureUnit unit = TextureUnit.Texture0) { if (Textures.ContainsKey(name)) { Textures[name].Bind(unit); } return false; }
        static public void Run(string FirstScreen)
        {
            ActiveScreen = FirstScreen;
            Window.Load += () =>
            {
                int[] Viwe = new int[4]; GL.GetInteger(GetIndexedPName.Viewport, 00, Viwe); Input.Width = Viwe[2]; Input.Height = Viwe[3];
                Window.CursorState = CursorState.Normal;
                Window.VSync = VSyncMode.Adaptive;
                GL.Enable(EnableCap.Blend);
                Engin.UIShaderProgram = new ShaderProgram(Path.Combine("GUI", "GUI.vert"), Path.Combine("GUI", "GUI.frog"));
                Engin.WorldShaderProgram = new ShaderProgram(Path.Combine("Isometrik", "Isometrik.vert"), Path.Combine("Isometrik", "Isometrik.frog"));
                SetActiveScreen(ActiveScreen);
            };
            Window.Resize += (e) =>
            {
                GL.Viewport(0, 0, e.Width, e.Height);
                Input.Width = e.Width; Input.Height = e.Height;
            };
            Window.RenderFrame += (e) =>
            {
                Window.Title = $"(Vsync: {Window.VSync}) FPS: {1f / e.Time:0}";
                GL.ClearDepth(1); GL.ClearColor(0.3f, 0.3f, 1f, 1f); GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Screens[ActiveScreen].OnRenderFrame();
                Window.Context.SwapBuffers();
            };
            Window.Unload += () => { Screens[ActiveScreen].OnUnLoad(); };
            Window.UpdateFrame += (e) =>
            {
                Input.Mouse = Window.MouseState;
                Input.Keyboard = Window.KeyboardState;
                Input.FPS = (int)(1f / e.Time);
                Screens[ActiveScreen].OnUpdateFrame(e);
            };
            Window.Run();
        }



    }
}
