using System;
using System.Drawing;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using AvaTracer3.Gui.Models.DataStructures.OpenGl;
using Silk.NET.OpenGL;
using Shader = AvaTracer3.Gui.Models.DataStructures.OpenGl.Shader;

namespace AvaTracer3.Gui.Views;

public class OpenGlView : OpenGlControlBase
{
    private GL                             m_gl;
    private BufferObject<float>            m_vbo;
    private BufferObject<uint>             m_ebo;
    private VertexArrayObject<float, uint> m_vao;
    private Shader                         m_shader;

    private static readonly float[] Vertices =
    {
        //X    Y      Z     R  G  B  A
        0.5f, 0.5f, 0.0f, 1, 0, 0, 1,
        0.5f, -0.5f, 0.0f, 0, 0, 0, 1,
        -0.5f, -0.5f, 0.0f, 0, 0, 1, 1,
        -0.5f, 0.5f, 0.5f, 0, 0, 0, 1
    };

    private static readonly uint[] Indices =
    {
        0, 1, 3,
        1, 2, 3
    };

    protected override void OnOpenGlInit(GlInterface p_gl)
    {
        base.OnOpenGlInit(p_gl);

        m_gl = GL.GetApi(p_gl.GetProcAddress);

        //Instantiating our new abstractions
        m_ebo = new BufferObject<uint>(m_gl, Indices, BufferTargetARB.ElementArrayBuffer);
        m_vbo = new BufferObject<float>(m_gl, Vertices, BufferTargetARB.ArrayBuffer);
        m_vao = new VertexArrayObject<float, uint>(m_gl, m_vbo, m_ebo);

        //Telling the VAO object how to lay out the attribute pointers
        m_vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
        m_vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);

        m_shader = new Shader(m_gl, "shader.vert", "shader.frag");
    }

    protected override void OnOpenGlDeinit(GlInterface p_gl)
    {
        m_vbo.Dispose();
        m_ebo.Dispose();
        m_vao.Dispose();
        m_shader.Dispose();
        base.OnOpenGlDeinit(p_gl);
    }

    protected override unsafe void OnOpenGlRender(GlInterface p_gl, int p_fb)
    {
        m_gl.ClearColor(Color.Firebrick);
        m_gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
        m_gl.Enable(EnableCap.DepthTest);
        m_gl.Viewport(0, 0, (uint)Bounds.Width, (uint)Bounds.Height);

        m_ebo.Bind();
        m_vbo.Bind();
        m_vao.Bind();
        m_shader.Use();
        m_shader.SetUniform("uBlue", (float)Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));

        m_gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
        Dispatcher.UIThread.Post(RequestNextFrameRendering, DispatcherPriority.Background);
    }
}