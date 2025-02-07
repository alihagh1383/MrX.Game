using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MrX.Game.Isometrik
{
    internal class Camera
    {
        public float
          CameraSPEED = 1f,
          CameraZOME = 1f;
        public Vector3
            CameraPosition = new Vector3(-2000, 1000, -2000),
            CameraLockAT = new Vector3(20, -10, 20),
            CameraUP = Vector3.UnitY,
            CameraLockTarget = new Vector3(0, 0, 0);
        int SCREENWIDTH => Input.Width;
        int SCREENHEIGHT => Input.Height;
        public Matrix4 view;
        public Matrix4 projection;
        public Camera()
        {
            GetProjectionMatrix(); GetViewMatrix();
            CameraLockAT /= MathF.Abs(CameraLockAT.Y);
        }
        public Matrix4 GetViewMatrix()
        {
            CameraLockAT /= MathF.Abs(CameraLockAT.Y);
            var m = Matrix4.LookAt(CameraPosition, CameraPosition + CameraLockAT, CameraUP);
            view = m;
            return m;
        }
        public Matrix4 GetProjectionMatrix()
        {
            float w = SCREENWIDTH / CameraZOME / 50 / 2;
            float h = SCREENHEIGHT / CameraZOME / 75 / 2;
            var m = Matrix4.CreateOrthographicOffCenter(-w, w, -h, h, 0.0f, 1000000.0f);
            projection = m;
            return m;
            //return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(1), (w) / (h), 0.1f, 10000.0f);
        }
        public void Update(KeyboardState input, MouseState mouse, FrameEventArgs e)
        {
            if (input.IsKeyDown(Keys.W))
            {
                CameraPosition.X += CameraSPEED * (float)e.Time;
                CameraPosition.Z += CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                CameraPosition.X -= CameraSPEED * (float)e.Time;
                CameraPosition.Z -= CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                CameraPosition.X += CameraSPEED * (float)e.Time;
                CameraPosition.Z -= CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                CameraPosition.X -= CameraSPEED * (float)e.Time;
                CameraPosition.Z += CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                CameraPosition.Y += CameraSPEED * (float)e.Time;
                CameraPosition.X -= CameraSPEED * (float)e.Time;
                CameraPosition.Z -= CameraSPEED * (float)e.Time;
                CameraPosition.X -= CameraSPEED * (float)e.Time;
                CameraPosition.Z -= CameraSPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                CameraPosition.Y -= CameraSPEED * (float)e.Time * 100;
                CameraPosition.X += CameraSPEED * (float)e.Time * 100;
                CameraPosition.Z += CameraSPEED * (float)e.Time * 100;
                CameraPosition.X += CameraSPEED * (float)e.Time * 100;
                CameraPosition.Z += CameraSPEED * (float)e.Time * 100;
            }
            if (input.IsKeyPressed(Keys.KeyPadAdd)) { CameraZOME = CameraZOME * 2; }
            if (input.IsKeyPressed(Keys.KeyPadDivide)) { CameraZOME = CameraZOME / 2; }
            if (mouse.ScrollDelta.Y != 0)
                CameraZOME = mouse.ScrollDelta.Y > 0 ? CameraZOME * 2 : CameraZOME / 2;
            if (CameraZOME < 0) CameraZOME = -CameraZOME;
        }
    }
}
