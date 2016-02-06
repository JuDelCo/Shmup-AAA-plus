using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CoroutineWaitComponent coroutineWait { get { return (CoroutineWaitComponent)GetComponent(ComponentIds.CoroutineWait); } }

        public bool hasCoroutineWait { get { return HasComponent(ComponentIds.CoroutineWait); } }

        static readonly Stack<CoroutineWaitComponent> _coroutineWaitComponentPool = new Stack<CoroutineWaitComponent>();

        public static void ClearCoroutineWaitComponentPool() {
            _coroutineWaitComponentPool.Clear();
        }

        public Entity AddCoroutineWait(float newTime) {
            var component = _coroutineWaitComponentPool.Count > 0 ? _coroutineWaitComponentPool.Pop() : new CoroutineWaitComponent();
            component.time = newTime;
            return AddComponent(ComponentIds.CoroutineWait, component);
        }

        public Entity ReplaceCoroutineWait(float newTime) {
            var previousComponent = hasCoroutineWait ? coroutineWait : null;
            var component = _coroutineWaitComponentPool.Count > 0 ? _coroutineWaitComponentPool.Pop() : new CoroutineWaitComponent();
            component.time = newTime;
            ReplaceComponent(ComponentIds.CoroutineWait, component);
            if (previousComponent != null) {
                _coroutineWaitComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCoroutineWait() {
            var component = coroutineWait;
            RemoveComponent(ComponentIds.CoroutineWait);
            _coroutineWaitComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCoroutineWait;

        public static AllOfMatcher CoroutineWait {
            get {
                if (_matcherCoroutineWait == null) {
                    _matcherCoroutineWait = new Matcher(ComponentIds.CoroutineWait);
                }

                return _matcherCoroutineWait;
            }
        }
    }
}
