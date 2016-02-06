namespace Entitas {
    public partial class Entity {
        static readonly ScoreNumScreenComponent scoreNumScreenComponent = new ScoreNumScreenComponent();

        public bool isScoreNumScreen {
            get { return HasComponent(ComponentIds.ScoreNumScreen); }
            set {
                if (value != isScoreNumScreen) {
                    if (value) {
                        AddComponent(ComponentIds.ScoreNumScreen, scoreNumScreenComponent);
                    } else {
                        RemoveComponent(ComponentIds.ScoreNumScreen);
                    }
                }
            }
        }

        public Entity IsScoreNumScreen(bool value) {
            isScoreNumScreen = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherScoreNumScreen;

        public static AllOfMatcher ScoreNumScreen {
            get {
                if (_matcherScoreNumScreen == null) {
                    _matcherScoreNumScreen = new Matcher(ComponentIds.ScoreNumScreen);
                }

                return _matcherScoreNumScreen;
            }
        }
    }
}
