using UnityEngine;
using Entitas;

public class ImmortalSystem : IExecuteSystem, ISetPool
{
	Group _immortals;
	
	public void SetPool(Pool pool)
	{
		_immortals = pool.GetGroup(Matcher.Immortal);
	}
	
	public void Execute()
	{
		foreach (var e in _immortals.GetEntities())
		{
			if(e.immortal.untilTime <= Time.time)
			{
				e.RemoveImmortal();
			}
		}
	}
}
