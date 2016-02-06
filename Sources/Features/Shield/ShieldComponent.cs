using System.Collections;
using UnityEngine;
using Entitas;

public class ShieldComponent : IComponent
{
	public Entity owner;
}

public static class ShieldHelper
{
	static public IEnumerator GetCoroutine(Entity e)
	{
		return ShieldCoroutine(e, Time.time).GetEnumerator();
	}
	
	static IEnumerable WaitMs(Entity e, float ms)
	{
		e.ReplaceCoroutineWait(Time.time + (ms / 1000.0f));
		return null;
	}
	
	static IEnumerable ShieldCoroutine(Entity e, float startTime)
	{
		e.shield.owner.ReplaceImmortal(startTime + 2f);
		
		yield return WaitMs(e, 1000);
		
		for(int i = 0; i < 10f; ++i)
		{
			e.view.gameObject.GetComponent<SpriteRenderer>().color = (i%2 == 0 ? Color.clear : Color.white);
			
			if(i%2 == 0) AudioController.PlayShieldEffect();
			
			yield return WaitMs(e, 100);
		}
		
		e.isDestroy = true;
	}
}
