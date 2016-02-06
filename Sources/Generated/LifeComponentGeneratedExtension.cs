using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public LifeComponent life { get { return (LifeComponent)GetComponent(ComponentIds.Life); } }

        public bool hasLife { get { return HasComponent(ComponentIds.Life); } }

        static readonly Stack<LifeComponent> _lifeComponentPool = new Stack<LifeComponent>();

        public static void ClearLifeComponentPool() {
            _lifeComponentPool.Clear();
        }

        public Entity AddLife(int newValue) {
            var component = _lifeComponentPool.Count > 0 ? _lifeComponentPool.Pop() : new LifeComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.Life, component);
        }

        public Entity ReplaceLife(int newValue) {
            var previousComponent = hasLife ? life : null;
            var component = _lifeComponentPool.Count > 0 ? _lifeComponentPool.Pop() : new LifeComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.Life, component);
            if (previousComponent != null) {
                _lifeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLife() {
            var component = life;
            RemoveComponent(ComponentIds.Life);
            _lifeComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity lifeEntity { get { return GetGroup(Matcher.Life).GetSingleEntity(); } }

        public LifeComponent life { get { return lifeEntity.life; } }

        public bool hasLife { get { return lifeEntity != null; } }

        public Entity SetLife(int newValue) {
            if (hasLife) {
                throw new SingleEntityException(Matcher.Life);
            }
            var entity = CreateEntity();
            entity.AddLife(newValue);
            return entity;
        }

        public Entity ReplaceLife(int newValue) {
            var entity = lifeEntity;
            if (entity == null) {
                entity = SetLife(newValue);
            } else {
                entity.ReplaceLife(newValue);
            }

            return entity;
        }

        public void RemoveLife() {
            DestroyEntity(lifeEntity);
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherLife;

        public static AllOfMatcher Life {
            get {
                if (_matcherLife == null) {
                    _matcherLife = new Matcher(ComponentIds.Life);
                }

                return _matcherLife;
            }
        }
    }
}
