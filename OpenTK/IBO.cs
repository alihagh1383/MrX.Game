using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MrX.Game.Graphics
{
    internal class IBO
    {
        public int ID;
        public IBO()
        {
            ID = GL.GenBuffer();
        }
        public void BindData(List<uint> data)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StaticDraw);

        }
        public void Bind() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID); }
        public void Unbind() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); }
        public void Delete() { GL.DeleteBuffer(ID); }
    }
}
