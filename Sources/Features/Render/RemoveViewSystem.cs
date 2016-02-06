using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RemoveViewSystem : IMultiReactiveSystem, ISetPool, IEnsureComponents
{
	public IMatcher[] triggers { get { return new [] {
        Matcher.Resource,
        Matcher.AllOf(Matcher.Resource, Matcher.Destroy),
        Matcher.AllOf(Matcher.Resource, Matcher.RegenerateView)
    }; } }
	
	public GroupEventType[] eventTypes { get { return new [] {
        GroupEventType.OnEntityRemoved,
        GroupEventType.OnEntityAdded,
        GroupEventType.OnEntityAdded
    }; } }
	
	public IMatcher ensureComponents { get { return Matcher.View; } }
	
	public void SetPool(Pool pool)
	{
		// Destroy unity gameObject when view component is destroyed
		pool.GetGroup(Matcher.View).OnEntityRemoved += OnEntityRemoved;
	}
	
	void OnEntityRemoved(Group group, Entity entity, int index, IComponent component)
	{
		Object.Destroy(((ViewComponent)component).gameObject);
	}
	
	public void Execute(List<Entity> entities)
	{
		foreach (var e in entities)
		{
			e.RemoveView();
		}
	}
}
