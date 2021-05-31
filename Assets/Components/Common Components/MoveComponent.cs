using System;
using UnityEngine;

namespace Components.Common_Components
{
    [Serializable]
    public struct MoveComponent
    {
        public Vector3 Direction;
        public float Speed;
        public Rigidbody Rigidbody;
    }
}