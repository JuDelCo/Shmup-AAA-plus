namespace Entitas {
    public partial class Pool {
        public ISystem CreateShieldSystem() {
            return this.CreateSystem<ShieldSystem>();
        }
    }
}