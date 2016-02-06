namespace Entitas {
    public partial class Entity {
        static readonly NotFriendlyComponent notFriendlyComponent = new NotFriendlyComponent();

        public bool isNotFriendly {
            get { return HasComponent(ComponentIds.NotFriendly); }
            set {
                if (value != isNotFriendly) {
                    if (value) {
                        AddComponent(ComponentIds.NotFriendly, notFriendlyComponent);
                    } else {
                        RemoveComponent(ComponentIds.NotFriendly);
                    }
                }
            }
        }

        public Entity IsNotFriendly(bool value) {
            isNotFriendly = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherNotFriendly;

        public static AllOfMatcher NotFriendly {
            get {
                if (_matcherNotFriendly == null) {
                    _matcherNotFriendly = new Matcher(ComponentIds.NotFriendly);
                }

                return _matcherNotFriendly;
            }
        }
    }
}
