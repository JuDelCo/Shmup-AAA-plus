namespace Entitas {
    public partial class Pool {
        public ISystem CreateScreenCollisionDestroySystem() {
            return this.CreateSystem<ScreenCollisionDestroySystem>();
        }
    }
}