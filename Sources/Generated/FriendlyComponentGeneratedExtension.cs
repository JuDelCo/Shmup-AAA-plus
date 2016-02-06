namespace Entitas {
    public partial class Entity {
        static readonly FriendlyComponent friendlyComponent = new FriendlyComponent();

        public bool isFriendly {
            get { return HasComponent(ComponentIds.Friendly); }
            set {
                if (value != isFriendly) {
                    if (value) {
                        AddComponent(ComponentIds.Friendly, friendlyComponent);
                    } else {
                        RemoveComponent(ComponentIds.Friendly);
                    }
                }
            }
        }

        public Entity IsFriendly(bool value) {
            isFriendly = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherFriendly;

        public static AllOfMatcher Friendly {
            get {
                if (_matcherFriendly == null) {
                    _matcherFriendly = new Matcher(ComponentIds.Friendly);
                }

                return _matcherFriendly;
            }
        }
    }
}
