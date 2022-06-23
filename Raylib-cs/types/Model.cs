using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Raylib_cs
{
    /// <summary>
    /// Bone information
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct BoneInfo
    {
        /// <summary>
        /// Bone name (char[32])
        /// </summary>
        public fixed sbyte Name[32];

        /// <summary>
        /// Bone parent
        /// </summary>
        public int Parent;
    }

    /// <summary>
    /// Model type
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct Model
    {
        /// <summary>
        /// Local transform matrix
        /// </summary>
        public Matrix4x4 Transform;

        /// <summary>
        /// Number of meshes
        /// </summary>
        public int MeshCount;

        /// <summary>
        /// Number of materials
        /// </summary>
        public int MaterialCount;

        /// <summary>
        /// Meshes array (Mesh *)
        /// </summary>
        public Mesh* Meshes;

        /// <summary>
        /// Materials array (Material *)
        /// </summary>
        public Material* Materials;

        /// <summary>
        /// Mesh material number (int *)
        /// </summary>
        public int* MeshMaterial;

        /// <summary>
        /// Number of bones
        /// </summary>
        public int BoneCount;

        //TODO: Span
        /// <summary>
        /// Bones information (skeleton, BoneInfo *)
        /// </summary>
        public BoneInfo* Bones;

        //TODO: Span
        /// <summary>
        /// Bones base transformation (pose, Transform *)
        /// </summary>
        public Transform* BindPose;
    }

    /// <summary>
    /// Model animation
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe partial struct ModelAnimation
    {
        /// <summary>
        /// Number of bones
        /// </summary>
        public readonly int BoneCount;

        /// <summary>
        /// Number of animation frames
        /// </summary>
        public readonly int FrameCount;

        /// <summary>
        /// Bones information (skeleton, BoneInfo *)
        /// </summary>
        public readonly BoneInfo* Bones;

        /// <inheritdoc cref="Bones"/>
        public ReadOnlySpan<BoneInfo> BoneInfo => new(Bones, BoneCount);

        /// <summary>
        /// Poses array by frame (Transform **)
        /// </summary>
        public readonly Transform** framePoses;

        /// <inheritdoc cref="framePoses"/>
        public FramePosesCollection FramePoses => new(framePoses, FrameCount, BoneCount);

        public struct FramePosesCollection
        {
            readonly Transform** framePoses;

            readonly int frameCount;

            readonly int boneCount;

            public FramePoses this[int index] => new(framePoses[index], boneCount);

            public Transform this[int index1, int index2] => new FramePoses(framePoses[index1], boneCount)[index2];

            internal FramePosesCollection(Transform** framePoses, int frameCount, int boneCount)
            {
                this.framePoses = framePoses;
                this.frameCount = frameCount;
                this.boneCount = boneCount;
            }
        }
    }

    public unsafe struct FramePoses
    {
        readonly Transform* Poses;

        readonly int Count;

        public ref Transform this[int index]
        {
            get
            {
                return ref Poses[index];
            }
        }

        internal FramePoses(Transform* poses, int count)
        {
            this.Poses = poses;
            this.Count = count;
        }
    }
}
