using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrX.Game.Isometrik;
using OpenTK.Mathematics;

namespace MrX.Game
{
    internal static class Utility
    {

        internal static Vector4 GetScreenLocationOfPoint(Vector3 Point, Camera camera)
        {
            return new Vector4(Point, 1) * Matrix4.Identity * camera.view * camera.projection;
        }
        internal static Vector3 GetWorldLocationMouseSelecteByY(float y, Camera camera)
        {
            PointF PO = new(
             Input.Mouse.X / (Input.Width / 2) - 1,
             -(Input.Mouse.Y / (Input.Height / 2) - 1));
            return GetWorldLocationByYAndScreenPoint(PO, y, camera);
        }
        internal static Vector3 GetWorldLocationByYAndScreenPoint(PointF point, float y, Camera camera)
        {
            var v = camera.view; var p = camera.projection;
            Console.WriteLine("mp :" + point);
            float f1 = point.X;
            float f2 = point.Y;
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
            f1 = f1 / p.M11; f1 = f1 - (y * v.M21 + v.M41);
            f2 = f2 / p.M22; f2 = f2 - (y * v.M22 + v.M42);
            Matrix2 m1 = new(new(v.M11, v.M31), new(v.M12, v.M32));
            Vector2 f = new(f1, f2);
            Vector2 xz = m1.Inverted() * f;
            return new(xz.X, y, xz.Y);
        }
    }
}
