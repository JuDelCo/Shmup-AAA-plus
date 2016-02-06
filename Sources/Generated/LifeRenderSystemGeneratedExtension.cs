namespace Entitas {
    public partial class Pool {
        public ISystem CreateLifeRenderSystem() {
            return this.CreateSystem<LifeRenderSystem>();
        }
    }
}