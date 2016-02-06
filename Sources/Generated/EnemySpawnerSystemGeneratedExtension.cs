namespace Entitas {
    public partial class Pool {
        public ISystem CreateEnemySpawnerSystem() {
            return this.CreateSystem<EnemySpawnerSystem>();
        }
    }
}