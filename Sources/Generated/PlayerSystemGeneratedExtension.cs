namespace Entitas {
    public partial class Pool {
        public ISystem CreatePlayerSystem() {
            return this.CreateSystem<PlayerSystem>();
        }
    }
}