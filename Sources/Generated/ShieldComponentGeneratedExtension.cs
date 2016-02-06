using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ShieldComponent shield { get { return (ShieldComponent)GetComponent(ComponentIds.Shield); } }

        public bool hasShield { get { return HasComponent(ComponentIds.Shield); } }

        static readonly Stack<ShieldComponent> _shieldComponentPool = new Stack<ShieldComponent>();

        public static void ClearShieldComponentPool() {
            _shieldComponentPool.Clear();
        }

        public Entity AddShield(Entitas.Entity newOwner) {
            var component = _shieldComponentPool.Count > 0 ? _shieldComponentPool.Pop() : new ShieldComponent();
            component.owner = newOwner;
            return AddComponent(ComponentIds.Shield, component);
        }

        public Entity ReplaceShield(Entitas.Entity newOwner) {
            var previousComponent = hasShield ? shield : null;
            var component = _shieldComponentPool.Count > 0 ? _shieldComponentPool.Pop() : new ShieldComponent();
            component.owner = newOwner;
            ReplaceComponent(ComponentIds.Shield, component);
            if (previousComponent != null) {
                _shieldComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveShield() {
            var component = shield;
            RemoveComponent(ComponentIds.Shield);
            _shieldComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherShield;

        public static AllOfMatcher Shield {
            get {
                if (_matcherShield == null) {
                    _matcherShield = new Matcher(ComponentIds.Shield);
                }

                return _matcherShield;
            }
        }
    }
}
