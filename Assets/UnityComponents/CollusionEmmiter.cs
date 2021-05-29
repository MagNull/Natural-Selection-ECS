using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;
using Voody.UniLeo;

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
