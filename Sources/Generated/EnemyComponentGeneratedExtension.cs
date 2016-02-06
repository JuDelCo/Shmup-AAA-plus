namespace Entitas {
    public partial class Entity {
        static readonly EnemyComponent enemyComponent = new EnemyComponent();

        public bool isEnemy {
            get { return HasComponent(ComponentIds.Enemy); }
            set {
                if (value != isEnemy) {
                    if (value) {
                        AddComponent(ComponentIds.Enemy, enemyComponent);
                    } else {
                        RemoveComponent(ComponentIds.Enemy);
                    }
                }
            }
        }

        public Entity IsEnemy(bool value) {
            isEnemy = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherEnemy;

        public static AllOfMatcher Enemy {
            get {
                if (_matcherEnemy == null) {
                    _matcherEnemy = new Matcher(ComponentIds.Enemy);
                }

                return _matcherEnemy;
            }
        }
    }
}
