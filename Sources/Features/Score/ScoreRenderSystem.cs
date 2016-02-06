using System.Collections.Generic;
using Entitas;

public class ScoreRenderSystem : IReactiveSystem, ISetPool
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Score); } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }
	
	Pool _pool;
	Group _scoreNumbers;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
		_scoreNumbers = pool.GetGroup(Matcher.AllOf(Matcher.ScoreNumScreen, Matcher.View));
	}
	
	public void Execute(List<Entity> entites)
	{
		// Remove old value (this should be improved...)
		foreach(Entity num in _scoreNumbers.GetEntities())
		{
			num.isDestroy = true;
		}
		
		DrawScore();
	}
	
	void DrawScore()
	{
		var score = _pool.score.value.ToString("00000000");
		int counter = 0;
		
		foreach (char num in score)
		{
			_pool.CreateScoreUiNumber(-2.0f + (++counter * 0.5f), -4.25f, num);
		}
	}
}
