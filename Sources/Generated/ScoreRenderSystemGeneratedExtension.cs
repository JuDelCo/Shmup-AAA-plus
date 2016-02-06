namespace Entitas {
    public partial class Pool {
        public ISystem CreateScoreRenderSystem() {
            return this.CreateSystem<ScoreRenderSystem>();
        }
    }
}