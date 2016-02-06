namespace Entitas {
    public partial class Pool {
        public ISystem CreateEnemyTestSpawnerSystem() {
            return this.CreateSystem<EnemyTestSpawnerSystem>();
        }
    }
}