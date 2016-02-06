using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PointsComponent points { get { return (PointsComponent)GetComponent(ComponentIds.Points); } }

        public bool hasPoints { get { return HasComponent(ComponentIds.Points); } }

        static readonly Stack<PointsComponent> _pointsComponentPool = new Stack<PointsComponent>();

        public static void ClearPointsComponentPool() {
            _pointsComponentPool.Clear();
        }

        public Entity AddPoints(int newValue) {
            var component = _pointsComponentPool.Count > 0 ? _pointsComponentPool.Pop() : new PointsComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.Points, component);
        }

        public Entity ReplacePoints(int newValue) {
            var previousComponent = hasPoints ? points : null;
            var component = _pointsComponentPool.Count > 0 ? _pointsComponentPool.Pop() : new PointsComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.Points, component);
            if (previousComponent != null) {
                _pointsComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePoints() {
            var component = points;
            RemoveComponent(ComponentIds.Points);
            _pointsComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherPoints;

        public static AllOfMatcher Points {
            get {
                if (_matcherPoints == null) {
                    _matcherPoints = new Matcher(ComponentIds.Points);
                }

                return _matcherPoints;
            }
        }
    }
}
