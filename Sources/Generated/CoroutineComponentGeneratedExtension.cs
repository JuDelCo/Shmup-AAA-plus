using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CoroutineComponent coroutine { get { return (CoroutineComponent)GetComponent(ComponentIds.Coroutine); } }

        public bool hasCoroutine { get { return HasComponent(ComponentIds.Coroutine); } }

        static readonly Stack<CoroutineComponent> _coroutineComponentPool = new Stack<CoroutineComponent>();

        public static void ClearCoroutineComponentPool() {
            _coroutineComponentPool.Clear();
        }

        public Entity AddCoroutine(System.Collections.IEnumerator newValue) {
            var component = _coroutineComponentPool.Count > 0 ? _coroutineComponentPool.Pop() : new CoroutineComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.Coroutine, component);
        }

        public Entity ReplaceCoroutine(System.Collections.IEnumerator newValue) {
            var previousComponent = hasCoroutine ? coroutine : null;
            var component = _coroutineComponentPool.Count > 0 ? _coroutineComponentPool.Pop() : new CoroutineComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.Coroutine, component);
            if (previousComponent != null) {
                _coroutineComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCoroutine() {
            var component = coroutine;
            RemoveComponent(ComponentIds.Coroutine);
            _coroutineComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCoroutine;

        public static AllOfMatcher Coroutine {
            get {
                if (_matcherCoroutine == null) {
                    _matcherCoroutine = new Matcher(ComponentIds.Coroutine);
                }

                return _matcherCoroutine;
            }
        }
    }
}
