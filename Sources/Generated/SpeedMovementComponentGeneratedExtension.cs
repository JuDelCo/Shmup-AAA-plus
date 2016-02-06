using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SpeedMovementComponent speedMovement { get { return (SpeedMovementComponent)GetComponent(ComponentIds.SpeedMovement); } }

        public bool hasSpeedMovement { get { return HasComponent(ComponentIds.SpeedMovement); } }

        static readonly Stack<SpeedMovementComponent> _speedMovementComponentPool = new Stack<SpeedMovementComponent>();

        public static void ClearSpeedMovementComponentPool() {
            _speedMovementComponentPool.Clear();
        }

        public Entity AddSpeedMovement(UnityEngine.Vector2 newValue) {
            var component = _speedMovementComponentPool.Count > 0 ? _speedMovementComponentPool.Pop() : new SpeedMovementComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.SpeedMovement, component);
        }

        public Entity ReplaceSpeedMovement(UnityEngine.Vector2 newValue) {
            var previousComponent = hasSpeedMovement ? speedMovement : null;
            var component = _speedMovementComponentPool.Count > 0 ? _speedMovementComponentPool.Pop() : new SpeedMovementComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.SpeedMovement, component);
            if (previousComponent != null) {
                _speedMovementComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSpeedMovement() {
            var component = speedMovement;
            RemoveComponent(ComponentIds.SpeedMovement);
            _speedMovementComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherSpeedMovement;

        public static AllOfMatcher SpeedMovement {
            get {
                if (_matcherSpeedMovement == null) {
                    _matcherSpeedMovement = new Matcher(ComponentIds.SpeedMovement);
                }

                return _matcherSpeedMovement;
            }
        }
    }
}
