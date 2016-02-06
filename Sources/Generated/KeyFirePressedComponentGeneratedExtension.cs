namespace Entitas {
    public partial class Entity {
        static readonly KeyFirePressedComponent keyFirePressedComponent = new KeyFirePressedComponent();

        public bool isKeyFirePressed {
            get { return HasComponent(ComponentIds.KeyFirePressed); }
            set {
                if (value != isKeyFirePressed) {
                    if (value) {
                        AddComponent(ComponentIds.KeyFirePressed, keyFirePressedComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeyFirePressed);
                    }
                }
            }
        }

        public Entity IsKeyFirePressed(bool value) {
            isKeyFirePressed = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity keyFirePressedEntity { get { return GetGroup(Matcher.KeyFirePressed).GetSingleEntity(); } }

        public bool isKeyFirePressed {
            get { return keyFirePressedEntity != null; }
            set {
                var entity = keyFirePressedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isKeyFirePressed = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeyFirePressed;

        public static AllOfMatcher KeyFirePressed {
            get {
                if (_matcherKeyFirePressed == null) {
                    _matcherKeyFirePressed = new Matcher(ComponentIds.KeyFirePressed);
                }

                return _matcherKeyFirePressed;
            }
        }
    }
}
