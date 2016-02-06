namespace Entitas {
    public partial class Entity {
        static readonly ExplosionComponent explosionComponent = new ExplosionComponent();

        public bool isExplosion {
            get { return HasComponent(ComponentIds.Explosion); }
            set {
                if (value != isExplosion) {
                    if (value) {
                        AddComponent(ComponentIds.Explosion, explosionComponent);
                    } else {
                        RemoveComponent(ComponentIds.Explosion);
                    }
                }
            }
        }

        public Entity IsExplosion(bool value) {
            isExplosion = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherExplosion;

        public static AllOfMatcher Explosion {
            get {
                if (_matcherExplosion == null) {
                    _matcherExplosion = new Matcher(ComponentIds.Explosion);
                }

                return _matcherExplosion;
            }
        }
    }
}
