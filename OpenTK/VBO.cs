using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrX.Game.Graphics
{
    internal class VBO
    {
        public int ID;
        public VBO()
        {
            ID = GL.GenBuffer();
        }
        public void BindData(List<Vector4> data)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector4.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
        }
        public void BindData(List<Vector3> data)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector3.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
        }
        public void BindData(List<Vector2> data)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector2.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
        }
        public void BindData(List<int> data)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * sizeof(int), data.ToArray(), BufferUsageHint.StaticDraw);
        }
        public void Bind() { GL.BindBuffer(BufferTarget.ArrayBuffer, ID); }
        public void Unbind() { GL.BindBuffer(BufferTarget.ArrayBuffer, 0); }
        public void Delete() { GL.DeleteBuffer(ID); }
    }
}
