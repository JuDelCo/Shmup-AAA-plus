using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ScreenLimitedPosition screenLimitedPosition { get { return (ScreenLimitedPosition)GetComponent(ComponentIds.ScreenLimitedPosition); } }

        public bool hasScreenLimitedPosition { get { return HasComponent(ComponentIds.ScreenLimitedPosition); } }

        static readonly Stack<ScreenLimitedPosition> _screenLimitedPositionComponentPool = new Stack<ScreenLimitedPosition>();

        public static void ClearScreenLimitedPositionComponentPool() {
            _screenLimitedPositionComponentPool.Clear();
        }

        public Entity AddScreenLimitedPosition(UnityEngine.Vector4 newOffset) {
            var component = _screenLimitedPositionComponentPool.Count > 0 ? _screenLimitedPositionComponentPool.Pop() : new ScreenLimitedPosition();
            component.offset = newOffset;
            return AddComponent(ComponentIds.ScreenLimitedPosition, component);
        }

        public Entity ReplaceScreenLimitedPosition(UnityEngine.Vector4 newOffset) {
            var previousComponent = hasScreenLimitedPosition ? screenLimitedPosition : null;
            var component = _screenLimitedPositionComponentPool.Count > 0 ? _screenLimitedPositionComponentPool.Pop() : new ScreenLimitedPosition();
            component.offset = newOffset;
            ReplaceComponent(ComponentIds.ScreenLimitedPosition, component);
            if (previousComponent != null) {
                _screenLimitedPositionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveScreenLimitedPosition() {
            var component = screenLimitedPosition;
            RemoveComponent(ComponentIds.ScreenLimitedPosition);
            _screenLimitedPositionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherScreenLimitedPosition;

        public static AllOfMatcher ScreenLimitedPosition {
            get {
                if (_matcherScreenLimitedPosition == null) {
                    _matcherScreenLimitedPosition = new Matcher(ComponentIds.ScreenLimitedPosition);
                }

                return _matcherScreenLimitedPosition;
            }
        }
    }
}
