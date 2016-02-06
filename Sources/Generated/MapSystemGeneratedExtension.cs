namespace Entitas {
    public partial class Pool {
        public ISystem CreateMapSystem() {
            return this.CreateSystem<MapSystem>();
        }
    }
}