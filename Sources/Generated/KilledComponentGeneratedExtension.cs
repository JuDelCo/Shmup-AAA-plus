namespace Entitas {
    public partial class Entity {
        static readonly KilledComponent killedComponent = new KilledComponent();

        public bool isKilled {
            get { return HasComponent(ComponentIds.Killed); }
            set {
                if (value != isKilled) {
                    if (value) {
                        AddComponent(ComponentIds.Killed, killedComponent);
                    } else {
                        RemoveComponent(ComponentIds.Killed);
                    }
                }
            }
        }

        public Entity IsKilled(bool value) {
            isKilled = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKilled;

        public static AllOfMatcher Killed {
            get {
                if (_matcherKilled == null) {
                    _matcherKilled = new Matcher(ComponentIds.Killed);
                }

                return _matcherKilled;
            }
        }
    }
}
