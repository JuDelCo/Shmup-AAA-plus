using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public LowestScoreLeaderboardComponent lowestScoreLeaderboard { get { return (LowestScoreLeaderboardComponent)GetComponent(ComponentIds.LowestScoreLeaderboard); } }

        public bool hasLowestScoreLeaderboard { get { return HasComponent(ComponentIds.LowestScoreLeaderboard); } }

        static readonly Stack<LowestScoreLeaderboardComponent> _lowestScoreLeaderboardComponentPool = new Stack<LowestScoreLeaderboardComponent>();

        public static void ClearLowestScoreLeaderboardComponentPool() {
            _lowestScoreLeaderboardComponentPool.Clear();
        }

        public Entity AddLowestScoreLeaderboard(int newValue) {
            var component = _lowestScoreLeaderboardComponentPool.Count > 0 ? _lowestScoreLeaderboardComponentPool.Pop() : new LowestScoreLeaderboardComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.LowestScoreLeaderboard, component);
        }

        public Entity ReplaceLowestScoreLeaderboard(int newValue) {
            var previousComponent = hasLowestScoreLeaderboard ? lowestScoreLeaderboard : null;
            var component = _lowestScoreLeaderboardComponentPool.Count > 0 ? _lowestScoreLeaderboardComponentPool.Pop() : new LowestScoreLeaderboardComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.LowestScoreLeaderboard, component);
            if (previousComponent != null) {
                _lowestScoreLeaderboardComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLowestScoreLeaderboard() {
            var component = lowestScoreLeaderboard;
            RemoveComponent(ComponentIds.LowestScoreLeaderboard);
            _lowestScoreLeaderboardComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity lowestScoreLeaderboardEntity { get { return GetGroup(Matcher.LowestScoreLeaderboard).GetSingleEntity(); } }

        public LowestScoreLeaderboardComponent lowestScoreLeaderboard { get { return lowestScoreLeaderboardEntity.lowestScoreLeaderboard; } }

        public bool hasLowestScoreLeaderboard { get { return lowestScoreLeaderboardEntity != null; } }

        public Entity SetLowestScoreLeaderboard(int newValue) {
            if (hasLowestScoreLeaderboard) {
                throw new SingleEntityException(Matcher.LowestScoreLeaderboard);
            }
            var entity = CreateEntity();
            entity.AddLowestScoreLeaderboard(newValue);
            return entity;
        }

        public Entity ReplaceLowestScoreLeaderboard(int newValue) {
            var entity = lowestScoreLeaderboardEntity;
            if (entity == null) {
                entity = SetLowestScoreLeaderboard(newValue);
            } else {
                entity.ReplaceLowestScoreLeaderboard(newValue);
            }

            return entity;
        }

        public void RemoveLowestScoreLeaderboard() {
            DestroyEntity(lowestScoreLeaderboardEntity);
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherLowestScoreLeaderboard;

        public static AllOfMatcher LowestScoreLeaderboard {
            get {
                if (_matcherLowestScoreLeaderboard == null) {
                    _matcherLowestScoreLeaderboard = new Matcher(ComponentIds.LowestScoreLeaderboard);
                }

                return _matcherLowestScoreLeaderboard;
            }
        }
    }
}
