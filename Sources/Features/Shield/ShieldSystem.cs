using Entitas;

public class ShieldSystem : IExecuteSystem, ISetPool
{
	Group _shields;
	
	public void SetPool(Pool pool)
	{
		_shields = pool.GetGroup(Matcher.Shield);
	}
	
	public void Execute()
	{
		foreach(var e in _shields.GetEntities())
		{
			if(! e.shield.owner.hasPosition) return;
			
			e.ReplacePosition(e.shield.owner.position.x, e.shield.owner.position.y);
		}
	}
}
