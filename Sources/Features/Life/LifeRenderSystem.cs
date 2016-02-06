using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class LifeRenderSystem : IReactiveSystem, ISetPool
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Life); } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }
	
	Pool _pool;
	Group _lifeNumbers;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
		_lifeNumbers = pool.GetGroup(Matcher.AllOf(Matcher.LifeNumScreen, Matcher.View));
	}
	
	public void Execute(List<Entity> entites)
	{
		// Remove old value (this should be improved...)
		foreach(Entity num in _lifeNumbers.GetEntities())
		{
			num.isDestroy = true;
		}
		
		DrawLifes();
	}
	
	void DrawLifes()
	{
		_pool.CreateEntity()
				.IsLifeNumScreen(true)
				.AddPosition(3.5f, -4.25f)
				.AddResource("LifeUI");
		
		var lifes = Mathf.Max(0, _pool.life.value).ToString("00");
		int counter = 0;
		
		foreach (char num in lifes)
		{
			_pool.CreateLifeUiNumber(3.5f + (++counter * 0.5f), -4.25f, num);
		}
	}
}
