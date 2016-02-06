namespace Entitas {
    public partial class Entity {
        static readonly KeyReturnPressedComponent keyReturnPressedComponent = new KeyReturnPressedComponent();

        public bool isKeyReturnPressed {
            get { return HasComponent(ComponentIds.KeyReturnPressed); }
            set {
                if (value != isKeyReturnPressed) {
                    if (value) {
                        AddComponent(ComponentIds.KeyReturnPressed, keyReturnPressedComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeyReturnPressed);
                    }
                }
            }
        }

        public Entity IsKeyReturnPressed(bool value) {
            isKeyReturnPressed = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity keyReturnPressedEntity { get { return GetGroup(Matcher.KeyReturnPressed).GetSingleEntity(); } }

        public bool isKeyReturnPressed {
            get { return keyReturnPressedEntity != null; }
            set {
                var entity = keyReturnPressedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isKeyReturnPressed = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeyReturnPressed;

        public static AllOfMatcher KeyReturnPressed {
            get {
                if (_matcherKeyReturnPressed == null) {
                    _matcherKeyReturnPressed = new Matcher(ComponentIds.KeyReturnPressed);
                }

                return _matcherKeyReturnPressed;
            }
        }
    }
}
