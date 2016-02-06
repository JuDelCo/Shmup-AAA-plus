namespace Entitas {
    public partial class Pool {
        public ISystem CreateRotateViewSystem() {
            return this.CreateSystem<RotateViewSystem>();
        }
    }
}