using System.Collections.Generic;
using System.Linq;
using Systems;
using Systems.Common_Systems;
using Systems.Eating_Systems;
using Systems.Find_Food_Systems;
using Components;
using Components.Common_Components;
using Components.Person_Components;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;
using Voody.UniLeo;

namespace Services 
{
    [RequireComponent(typeof(ConfigChanger))]
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Configs _defaultConfigs;
        private Configs _currentConfigs;
        private EcsWorld _world;
        private EcsSystems _systems;
        private bool _isPaused;
        private FoodAndPersonPools _pools;

        private ConfigChanger _changer;
        void Awake ()
        {
            _changer = GetComponent<ConfigChanger>();
            _changer.DefaultConfigs = _defaultConfigs;

            _currentConfigs = Instantiate(_defaultConfigs);
            _changer.CurrentConfigs = _currentConfigs;

            _pools = new FoodAndPersonPools();
            
            _world = new EcsWorld ();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            
        }


        public void StartSimulation()
        {
            if (_systems.GetAllSystems().Count == 0)
            {
                InitSystems();
                _isPaused = false;
            }
        }

        public void ClearSimulation()
        {
            EcsEntity[] entities = new EcsEntity[100];
            _world.GetAllEntities(ref entities);
            if (!entities[0].IsNull())
            {
                foreach (var ecsEntity in entities)
                {
                    if (ecsEntity.Has<ViewComponent>())
                    {
                        if (ecsEntity.Has<PersonFoodComponent>())
                        {
                            _pools.PersonPool.Return(ecsEntity.Get<ViewComponent>().View);
                        }
                        else
                        {
                            _pools.FoodPool.Return(ecsEntity.Get<ViewComponent>().View);
                        }
                        ecsEntity.Get<ViewComponent>().View.SetActive(false);
                    }
                    ecsEntity.Destroy();
                }
            }
            _systems = new EcsSystems(_world);
        }
        
        private void InitSystems()
        {
            _systems = new EcsSystems (_world);
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
                .Inject(_currentConfigs)
                .Inject(_pools)
                .Init();
        }

        void Update () 
        {
            if(!_isPaused && _systems.GetRunSystems().Count > 0) _systems?.Run();
        }

        public void ChangeGameState()
        {
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                EcsEntity[] entities = new EcsEntity[100];
                _world.GetAllEntities(ref entities);
                if (!entities[0].IsNull())
                {
                    entities = entities.Where(x => x.Has<MoveComponent>()).ToArray();
                    foreach (var ecsEntity in entities)
                    {
                        ecsEntity.Get<MoveComponent>().Rigidbody.velocity = Vector3.zero;
                    }
                }
            }
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