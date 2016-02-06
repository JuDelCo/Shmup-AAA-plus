using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MapSpeedComponent mapSpeed { get { return (MapSpeedComponent)GetComponent(ComponentIds.MapSpeed); } }

        public bool hasMapSpeed { get { return HasComponent(ComponentIds.MapSpeed); } }

        static readonly Stack<MapSpeedComponent> _mapSpeedComponentPool = new Stack<MapSpeedComponent>();

        public static void ClearMapSpeedComponentPool() {
            _mapSpeedComponentPool.Clear();
        }

        public Entity AddMapSpeed(UnityEngine.Vector2 newValue) {
            var component = _mapSpeedComponentPool.Count > 0 ? _mapSpeedComponentPool.Pop() : new MapSpeedComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.MapSpeed, component);
        }

        public Entity ReplaceMapSpeed(UnityEngine.Vector2 newValue) {
            var previousComponent = hasMapSpeed ? mapSpeed : null;
            var component = _mapSpeedComponentPool.Count > 0 ? _mapSpeedComponentPool.Pop() : new MapSpeedComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.MapSpeed, component);
            if (previousComponent != null) {
                _mapSpeedComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMapSpeed() {
            var component = mapSpeed;
            RemoveComponent(ComponentIds.MapSpeed);
            _mapSpeedComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity mapSpeedEntity { get { return GetGroup(Matcher.MapSpeed).GetSingleEntity(); } }

        public MapSpeedComponent mapSpeed { get { return mapSpeedEntity.mapSpeed; } }

        public bool hasMapSpeed { get { return mapSpeedEntity != null; } }

        public Entity SetMapSpeed(UnityEngine.Vector2 newValue) {
            if (hasMapSpeed) {
                throw new SingleEntityException(Matcher.MapSpeed);
            }
            var entity = CreateEntity();
            entity.AddMapSpeed(newValue);
            return entity;
        }

        public Entity ReplaceMapSpeed(UnityEngine.Vector2 newValue) {
            var entity = mapSpeedEntity;
            if (entity == null) {
                entity = SetMapSpeed(newValue);
            } else {
                entity.ReplaceMapSpeed(newValue);
            }

            return entity;
        }

        public void RemoveMapSpeed() {
            DestroyEntity(mapSpeedEntity);
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherMapSpeed;

        public static AllOfMatcher MapSpeed {
            get {
                if (_matcherMapSpeed == null) {
                    _matcherMapSpeed = new Matcher(ComponentIds.MapSpeed);
                }

                return _matcherMapSpeed;
            }
        }
    }
}
