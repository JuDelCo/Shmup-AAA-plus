namespace Entitas {
    public partial class Pool {
        public ISystem CreateHealthSystem() {
            return this.CreateSystem<HealthSystem>();
        }
    }
}