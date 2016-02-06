using System;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ScreenCollisionDestroySystem : IReactiveSystem, ISetPool
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.ScreenLimitedDestroy); } }
	
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
			var position = e.position;
			var screenSize = _pool.screen.size / 2f;
			var offset = e.screenLimitedDestroy.offset;
			
			if(-screenSize.x + offset.x > position.x ||  screenSize.x + offset.z < position.x ||
				screenSize.y + offset.y < position.y || -screenSize.y + offset.w > position.y)
			{
				e.isDestroy = true;
			}
		}
	}
}
