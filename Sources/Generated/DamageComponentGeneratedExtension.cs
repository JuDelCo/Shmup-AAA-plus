using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DamageComponent damage { get { return (DamageComponent)GetComponent(ComponentIds.Damage); } }

        public bool hasDamage { get { return HasComponent(ComponentIds.Damage); } }

        static readonly Stack<DamageComponent> _damageComponentPool = new Stack<DamageComponent>();

        public static void ClearDamageComponentPool() {
            _damageComponentPool.Clear();
        }

        public Entity AddDamage(int newValue) {
            var component = _damageComponentPool.Count > 0 ? _damageComponentPool.Pop() : new DamageComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.Damage, component);
        }

        public Entity ReplaceDamage(int newValue) {
            var previousComponent = hasDamage ? damage : null;
            var component = _damageComponentPool.Count > 0 ? _damageComponentPool.Pop() : new DamageComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.Damage, component);
            if (previousComponent != null) {
                _damageComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDamage() {
            var component = damage;
            RemoveComponent(ComponentIds.Damage);
            _damageComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherDamage;

        public static AllOfMatcher Damage {
            get {
                if (_matcherDamage == null) {
                    _matcherDamage = new Matcher(ComponentIds.Damage);
                }

                return _matcherDamage;
            }
        }
    }
}
