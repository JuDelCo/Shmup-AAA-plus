using System.Collections.Generic;
using Entitas;

public class ScoreSystem : IStartSystem, IReactiveSystem, ISetPool
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Points, Matcher.Killed); } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }
	
	Pool _pool;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
	}
	
	public void Start()
	{
		_pool.SetScore(0);
	}
	
	public void Execute(List<Entity> entites)
	{
		if(! _pool.hasScore)
		{
			return;
		}
		
		foreach(Entity e in entites)
		{
			_pool.ReplaceScore(_pool.score.value + e.points.value);
		}
	}
}
