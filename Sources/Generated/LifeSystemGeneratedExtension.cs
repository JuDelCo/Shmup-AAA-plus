namespace Entitas {
    public partial class Pool {
        public ISystem CreateLifeSystem() {
            return this.CreateSystem<LifeSystem>();
        }
    }
}