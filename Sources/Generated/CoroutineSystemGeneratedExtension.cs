namespace Entitas {
    public partial class Pool {
        public ISystem CreateCoroutineSystem() {
            return this.CreateSystem<CoroutineSystem>();
        }
    }
}