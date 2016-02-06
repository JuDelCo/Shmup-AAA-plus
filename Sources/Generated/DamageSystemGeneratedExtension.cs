namespace Entitas {
    public partial class Pool {
        public ISystem CreateDamageSystem() {
            return this.CreateSystem<DamageSystem>();
        }
    }
}