using System;
using System.Runtime.InteropServices;
using Silk.NET.OpenGL;

namespace AvaTracer3.Gui.Models.DataStructures.OpenGl;

public class BufferObject<TDataType> : IDisposable
    where TDataType : unmanaged
{
    private uint            m_handle;
    private BufferTargetARB m_bufferType;
    private GL              m_gl;

    public unsafe BufferObject(GL p_gl, Span<TDataType> p_data, BufferTargetARB p_bufferType)
    {
        m_gl         = p_gl;
        m_bufferType = p_bufferType;

        m_handle = m_gl.GenBuffer();
        Bind();
        fixed (void* d = p_data)
        {
            m_gl.BufferData(p_bufferType, (nuint)(p_data.Length * Marshal.SizeOf<TDataType>()), d,
                           BufferUsageARB.StaticDraw);
        }
    }

    public void Bind()
    {
        m_gl.BindBuffer(m_bufferType, m_handle);
    }

    public void Dispose()
    {
        m_gl.DeleteBuffer(m_handle);
    }
}