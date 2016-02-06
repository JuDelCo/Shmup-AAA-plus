using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ScreenLimitedDestroy screenLimitedDestroy { get { return (ScreenLimitedDestroy)GetComponent(ComponentIds.ScreenLimitedDestroy); } }

        public bool hasScreenLimitedDestroy { get { return HasComponent(ComponentIds.ScreenLimitedDestroy); } }

        static readonly Stack<ScreenLimitedDestroy> _screenLimitedDestroyComponentPool = new Stack<ScreenLimitedDestroy>();

        public static void ClearScreenLimitedDestroyComponentPool() {
            _screenLimitedDestroyComponentPool.Clear();
        }

        public Entity AddScreenLimitedDestroy(UnityEngine.Vector4 newOffset) {
            var component = _screenLimitedDestroyComponentPool.Count > 0 ? _screenLimitedDestroyComponentPool.Pop() : new ScreenLimitedDestroy();
            component.offset = newOffset;
            return AddComponent(ComponentIds.ScreenLimitedDestroy, component);
        }

        public Entity ReplaceScreenLimitedDestroy(UnityEngine.Vector4 newOffset) {
            var previousComponent = hasScreenLimitedDestroy ? screenLimitedDestroy : null;
            var component = _screenLimitedDestroyComponentPool.Count > 0 ? _screenLimitedDestroyComponentPool.Pop() : new ScreenLimitedDestroy();
            component.offset = newOffset;
            ReplaceComponent(ComponentIds.ScreenLimitedDestroy, component);
            if (previousComponent != null) {
                _screenLimitedDestroyComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveScreenLimitedDestroy() {
            var component = screenLimitedDestroy;
            RemoveComponent(ComponentIds.ScreenLimitedDestroy);
            _screenLimitedDestroyComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherScreenLimitedDestroy;

        public static AllOfMatcher ScreenLimitedDestroy {
            get {
                if (_matcherScreenLimitedDestroy == null) {
                    _matcherScreenLimitedDestroy = new Matcher(ComponentIds.ScreenLimitedDestroy);
                }

                return _matcherScreenLimitedDestroy;
            }
        }
    }
}
