using Components.Common_Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace UnityComponents
{
    [RequireComponent(typeof(Rigidbody), typeof(LinkedEntity))]
    public class CollusionEmmiter : MonoBehaviour
    {
        private EcsWorld _world;

        private void Start()
        {
            _world = WorldHandler.GetWorld();
        }

        // private void OnCollisionEnter(Collision other)
        // {
        //     if (other.gameObject.TryGetComponent(out LinkedEntity linkedEntity))
        //     {
        //         EcsEntity entity = _world.NewEntity();
        //
        //         entity.Replace(new CollusionComponent
        //         {
        //             Entity1 = GetComponent<LinkedEntity>().Entity,
        //             Entity2 = linkedEntity.Entity
        //         });
        //     }
        // }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out LinkedEntity linkedEntity))
            {
                EcsEntity entity = _world.NewEntity();
    
                entity.Replace(new CollusionComponent
                {
                    Entity1 = GetComponent<LinkedEntity>().Entity,
                    Entity2 = linkedEntity.Entity
                });
            }
        }
    }
}
