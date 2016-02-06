namespace Entitas {
    public partial class Pool {
        public ISystem CreateImmortalSystem() {
            return this.CreateSystem<ImmortalSystem>();
        }
    }
}