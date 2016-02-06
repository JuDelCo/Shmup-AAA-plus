using UnityEngine;
using Entitas;

public class FlashSystem : IExecuteSystem, ISetPool
{
	Group _damaged;
	Group _flashed;
	
	public void SetPool(Pool pool)
	{
		_damaged = pool.GetGroup(Matcher.AllOf(Matcher.View, Matcher.Damage));
		_flashed = pool.GetGroup(Matcher.AllOf(Matcher.View, Matcher.Flash));
	}
	
	public void Execute()
	{
		const float flashDuration = 0.1f;
		
		foreach (var e in _damaged.GetEntities())
		{
			if(e.hasFlash)
			{
				e.ReplaceFlash(e.flash.untilTime + flashDuration / 2f);
				continue;
			}
			
			e.view.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f);
			e.AddFlash(Time.time + flashDuration);
		}
		
		foreach (var e in _flashed.GetEntities())
		{
			if(e.flash.untilTime <= Time.time)
			{
				e.view.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
				e.RemoveFlash();
			}
		}
	}
}
