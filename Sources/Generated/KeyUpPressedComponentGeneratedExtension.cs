namespace Entitas {
    public partial class Entity {
        static readonly KeyUpPressedComponent keyUpPressedComponent = new KeyUpPressedComponent();

        public bool isKeyUpPressed {
            get { return HasComponent(ComponentIds.KeyUpPressed); }
            set {
                if (value != isKeyUpPressed) {
                    if (value) {
                        AddComponent(ComponentIds.KeyUpPressed, keyUpPressedComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeyUpPressed);
                    }
                }
            }
        }

        public Entity IsKeyUpPressed(bool value) {
            isKeyUpPressed = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity keyUpPressedEntity { get { return GetGroup(Matcher.KeyUpPressed).GetSingleEntity(); } }

        public bool isKeyUpPressed {
            get { return keyUpPressedEntity != null; }
            set {
                var entity = keyUpPressedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isKeyUpPressed = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeyUpPressed;

        public static AllOfMatcher KeyUpPressed {
            get {
                if (_matcherKeyUpPressed == null) {
                    _matcherKeyUpPressed = new Matcher(ComponentIds.KeyUpPressed);
                }

                return _matcherKeyUpPressed;
            }
        }
    }
}
