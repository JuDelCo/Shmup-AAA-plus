using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PlayerComponent player { get { return (PlayerComponent)GetComponent(ComponentIds.Player); } }

        public bool hasPlayer { get { return HasComponent(ComponentIds.Player); } }

        static readonly Stack<PlayerComponent> _playerComponentPool = new Stack<PlayerComponent>();

        public static void ClearPlayerComponentPool() {
            _playerComponentPool.Clear();
        }

        public Entity AddPlayer(float newShootSpeed) {
            var component = _playerComponentPool.Count > 0 ? _playerComponentPool.Pop() : new PlayerComponent();
            component.shootSpeed = newShootSpeed;
            return AddComponent(ComponentIds.Player, component);
        }

        public Entity ReplacePlayer(float newShootSpeed) {
            var previousComponent = hasPlayer ? player : null;
            var component = _playerComponentPool.Count > 0 ? _playerComponentPool.Pop() : new PlayerComponent();
            component.shootSpeed = newShootSpeed;
            ReplaceComponent(ComponentIds.Player, component);
            if (previousComponent != null) {
                _playerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePlayer() {
            var component = player;
            RemoveComponent(ComponentIds.Player);
            _playerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity playerEntity { get { return GetGroup(Matcher.Player).GetSingleEntity(); } }

        public PlayerComponent player { get { return playerEntity.player; } }

        public bool hasPlayer { get { return playerEntity != null; } }

        public Entity SetPlayer(float newShootSpeed) {
            if (hasPlayer) {
                throw new SingleEntityException(Matcher.Player);
            }
            var entity = CreateEntity();
            entity.AddPlayer(newShootSpeed);
            return entity;
        }

        public Entity ReplacePlayer(float newShootSpeed) {
            var entity = playerEntity;
            if (entity == null) {
                entity = SetPlayer(newShootSpeed);
            } else {
                entity.ReplacePlayer(newShootSpeed);
            }

            return entity;
        }

        public void RemovePlayer() {
            DestroyEntity(playerEntity);
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherPlayer;

        public static AllOfMatcher Player {
            get {
                if (_matcherPlayer == null) {
                    _matcherPlayer = new Matcher(ComponentIds.Player);
                }

                return _matcherPlayer;
            }
        }
    }
}
