namespace Entitas {
    public partial class Pool {
        public ISystem CreateScreenCollisionPositionSystem() {
            return this.CreateSystem<ScreenCollisionPositionSystem>();
        }
    }
}