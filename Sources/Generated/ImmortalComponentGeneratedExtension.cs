using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ImmortalComponent immortal { get { return (ImmortalComponent)GetComponent(ComponentIds.Immortal); } }

        public bool hasImmortal { get { return HasComponent(ComponentIds.Immortal); } }

        static readonly Stack<ImmortalComponent> _immortalComponentPool = new Stack<ImmortalComponent>();

        public static void ClearImmortalComponentPool() {
            _immortalComponentPool.Clear();
        }

        public Entity AddImmortal(float newUntilTime) {
            var component = _immortalComponentPool.Count > 0 ? _immortalComponentPool.Pop() : new ImmortalComponent();
            component.untilTime = newUntilTime;
            return AddComponent(ComponentIds.Immortal, component);
        }

        public Entity ReplaceImmortal(float newUntilTime) {
            var previousComponent = hasImmortal ? immortal : null;
            var component = _immortalComponentPool.Count > 0 ? _immortalComponentPool.Pop() : new ImmortalComponent();
            component.untilTime = newUntilTime;
            ReplaceComponent(ComponentIds.Immortal, component);
            if (previousComponent != null) {
                _immortalComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveImmortal() {
            var component = immortal;
            RemoveComponent(ComponentIds.Immortal);
            _immortalComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherImmortal;

        public static AllOfMatcher Immortal {
            get {
                if (_matcherImmortal == null) {
                    _matcherImmortal = new Matcher(ComponentIds.Immortal);
                }

                return _matcherImmortal;
            }
        }
    }
}
