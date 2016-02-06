using Entitas;
using UnityEngine;

public class MoveSystem : IExecuteSystem, ISetPool
{
	Group _group;
	
	public void SetPool(Pool pool)
	{
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Speed, Matcher.Position));
	}
	
	public void Execute()
	{
		foreach (var e in _group.GetEntities())
		{
			var speed = e.speed.value;
			var oldPos = e.position;
			
			if(speed == Vector2.zero) continue;
			
			e.ReplacePosition(oldPos.x + speed.x, oldPos.y + speed.y);
		}
	}
}
