using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PositionComponent position { get { return (PositionComponent)GetComponent(ComponentIds.Position); } }

        public bool hasPosition { get { return HasComponent(ComponentIds.Position); } }

        static readonly Stack<PositionComponent> _positionComponentPool = new Stack<PositionComponent>();

        public static void ClearPositionComponentPool() {
            _positionComponentPool.Clear();
        }

        public Entity AddPosition(float newX, float newY) {
            var component = _positionComponentPool.Count > 0 ? _positionComponentPool.Pop() : new PositionComponent();
            component.x = newX;
            component.y = newY;
            return AddComponent(ComponentIds.Position, component);
        }

        public Entity ReplacePosition(float newX, float newY) {
            var previousComponent = hasPosition ? position : null;
            var component = _positionComponentPool.Count > 0 ? _positionComponentPool.Pop() : new PositionComponent();
            component.x = newX;
            component.y = newY;
            ReplaceComponent(ComponentIds.Position, component);
            if (previousComponent != null) {
                _positionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePosition() {
            var component = position;
            RemoveComponent(ComponentIds.Position);
            _positionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherPosition;

        public static AllOfMatcher Position {
            get {
                if (_matcherPosition == null) {
                    _matcherPosition = new Matcher(ComponentIds.Position);
                }

                return _matcherPosition;
            }
        }
    }
}
