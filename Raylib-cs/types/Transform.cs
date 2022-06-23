using System.Numerics;
using System.Runtime.InteropServices;

namespace Raylib_cs
{
    /// <summary>
    /// Transform, vectex transformation data
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Transform
    {
        /// <summary>
        /// Translation
        /// </summary>
        public Vector3 Translation;

        /// <summary>
        /// Rotation
        /// </summary>
        public Vector4 Rotation;

        /// <summary>
        /// Scale
        /// </summary>
        public Vector3 Scale;
    }
}
