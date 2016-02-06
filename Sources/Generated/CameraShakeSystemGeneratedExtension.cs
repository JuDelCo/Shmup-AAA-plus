namespace Entitas {
    public partial class Pool {
        public ISystem CreateCameraShakeSystem() {
            return this.CreateSystem<CameraShakeSystem>();
        }
    }
}