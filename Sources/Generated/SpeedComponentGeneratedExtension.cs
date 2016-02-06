using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SpeedComponent speed { get { return (SpeedComponent)GetComponent(ComponentIds.Speed); } }

        public bool hasSpeed { get { return HasComponent(ComponentIds.Speed); } }

        static readonly Stack<SpeedComponent> _speedComponentPool = new Stack<SpeedComponent>();

        public static void ClearSpeedComponentPool() {
            _speedComponentPool.Clear();
        }

        public Entity AddSpeed(UnityEngine.Vector2 newValue) {
            var component = _speedComponentPool.Count > 0 ? _speedComponentPool.Pop() : new SpeedComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.Speed, component);
        }

        public Entity ReplaceSpeed(UnityEngine.Vector2 newValue) {
            var previousComponent = hasSpeed ? speed : null;
            var component = _speedComponentPool.Count > 0 ? _speedComponentPool.Pop() : new SpeedComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.Speed, component);
            if (previousComponent != null) {
                _speedComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSpeed() {
            var component = speed;
            RemoveComponent(ComponentIds.Speed);
            _speedComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherSpeed;

        public static AllOfMatcher Speed {
            get {
                if (_matcherSpeed == null) {
                    _matcherSpeed = new Matcher(ComponentIds.Speed);
                }

                return _matcherSpeed;
            }
        }
    }
}
