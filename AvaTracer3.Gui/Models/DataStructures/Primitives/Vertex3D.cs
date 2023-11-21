using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace AvaTracer3.Gui.Models.DataStructures.Primitives;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct Vertex3D
{
    public Vertex3D(Vector3 p_worldPosition, 
                    Color4 p_color)
    {
        m_worldPosition = p_worldPosition;
        m_color    = p_color;
    }
    
    private readonly Vector3 m_worldPosition;
    private readonly Color4  m_color;
}