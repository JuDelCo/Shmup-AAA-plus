using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public FlashComponent flash { get { return (FlashComponent)GetComponent(ComponentIds.Flash); } }

        public bool hasFlash { get { return HasComponent(ComponentIds.Flash); } }

        static readonly Stack<FlashComponent> _flashComponentPool = new Stack<FlashComponent>();

        public static void ClearFlashComponentPool() {
            _flashComponentPool.Clear();
        }

        public Entity AddFlash(float newUntilTime) {
            var component = _flashComponentPool.Count > 0 ? _flashComponentPool.Pop() : new FlashComponent();
            component.untilTime = newUntilTime;
            return AddComponent(ComponentIds.Flash, component);
        }

        public Entity ReplaceFlash(float newUntilTime) {
            var previousComponent = hasFlash ? flash : null;
            var component = _flashComponentPool.Count > 0 ? _flashComponentPool.Pop() : new FlashComponent();
            component.untilTime = newUntilTime;
            ReplaceComponent(ComponentIds.Flash, component);
            if (previousComponent != null) {
                _flashComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveFlash() {
            var component = flash;
            RemoveComponent(ComponentIds.Flash);
            _flashComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherFlash;

        public static AllOfMatcher Flash {
            get {
                if (_matcherFlash == null) {
                    _matcherFlash = new Matcher(ComponentIds.Flash);
                }

                return _matcherFlash;
            }
        }
    }
}
