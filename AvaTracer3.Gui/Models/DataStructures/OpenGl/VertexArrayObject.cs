using System;
using Silk.NET.OpenGL;

namespace AvaTracer3.Gui.Models.DataStructures.OpenGl;

public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
    where TVertexType : unmanaged
    where TIndexType : unmanaged
{
    private readonly uint m_handle;
    private readonly GL   m_gl;

    public VertexArrayObject(GL p_gl, BufferObject<TVertexType> p_vbo, BufferObject<TIndexType> p_ebo)
    {
        m_gl = p_gl;

        m_handle = m_gl.GenVertexArray();
        Bind();
        p_vbo.Bind();
        p_ebo.Bind();
    }

    public unsafe void VertexAttributePointer(uint p_index, int p_count, VertexAttribPointerType p_type, uint p_vertexSize, int p_offSet)
    {
        m_gl.VertexAttribPointer(p_index, p_count, p_type, false, p_vertexSize * (uint) sizeof(TVertexType), (void*) (p_offSet * sizeof(TVertexType)));
        m_gl.EnableVertexAttribArray(p_index);
    }

    public void Bind()
    {
        m_gl.BindVertexArray(m_handle);
    }

    public void Dispose()
    {
        m_gl.DeleteVertexArray(m_handle);
    }
}