namespace Entitas {
    public partial class Entity {
        static readonly RegenerateViewComponent regenerateViewComponent = new RegenerateViewComponent();

        public bool isRegenerateView {
            get { return HasComponent(ComponentIds.RegenerateView); }
            set {
                if (value != isRegenerateView) {
                    if (value) {
                        AddComponent(ComponentIds.RegenerateView, regenerateViewComponent);
                    } else {
                        RemoveComponent(ComponentIds.RegenerateView);
                    }
                }
            }
        }

        public Entity IsRegenerateView(bool value) {
            isRegenerateView = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherRegenerateView;

        public static AllOfMatcher RegenerateView {
            get {
                if (_matcherRegenerateView == null) {
                    _matcherRegenerateView = new Matcher(ComponentIds.RegenerateView);
                }

                return _matcherRegenerateView;
            }
        }
    }
}
