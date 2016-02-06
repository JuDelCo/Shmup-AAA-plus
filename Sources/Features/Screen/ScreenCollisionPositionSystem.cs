using System;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ScreenCollisionPositionSystem : IReactiveSystem, ISetPool
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.ScreenLimitedPosition); } }
	
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
			var offset = e.screenLimitedPosition.offset;
			
			if(-screenSize.x + offset.x > position.x)
			{
				position.x = -screenSize.x + offset.x;
			}
			else if(screenSize.x + offset.z < position.x)
			{
				position.x = screenSize.x + offset.z;
			}
			
			if(screenSize.y + offset.y < position.y)
			{
				position.y = screenSize.y + offset.y;
			}
			else if(-screenSize.y + offset.w > position.y)
			{
				position.y = -screenSize.y + offset.w;
			}
		}
	}
}
