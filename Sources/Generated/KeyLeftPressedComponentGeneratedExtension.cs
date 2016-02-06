namespace Entitas {
    public partial class Entity {
        static readonly KeyLeftPressedComponent keyLeftPressedComponent = new KeyLeftPressedComponent();

        public bool isKeyLeftPressed {
            get { return HasComponent(ComponentIds.KeyLeftPressed); }
            set {
                if (value != isKeyLeftPressed) {
                    if (value) {
                        AddComponent(ComponentIds.KeyLeftPressed, keyLeftPressedComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeyLeftPressed);
                    }
                }
            }
        }

        public Entity IsKeyLeftPressed(bool value) {
            isKeyLeftPressed = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity keyLeftPressedEntity { get { return GetGroup(Matcher.KeyLeftPressed).GetSingleEntity(); } }

        public bool isKeyLeftPressed {
            get { return keyLeftPressedEntity != null; }
            set {
                var entity = keyLeftPressedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isKeyLeftPressed = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeyLeftPressed;

        public static AllOfMatcher KeyLeftPressed {
            get {
                if (_matcherKeyLeftPressed == null) {
                    _matcherKeyLeftPressed = new Matcher(ComponentIds.KeyLeftPressed);
                }

                return _matcherKeyLeftPressed;
            }
        }
    }
}
