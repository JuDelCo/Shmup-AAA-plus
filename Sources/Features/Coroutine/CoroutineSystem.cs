using UnityEngine;
using Entitas;

public class CoroutineSystem : IExecuteSystem, ISetPool
{
	Group _coroutines;
	
	public void SetPool(Pool pool)
	{
		_coroutines = pool.GetGroup(Matcher.AllOf(Matcher.Coroutine));
	}
	
	public void Execute()
	{
		foreach (var e in _coroutines.GetEntities())
		{
			if(e.hasCoroutineWait)
			{
				if(e.coroutineWait.time >= Time.time)
				{
					continue;
				}
				
				e.RemoveCoroutineWait();
			}
			
			if(! e.coroutine.value.MoveNext())
			{
				e.RemoveCoroutine();
				
				if(e.GetComponentIndices().Length == 0)
				{
					e.isDestroy = true;
				}
			}
		}
	}
}
