using System.Collections.Generic;
using Entitas;

public class HealthSystem : IReactiveSystem, ISetPool
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Health); } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

	Pool _pool;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
	}
	
	public void Execute(List<Entity> entities)
	{
		foreach (var e in entities)
		{
			if(e.health.value <= 0f)
			{
				e.isKilled = true;
				e.isDestroy = true;
				
				_pool.CreateExplosion(e.position.x, e.position.y);
				
				if(e.hasPlayer)
				{
					AudioController.PlayBigExplosion();
				}
				else
				{
					AudioController.PlayExplosion();
				}
			}
		}
	}
}
