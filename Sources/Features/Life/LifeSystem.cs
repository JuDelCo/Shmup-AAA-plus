using System.Collections.Generic;
using Entitas;

public class LifeSystem : IReactiveSystem, ISetPool
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Player, Matcher.Killed); } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }
	
	Pool _pool;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
	}
	
	public void Execute(List<Entity> entities)
	{
		if(! _pool.hasLife) return;
		
		var newLifeValue = _pool.life.value - 1;
		
		if(newLifeValue < 0)
		{
			_pool.StartGameOver();
		}
		
		_pool.ReplaceLife(newLifeValue);
	}
}
