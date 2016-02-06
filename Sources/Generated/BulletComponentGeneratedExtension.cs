namespace Entitas {
    public partial class Entity {
        static readonly BulletComponent bulletComponent = new BulletComponent();

        public bool isBullet {
            get { return HasComponent(ComponentIds.Bullet); }
            set {
                if (value != isBullet) {
                    if (value) {
                        AddComponent(ComponentIds.Bullet, bulletComponent);
                    } else {
                        RemoveComponent(ComponentIds.Bullet);
                    }
                }
            }
        }

        public Entity IsBullet(bool value) {
            isBullet = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherBullet;

        public static AllOfMatcher Bullet {
            get {
                if (_matcherBullet == null) {
                    _matcherBullet = new Matcher(ComponentIds.Bullet);
                }

                return _matcherBullet;
            }
        }
    }
}
