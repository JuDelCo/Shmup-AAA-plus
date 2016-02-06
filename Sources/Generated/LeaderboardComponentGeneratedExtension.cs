using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public LeaderboardComponent leaderboard { get { return (LeaderboardComponent)GetComponent(ComponentIds.Leaderboard); } }

        public bool hasLeaderboard { get { return HasComponent(ComponentIds.Leaderboard); } }

        static readonly Stack<LeaderboardComponent> _leaderboardComponentPool = new Stack<LeaderboardComponent>();

        public static void ClearLeaderboardComponentPool() {
            _leaderboardComponentPool.Clear();
        }

        public Entity AddLeaderboard(string newUserName) {
            var component = _leaderboardComponentPool.Count > 0 ? _leaderboardComponentPool.Pop() : new LeaderboardComponent();
            component.userName = newUserName;
            return AddComponent(ComponentIds.Leaderboard, component);
        }

        public Entity ReplaceLeaderboard(string newUserName) {
            var previousComponent = hasLeaderboard ? leaderboard : null;
            var component = _leaderboardComponentPool.Count > 0 ? _leaderboardComponentPool.Pop() : new LeaderboardComponent();
            component.userName = newUserName;
            ReplaceComponent(ComponentIds.Leaderboard, component);
            if (previousComponent != null) {
                _leaderboardComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLeaderboard() {
            var component = leaderboard;
            RemoveComponent(ComponentIds.Leaderboard);
            _leaderboardComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity leaderboardEntity { get { return GetGroup(Matcher.Leaderboard).GetSingleEntity(); } }

        public LeaderboardComponent leaderboard { get { return leaderboardEntity.leaderboard; } }

        public bool hasLeaderboard { get { return leaderboardEntity != null; } }

        public Entity SetLeaderboard(string newUserName) {
            if (hasLeaderboard) {
                throw new SingleEntityException(Matcher.Leaderboard);
            }
            var entity = CreateEntity();
            entity.AddLeaderboard(newUserName);
            return entity;
        }

        public Entity ReplaceLeaderboard(string newUserName) {
            var entity = leaderboardEntity;
            if (entity == null) {
                entity = SetLeaderboard(newUserName);
            } else {
                entity.ReplaceLeaderboard(newUserName);
            }

            return entity;
        }

        public void RemoveLeaderboard() {
            DestroyEntity(leaderboardEntity);
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherLeaderboard;

        public static AllOfMatcher Leaderboard {
            get {
                if (_matcherLeaderboard == null) {
                    _matcherLeaderboard = new Matcher(ComponentIds.Leaderboard);
                }

                return _matcherLeaderboard;
            }
        }
    }
}
