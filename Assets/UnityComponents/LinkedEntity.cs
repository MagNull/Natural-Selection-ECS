using System;
using Leopotam.Ecs;
using UnityEngine;

namespace UnityComponents
{
    public class LinkedEntity : MonoBehaviour
    {
        private EcsEntity _entity;
        [SerializeField] private int _id;
        [SerializeField] private bool _linkedEntity;

        private void OnEnable()
        {
            _id = _entity.GetInternalId();
        }

        public EcsEntity Entity => _entity;

        public void Link(EcsEntity entity) => _entity = entity;
    }
}