namespace Entitas {
    public partial class Entity {
        static readonly RotateViewComponent rotateViewComponent = new RotateViewComponent();

        public bool isRotateView {
            get { return HasComponent(ComponentIds.RotateView); }
            set {
                if (value != isRotateView) {
                    if (value) {
                        AddComponent(ComponentIds.RotateView, rotateViewComponent);
                    } else {
                        RemoveComponent(ComponentIds.RotateView);
                    }
                }
            }
        }

        public Entity IsRotateView(bool value) {
            isRotateView = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherRotateView;

        public static AllOfMatcher RotateView {
            get {
                if (_matcherRotateView == null) {
                    _matcherRotateView = new Matcher(ComponentIds.RotateView);
                }

                return _matcherRotateView;
            }
        }
    }
}
