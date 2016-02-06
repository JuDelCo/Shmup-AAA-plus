namespace Entitas {
    public partial class Entity {
        static readonly KeyDeletePressedComponent keyDeletePressedComponent = new KeyDeletePressedComponent();

        public bool isKeyDeletePressed {
            get { return HasComponent(ComponentIds.KeyDeletePressed); }
            set {
                if (value != isKeyDeletePressed) {
                    if (value) {
                        AddComponent(ComponentIds.KeyDeletePressed, keyDeletePressedComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeyDeletePressed);
                    }
                }
            }
        }

        public Entity IsKeyDeletePressed(bool value) {
            isKeyDeletePressed = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity keyDeletePressedEntity { get { return GetGroup(Matcher.KeyDeletePressed).GetSingleEntity(); } }

        public bool isKeyDeletePressed {
            get { return keyDeletePressedEntity != null; }
            set {
                var entity = keyDeletePressedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isKeyDeletePressed = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeyDeletePressed;

        public static AllOfMatcher KeyDeletePressed {
            get {
                if (_matcherKeyDeletePressed == null) {
                    _matcherKeyDeletePressed = new Matcher(ComponentIds.KeyDeletePressed);
                }

                return _matcherKeyDeletePressed;
            }
        }
    }
}
