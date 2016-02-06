namespace Entitas {
    public partial class Entity {
        static readonly LifeNumScreenComponent lifeNumScreenComponent = new LifeNumScreenComponent();

        public bool isLifeNumScreen {
            get { return HasComponent(ComponentIds.LifeNumScreen); }
            set {
                if (value != isLifeNumScreen) {
                    if (value) {
                        AddComponent(ComponentIds.LifeNumScreen, lifeNumScreenComponent);
                    } else {
                        RemoveComponent(ComponentIds.LifeNumScreen);
                    }
                }
            }
        }

        public Entity IsLifeNumScreen(bool value) {
            isLifeNumScreen = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherLifeNumScreen;

        public static AllOfMatcher LifeNumScreen {
            get {
                if (_matcherLifeNumScreen == null) {
                    _matcherLifeNumScreen = new Matcher(ComponentIds.LifeNumScreen);
                }

                return _matcherLifeNumScreen;
            }
        }
    }
}
