using UnityEngine;
using Entitas;

public static class EnemyCoroutinesHelper
{
	// MOVEMENT MODES
	// =============================================
	
	static public void GoStop(Entity e)
	{
		e.RemoveSpeed();
	}
	
	static public void GoDown(Entity e)
	{
		e.ReplaceSpeed(new Vector2(0f, -0.08f));
	}
	
	static public void GoUp(Entity e)
	{
		e.ReplaceSpeed(new Vector2(0f, 0.08f));
	}
	
	static public void GoLateral(Entity e)
	{
		e.ReplaceSpeed(new Vector2(Mathf.Sign(e.speed.value.x) * 0.08f, 0f));
	}
	
	static public void GoDiagonalDown(Entity e)
	{
		e.ReplaceSpeed(new Vector2(-Mathf.Sign(e.position.x) * 0.04f, -0.05f));
	}
	
	static public void GoDiagonalUp(Entity e)
	{
		e.ReplaceSpeed(new Vector2(-Mathf.Sign(e.position.x) * 0.04f, 0.05f));
	}
	
	static public void GoToPlayer(Pool pool, Entity e)
	{
		e.ReplaceSpeed(GetEnemyToPlayerDirection(pool, e) * 0.08f);
	}
	
	// SHOOT MODES
	// =============================================
	
	static public void ShootToPlayer(Pool pool, Entity e)
	{
		pool.CreateBullet(e.position.x, e.position.y, GetEnemyToPlayerDirection(pool, e) * 0.05f, 5, 6, false);
	}
	
	static public void ShootCircleBullets(Pool pool, Entity e)
	{
		for(int i = 0; i < 12; ++i)
		{
			ShootCircleBullet(pool, e, i, 12);
		}
	}
	
	static public void ShootCircleBullet(Pool pool, Entity e, int circleBulletNum, int circleBulletCount)
	{
		float fraction = (2f * Mathf.PI) / circleBulletCount;
		
		pool.CreateBullet(e.position.x, e.position.y, new Vector2(Mathf.Sin(circleBulletNum*fraction), Mathf.Cos(circleBulletNum*fraction)) * 0.05f, 5, 8, false);
	}
	
	// HELPERS
	// =============================================
	
	static public Vector2 GetEnemyToPositionDirection(Entity e, Vector2 position)
	{
		return (position - e.position.ToVector2()).normalized;
	}
	
	static public Vector2 GetEnemyToPlayerDirection(Pool pool, Entity e)
	{
		var player = pool.GetGroup(Matcher.Player).GetSingleEntity();
		
		if(player == null)
		{
			return Vector2.down;
		}
		
		return (player.position.ToVector2() - e.position.ToVector2()).normalized;
	}
}
