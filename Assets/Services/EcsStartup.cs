using Systems;
using Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Services {
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Configs _configs;
        private EcsWorld _world;
        private EcsSystems _systems;

        void Awake ()
        {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            
            _systems
                .ConvertScene()
                .Add(new PersonsSpawnSystem())
                .Add(new MoveSystem())
                .Add(new PersonEatPlantSystem())
                .Add(new PredatorEatPersonSystem())
                .Add(new ReplicateSystem())
                .Add(new MutationSystem())
                .Add(new FromPlantToMeatChangeSystem())
                .Add(new LookAtMovementSystem())
                .Add(new FindHerbivoreFoodSystem())
                .Add(new FindPredatorFoodSystem())
                .Add(new FindHybridFoodSystem())
                .Add(new FoodSpawnSystem())
                .Add(new AgingSystem())
                .Add(new TimerSystem())
                .Add(new DestroySystem())
                .Inject(_configs)
                .Inject(new FoodAndPersonPools())
                .Init ();
        }

        void Update () 
        {
            _systems?.Run();
        }

        void OnDestroy () 
        {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}