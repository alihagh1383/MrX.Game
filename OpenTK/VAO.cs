﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace MrX.Game.Graphics
{
    internal class VAO
    {
        public int ID;
        public VAO()
        {
            ID = GL.GenVertexArray();
        }
        public void LinkToVAO(int location, int size, VBO vbo)
        {
            Bind();
            vbo.Bind();
            GL.VertexAttribPointer(location, size, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(location);
        }
        public void Bind() { GL.BindVertexArray(ID); }
        public void Unbind() { GL.BindVertexArray(0); }
        public void Delete() { GL.DeleteVertexArray(ID); }
    }
}
