namespace Entitas {
    public partial class Entity {
        static readonly KeyEscapePressedComponent keyEscapePressedComponent = new KeyEscapePressedComponent();

        public bool isKeyEscapePressed {
            get { return HasComponent(ComponentIds.KeyEscapePressed); }
            set {
                if (value != isKeyEscapePressed) {
                    if (value) {
                        AddComponent(ComponentIds.KeyEscapePressed, keyEscapePressedComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeyEscapePressed);
                    }
                }
            }
        }

        public Entity IsKeyEscapePressed(bool value) {
            isKeyEscapePressed = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity keyEscapePressedEntity { get { return GetGroup(Matcher.KeyEscapePressed).GetSingleEntity(); } }

        public bool isKeyEscapePressed {
            get { return keyEscapePressedEntity != null; }
            set {
                var entity = keyEscapePressedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isKeyEscapePressed = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeyEscapePressed;

        public static AllOfMatcher KeyEscapePressed {
            get {
                if (_matcherKeyEscapePressed == null) {
                    _matcherKeyEscapePressed = new Matcher(ComponentIds.KeyEscapePressed);
                }

                return _matcherKeyEscapePressed;
            }
        }
    }
}
