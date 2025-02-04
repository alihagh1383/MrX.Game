
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MrX.Game.Graphics;
using MrX.Game.World;
using System.Drawing;
using System.Reflection;
using OpenTK.Compute.OpenCL;

namespace MrX.Game
{
    // Game class that inherets from the Game Window Class
    internal class Game : GameWindow
    {
        GUI GUI;
        ShaderProgram program3D;
        ShaderProgram programUi;
        Camera camera;
        int width => Statics.SCREENWIDTH;
        int height => Statics.SCREENHEIGHT;
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Statics.SCREENHEIGHT = height;
            Statics.SCREENWIDTH = width;
            CenterWindow(new Vector2i(width, height));
            CenterWindow(new Vector2i(width, height));
            CenterWindow(new Vector2i(width, height));
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            Statics.SCREENHEIGHT = e.Height;
            Statics.SCREENWIDTH = e.Width;
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            VSync = VSyncMode.Adaptive;
            program3D = new ShaderProgram("Default.vert", "Default.frag");
            programUi = new ShaderProgram("Default2.vert", "Default2.frag");

            camera = new Camera();
            GUI = new GUI();
            for (int i = 0; i < 2; i++) for (int j = 0; j < 2; j++) for (int k = 0; k < 4; k++) Statics.Chunks[$"{i}:{j}:{k}"] = new Chunk(new Vector3(i, j, k));
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            CursorState = CursorState.Normal;

        }
        protected override void OnUnload()
        {
            base.OnUnload();
            foreach (var item in Statics.Chunks.Values) item.Delete();
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            Console.Clear();
            Title = $"(Vsync: {VSync}) FPS: {1f / args.Time:0}";
            GL.ClearDepth(1);
            GL.ClearColor(0.3f, 0.3f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            program3D.Bind();
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();
            GoBack(view, projection, 1);
            GL.UniformMatrix4(GL.GetUniformLocation(program3D.ID, "model"), true, ref model);
            GL.UniformMatrix4(GL.GetUniformLocation(program3D.ID, "view"), true, ref view);
            GL.UniformMatrix4(GL.GetUniformLocation(program3D.ID, "projection"), true, ref projection);
            foreach (var chunk in Statics.Chunks.Values) chunk.Render(program3D);

            programUi.Bind();
            GUI.Render(programUi);
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
        public void GoBack(Matrix4 v, Matrix4 p, float y)
        {
            Statics.CameraLockAT /= MathF.Abs(Statics.CameraLockAT.Y);
            PointF PO = new(
                (MouseState.X / (Statics.SCREENWIDTH / 2)) - 1,
                -(MouseState.Y / (Statics.SCREENHEIGHT / 2) - 1));
            Console.WriteLine("mp :" + PO);
            float f1 = PO.X;
            float f2 = PO.Y;
            /*
             * f1 = p11 * (v11x + v21y + v31z + v41)
             * f2 = p22 * (v12x + v22y + v32z + v42)  
             * f1 / p11 = v11x + 000y + v31z
             * f2 / p22 = v12x + v22y + v32z
             * f1 - (0000 + v41) = v11x + v31z
             * f2 - (v22y + v42) = v12x + v32z
             |v12  v32||x|  |f2|
             |        || | =|  |
             |v11  v31||z|  |f1|
             */
            f1 = f1 / p.M11;
            f2 = f2 / p.M22;
            f1 = f1 - ((y * v.M21) + v.M41);
            f2 = f2 - ((y * v.M22) + v.M42);
            Matrix2 m1 = new(new(v.M11, v.M31), new(v.M12, v.M32));
            Vector2 f = new(f1, f2);
            Vector2 xz = m1.Inverted() * f;
            Console.WriteLine("c  :" + xz);
            Vector4
                t = (xz.X, y, xz.Y, 1),
                t2 = (0, y, 0, 1),
                t3 = (1, y, 1, 1),
                t4 = (-1, y, -1, 1);
            //Console.WriteLine("tb :" + t * Matrix4.Identity * v * p);
            //Console.WriteLine("a0 :" + t2 * Matrix4.Identity * v * p);
            //Console.WriteLine("a1 :" + t3 * Matrix4.Identity * v * p);
            //Console.WriteLine("a-1:" + t4 * Matrix4.Identity * v * p);
            //Console.WriteLine("-----------------------------------------------------");
            Statics.CameraLockTarget = Statics.CameraPosition + ((Statics.CameraPosition.Y - y) * Statics.CameraLockAT);
            //Console.WriteLine(Statics.CameraLockAT);
            //Console.WriteLine(Statics.CameraLockTarget);
        }


        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;
            if (input.IsKeyDown(Keys.Escape)) Close();
            if (input.IsKeyDown(Keys.Z)) foreach (var chunk in Statics.Chunks.Values) { chunk.GenBlocks(); chunk.BuildChunk(); }
            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);

        }
    }
}


//Console.SetCursorPosition(0, 0);
//Console.Clear();
//Matrix4 model = Matrix4.Identity;
//Matrix4 projection = camera.GetProjectionMatrix();
//Matrix4 view = camera.GetViewMatrix();
//var c3 = (new Vector4(0, 0, 0, 1) * model * view * projection);
//Console.WriteLine(c3);
//Vector4 PO = new((MouseState.X / (Statics.SCREENWIDTH / 2)) - 1, MouseState.Y / (Statics.SCREENHEIGHT / 2) - 1, c3.Z, 1);
//Console.WriteLine(PO);
//var iprojection = projection.Inverted();
//PO *= iprojection;
//Console.WriteLine(PO);
//var iview = view.Inverted();
//PO *= iview;
//Console.WriteLine(PO);
//var imodel = model.Inverted();
//PO *= imodel;
//Console.WriteLine(PO);