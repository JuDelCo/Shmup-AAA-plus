namespace Entitas {
    public partial class Entity {
        static readonly MapComponent mapComponent = new MapComponent();

        public bool isMap {
            get { return HasComponent(ComponentIds.Map); }
            set {
                if (value != isMap) {
                    if (value) {
                        AddComponent(ComponentIds.Map, mapComponent);
                    } else {
                        RemoveComponent(ComponentIds.Map);
                    }
                }
            }
        }

        public Entity IsMap(bool value) {
            isMap = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherMap;

        public static AllOfMatcher Map {
            get {
                if (_matcherMap == null) {
                    _matcherMap = new Matcher(ComponentIds.Map);
                }

                return _matcherMap;
            }
        }
    }
}
