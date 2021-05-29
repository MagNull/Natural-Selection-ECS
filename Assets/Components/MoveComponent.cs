using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct MoveComponent
    {
        public Vector3 Direction;
        public float Speed;
        public Rigidbody Rigidbody;
    }
}