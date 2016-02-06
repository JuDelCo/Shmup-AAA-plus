namespace Entitas {
    public partial class Entity {
        static readonly KeyDownPressedComponent keyDownPressedComponent = new KeyDownPressedComponent();

        public bool isKeyDownPressed {
            get { return HasComponent(ComponentIds.KeyDownPressed); }
            set {
                if (value != isKeyDownPressed) {
                    if (value) {
                        AddComponent(ComponentIds.KeyDownPressed, keyDownPressedComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeyDownPressed);
                    }
                }
            }
        }

        public Entity IsKeyDownPressed(bool value) {
            isKeyDownPressed = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity keyDownPressedEntity { get { return GetGroup(Matcher.KeyDownPressed).GetSingleEntity(); } }

        public bool isKeyDownPressed {
            get { return keyDownPressedEntity != null; }
            set {
                var entity = keyDownPressedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isKeyDownPressed = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeyDownPressed;

        public static AllOfMatcher KeyDownPressed {
            get {
                if (_matcherKeyDownPressed == null) {
                    _matcherKeyDownPressed = new Matcher(ComponentIds.KeyDownPressed);
                }

                return _matcherKeyDownPressed;
            }
        }
    }
}
