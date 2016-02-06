using System;
using UnityEngine;
using Entitas;

public class CollisionDamageSystem : IExecuteSystem, ISetPool
{
	Group _damageDealers;
	Group _aliveEntities;
	Collider2D[] results = new Collider2D[10]; // Cache
	
	public void SetPool(Pool pool)
	{
		_damageDealers = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.CollisionDamage, Matcher.View));
		_aliveEntities = pool.GetGroup(Matcher.AllOf(Matcher.Health, Matcher.View));
	}
	
	public void Execute()
	{
		foreach (var dealer in _damageDealers.GetEntities())
		{
			var numCollisions = Physics2D.OverlapCircleNonAlloc(dealer.position.ToVector2(), dealer.view.gameObject.GetComponent<CircleCollider2D>().radius, results);
			
			for (int i = 0; i < numCollisions; ++i)
			{
				var e = results[i].gameObject.GetComponent<IEntitasComponent>().GetEntity();
				
				if( !(e.isFriendly && dealer.isFriendly) && !(e.isNotFriendly && dealer.isNotFriendly) && !(e.isBullet && dealer.isBullet)
				   && _aliveEntities.ContainsEntity(e))
				{
					e.ReplaceDamage(dealer.collisionDamage.value);
					
					if(dealer.isBullet)
					{
						dealer.isDestroy = true;
					}
				}
			}
		}
	}
}
