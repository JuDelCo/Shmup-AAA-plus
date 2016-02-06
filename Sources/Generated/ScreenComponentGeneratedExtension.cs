using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ScreenComponent screen { get { return (ScreenComponent)GetComponent(ComponentIds.Screen); } }

        public bool hasScreen { get { return HasComponent(ComponentIds.Screen); } }

        static readonly Stack<ScreenComponent> _screenComponentPool = new Stack<ScreenComponent>();

        public static void ClearScreenComponentPool() {
            _screenComponentPool.Clear();
        }

        public Entity AddScreen(UnityEngine.Vector2 newSize) {
            var component = _screenComponentPool.Count > 0 ? _screenComponentPool.Pop() : new ScreenComponent();
            component.size = newSize;
            return AddComponent(ComponentIds.Screen, component);
        }

        public Entity ReplaceScreen(UnityEngine.Vector2 newSize) {
            var previousComponent = hasScreen ? screen : null;
            var component = _screenComponentPool.Count > 0 ? _screenComponentPool.Pop() : new ScreenComponent();
            component.size = newSize;
            ReplaceComponent(ComponentIds.Screen, component);
            if (previousComponent != null) {
                _screenComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveScreen() {
            var component = screen;
            RemoveComponent(ComponentIds.Screen);
            _screenComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity screenEntity { get { return GetGroup(Matcher.Screen).GetSingleEntity(); } }

        public ScreenComponent screen { get { return screenEntity.screen; } }

        public bool hasScreen { get { return screenEntity != null; } }

        public Entity SetScreen(UnityEngine.Vector2 newSize) {
            if (hasScreen) {
                throw new SingleEntityException(Matcher.Screen);
            }
            var entity = CreateEntity();
            entity.AddScreen(newSize);
            return entity;
        }

        public Entity ReplaceScreen(UnityEngine.Vector2 newSize) {
            var entity = screenEntity;
            if (entity == null) {
                entity = SetScreen(newSize);
            } else {
                entity.ReplaceScreen(newSize);
            }

            return entity;
        }

        public void RemoveScreen() {
            DestroyEntity(screenEntity);
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherScreen;

        public static AllOfMatcher Screen {
            get {
                if (_matcherScreen == null) {
                    _matcherScreen = new Matcher(ComponentIds.Screen);
                }

                return _matcherScreen;
            }
        }
    }
}
