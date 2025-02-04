using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrX.Game
{
    internal class Camera
    {
        int SCREENWIDTH => Statics.SCREENWIDTH;
        int SCREENHEIGHT => Statics.SCREENHEIGHT;
        public Matrix4 GetViewMatrix()
        {
            var m = Matrix4.LookAt(Statics.CameraPosition, Statics.CameraPosition + Statics.CameraLockAT, Statics.CameraUP);
            return m;
        }
        public Matrix4 GetProjectionMatrix()
        {
            float w = ((SCREENWIDTH / Statics.CameraZOME) / 50) / 2;
            float h = ((SCREENHEIGHT / Statics.CameraZOME) / 75) / 2;
            var m = Matrix4.CreateOrthographicOffCenter(-w, w, -h, h, 0.0f, 1000000.0f);
            return m;
            //return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(1), (w) / (h), 0.1f, 10000.0f);
        }
        public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs e)
        {
            if (input.IsKeyDown(Keys.W))
            {
                Statics.CameraPosition.X += Statics.CameraSPEED * (float)e.Time;
                Statics.CameraPosition.Z += Statics.CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                Statics.CameraPosition.X -= Statics.CameraSPEED * (float)e.Time;
                Statics.CameraPosition.Z -= Statics.CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                Statics.CameraPosition.X += Statics.CameraSPEED * (float)e.Time;
                Statics.CameraPosition.Z -= Statics.CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                Statics.CameraPosition.X -= Statics.CameraSPEED * (float)e.Time;
                Statics.CameraPosition.Z += Statics.CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                Statics.CameraPosition.Y += Statics.CameraSPEED * (float)e.Time;
                Statics.CameraPosition.X -= Statics.CameraSPEED * (float)e.Time;
                Statics.CameraPosition.Z -= Statics.CameraSPEED * (float)e.Time;
                Statics.CameraPosition.X -= Statics.CameraSPEED * (float)e.Time;
                Statics.CameraPosition.Z -= Statics.CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                Statics.CameraPosition.Y -= Statics.CameraSPEED * (float)e.Time * 100;
                Statics.CameraPosition.X += Statics.CameraSPEED * (float)e.Time * 100;
                Statics.CameraPosition.Z += Statics.CameraSPEED * (float)e.Time * 100;
                Statics.CameraPosition.X += Statics.CameraSPEED * (float)e.Time * 100;
                Statics.CameraPosition.Z += Statics.CameraSPEED * (float)e.Time * 100;
            }
            if (input.IsKeyPressed(Keys.KeyPadAdd)) { Statics.CameraZOME = Statics.CameraZOME * 2; }
            if (input.IsKeyPressed(Keys.KeyPadDivide)) { Statics.CameraZOME = Statics.CameraZOME / 2; }
            if (mouse.ScrollDelta.Y != 0)
                Statics.CameraZOME = (mouse.ScrollDelta.Y > 0) ? Statics.CameraZOME * 2 : Statics.CameraZOME / 2;
            if (Statics.CameraZOME < 0) Statics.CameraZOME = -Statics.CameraZOME;
            //else if (Statics.CameraZOME < 0.3f) Statics.CameraZOME = 0.3f;
            //Console.SetCursorPosition(0, 10);
            //Console.WriteLine(Statics.CameraPosition);
            //Console.WriteLine(Statics.CameraZOME);
            //Console.WriteLine(1d / e.Time);
        }
        public void Update(KeyboardState input, MouseState mouse, FrameEventArgs e)
        {
            InputController(input, mouse, e);
        }
    }
}
