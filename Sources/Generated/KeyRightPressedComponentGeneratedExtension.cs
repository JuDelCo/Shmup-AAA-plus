namespace Entitas {
    public partial class Entity {
        static readonly KeyRightPressedComponent keyRightPressedComponent = new KeyRightPressedComponent();

        public bool isKeyRightPressed {
            get { return HasComponent(ComponentIds.KeyRightPressed); }
            set {
                if (value != isKeyRightPressed) {
                    if (value) {
                        AddComponent(ComponentIds.KeyRightPressed, keyRightPressedComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeyRightPressed);
                    }
                }
            }
        }

        public Entity IsKeyRightPressed(bool value) {
            isKeyRightPressed = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity keyRightPressedEntity { get { return GetGroup(Matcher.KeyRightPressed).GetSingleEntity(); } }

        public bool isKeyRightPressed {
            get { return keyRightPressedEntity != null; }
            set {
                var entity = keyRightPressedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isKeyRightPressed = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeyRightPressed;

        public static AllOfMatcher KeyRightPressed {
            get {
                if (_matcherKeyRightPressed == null) {
                    _matcherKeyRightPressed = new Matcher(ComponentIds.KeyRightPressed);
                }

                return _matcherKeyRightPressed;
            }
        }
    }
}
