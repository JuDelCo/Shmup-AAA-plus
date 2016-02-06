namespace Entitas {
    public partial class Pool {
        public ISystem CreateEnergySystem() {
            return this.CreateSystem<EnergySystem>();
        }
    }
}