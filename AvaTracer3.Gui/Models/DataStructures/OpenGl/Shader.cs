using System;
using System.IO;
using Silk.NET.OpenGL;

namespace AvaTracer3.Gui.Models.DataStructures.OpenGl;

public class Shader : IDisposable
{
    private readonly uint m_handle;
    private readonly GL   m_gl;

    public Shader(GL p_gl, string p_vertexPath, string p_fragmentPath)
    {
        m_gl = p_gl;

        var vertex   = LoadShader(ShaderType.VertexShader, p_vertexPath);
        var fragment = LoadShader(ShaderType.FragmentShader, p_fragmentPath);
        m_handle = m_gl.CreateProgram();
        m_gl.AttachShader(m_handle, vertex);
        m_gl.AttachShader(m_handle, fragment);
        m_gl.LinkProgram(m_handle);
        m_gl.GetProgram(m_handle, GLEnum.LinkStatus, out var status);
        
        if (status == 0)
        {
            throw new Exception($"Program failed to link with error: {m_gl.GetProgramInfoLog(m_handle)}");
        }

        m_gl.DetachShader(m_handle, vertex);
        m_gl.DetachShader(m_handle, fragment);
        m_gl.DeleteShader(vertex);
        m_gl.DeleteShader(fragment);
    }

    public void Use()
    {
        m_gl.UseProgram(m_handle);
    }

    public void SetUniform(string p_name, int p_value)
    {
        var location = m_gl.GetUniformLocation(m_handle, p_name);
        if (location == -1)
        {
            throw new Exception($"{p_name} uniform not found on shader.");
        }

        m_gl.Uniform1(location, p_value);
    }

    public void SetUniform(string p_name, float p_value)
    {
        var location = m_gl.GetUniformLocation(m_handle, p_name);
        if (location == -1)
        {
            throw new Exception($"{p_name} uniform not found on shader.");
        }

        m_gl.Uniform1(location, p_value);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        m_gl.DeleteProgram(m_handle);
    }

    private uint LoadShader(ShaderType p_type, string p_path)
    {
        var src    = File.ReadAllText(p_path);
        var   handle = m_gl.CreateShader(p_type);
        m_gl.ShaderSource(handle, src);
        m_gl.CompileShader(handle);
        var infoLog = m_gl.GetShaderInfoLog(handle);
        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            throw new Exception($"Error compiling shader of type {p_type}, failed with error {infoLog}");
        }

        return handle;
    }
}