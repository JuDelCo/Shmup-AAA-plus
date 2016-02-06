namespace Entitas {
    public partial class Pool {
        public ISystem CreateLeaderboardSystem() {
            return this.CreateSystem<LeaderboardSystem>();
        }
    }
}