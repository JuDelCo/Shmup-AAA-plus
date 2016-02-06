using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CollisionDamageComponent collisionDamage { get { return (CollisionDamageComponent)GetComponent(ComponentIds.CollisionDamage); } }

        public bool hasCollisionDamage { get { return HasComponent(ComponentIds.CollisionDamage); } }

        static readonly Stack<CollisionDamageComponent> _collisionDamageComponentPool = new Stack<CollisionDamageComponent>();

        public static void ClearCollisionDamageComponentPool() {
            _collisionDamageComponentPool.Clear();
        }

        public Entity AddCollisionDamage(int newValue) {
            var component = _collisionDamageComponentPool.Count > 0 ? _collisionDamageComponentPool.Pop() : new CollisionDamageComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.CollisionDamage, component);
        }

        public Entity ReplaceCollisionDamage(int newValue) {
            var previousComponent = hasCollisionDamage ? collisionDamage : null;
            var component = _collisionDamageComponentPool.Count > 0 ? _collisionDamageComponentPool.Pop() : new CollisionDamageComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.CollisionDamage, component);
            if (previousComponent != null) {
                _collisionDamageComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCollisionDamage() {
            var component = collisionDamage;
            RemoveComponent(ComponentIds.CollisionDamage);
            _collisionDamageComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCollisionDamage;

        public static AllOfMatcher CollisionDamage {
            get {
                if (_matcherCollisionDamage == null) {
                    _matcherCollisionDamage = new Matcher(ComponentIds.CollisionDamage);
                }

                return _matcherCollisionDamage;
            }
        }
    }
}
