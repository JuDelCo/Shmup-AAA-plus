namespace Entitas {
    public partial class Pool {
        public ISystem CreateFlashSystem() {
            return this.CreateSystem<FlashSystem>();
        }
    }
}