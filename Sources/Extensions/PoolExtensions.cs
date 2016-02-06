using Entitas;
using UnityEngine;

public static class PoolExtensions
{
	public static void StartGame(this Pool pool)
	{
		pool.SetScreen(new Vector2(10f, 9f));
		
		pool.SetLife(3);
		pool.CreatePlayerEnergy();
		var player = pool.CreatePlayer();
		pool.CreateShield(player);
		
		pool.SetMapSpeed(new Vector2(0f, -0.01f));
		pool.CreateMapBackground(3f, 0);
		pool.CreateMapBackground(11f);
		pool.CreateMapBackground(19f);
		
		LeaderBoardController.GetHighScores(5, highScores =>
		{
			int lowestTopValue = 999999999;
			
			for (int i = 0; i < 5; ++i)
			{
				if(highScores.Length >= i + 1)
				{
					if(highScores[i].score != 0 && highScores[i].score < lowestTopValue)
					{
						lowestTopValue = highScores[i].score;
					}
				}
			}
			
			pool.ReplaceLowestScoreLeaderboard(lowestTopValue);
		},
		ex => pool.ReplaceLowestScoreLeaderboard(0));
	}
	
	public static Entity StartGameOver(this Pool pool)
	{
		var e = pool.CreateEntity()
			.IsGameOver(true);
		
		return e.AddCoroutine(LeaderboardHelper.GetCoroutine(pool, e));
	}
	
	public static Entity CreatePlayer(this Pool pool)
	{
		return pool.CreateEntity()
			.AddPlayer(0.2f)
			.IsFriendly(true)
			.AddHealth(1) // One hit one kill
			.AddPosition(0f, -2.8f)
			.AddSpeed(Vector2.zero)
			.AddSpeedMovement(new Vector2(0.1f, 0.08f))
			.AddScreenLimitedPosition(new Vector4(0.55f, -0.5f, -0.55f, 1.0f))
			.AddResource("Ships/playerShip");
	}
	
	public static Entity CreateEnemyWave(this Pool pool, string waveType)
	{
		var e = pool.CreateEntity();
		return e.AddCoroutine(EnemySpawnerCoroutines.GetCoroutine(waveType, pool, e));
	}
	
	public static Entity CreateEnemyNeutral(this Pool pool, float x, float y, int health, int points, string coroutineType)
	{
		var e = pool.CreateEntity()
			.IsEnemy(true)
			.IsNotFriendly(true)
			.AddPoints(points)
			.AddHealth(health)
			.AddScreenLimitedDestroy(new Vector4(-1.0f, 2.0f, 1.0f, -1.0f))
			.AddCollisionDamage(100)
			.AddPosition(x, y)
			.IsRotateView(true)
			.AddResource("Ships/enemyNeutral");
		
		return e.AddCoroutine(EnemyCoroutines.GetCoroutine(coroutineType, pool, e));
	}
	
	public static Entity CreateEnemySuicide(this Pool pool, float x, float y, int health, int points, string coroutineType)
	{
		var e = pool.CreateEntity()
			.IsEnemy(true)
			.IsNotFriendly(true)
			.AddPoints(points)
			.AddHealth(health)
			.AddScreenLimitedDestroy(new Vector4(-1.0f, 2.0f, 1.0f, -1.0f))
			.AddCollisionDamage(100)
			.AddPosition(x, y)
			.IsRotateView(true)
			.AddResource("Ships/enemySuicide");
		
		return e.AddCoroutine(EnemyCoroutines.GetCoroutine(coroutineType, pool, e));
	}
	
	public static Entity CreateEnemyFighter(this Pool pool, float x, float y, int health, int points, string coroutineType)
	{
		var e = pool.CreateEntity()
			.IsEnemy(true)
			.IsNotFriendly(true)
			.AddPoints(points)
			.AddHealth(health)
			.AddScreenLimitedDestroy(new Vector4(-1.0f, 2.0f, 1.0f, -1.0f))
			.AddCollisionDamage(100)
			.AddPosition(x, y)
			.IsRotateView(true)
			.AddResource("Ships/enemyFighter");
		
		return e.AddCoroutine(EnemyCoroutines.GetCoroutine(coroutineType, pool, e));
	}
	
	public static Entity CreateEnemyBomber(this Pool pool, float x, float y, int health, int points, string coroutineType)
	{
		var e = pool.CreateEntity()
			.IsEnemy(true)
			.IsNotFriendly(true)
			.AddPoints(points)
			.AddHealth(health)
			.AddScreenLimitedDestroy(new Vector4(-1.0f, 2.0f, 1.0f, -1.0f))
			.AddCollisionDamage(100)
			.AddPosition(x, y)
			.IsRotateView(true)
			.AddResource("Ships/enemyBomber");
		
		return e.AddCoroutine(EnemyCoroutines.GetCoroutine(coroutineType, pool, e));
	}
	
	public static Entity CreateBullet(this Pool pool, float x, float y, Vector2 velocity, int damage, int size, bool isFriendly)
	{
		if(size >= 10)
		{
			AudioController.PlayBigShoot();
		}
		else
		{
			AudioController.PlaySmallShoot();
		}
		
		return pool.CreateEntity()
			.IsBullet(true)
			.IsFriendly(isFriendly)
			.IsNotFriendly(!isFriendly)
			.AddPosition(x, y)
			.AddSpeed(velocity)
			.AddScreenLimitedDestroy(new Vector4(-1.0f, 1.0f, 1.0f, -1.0f))
			.AddCollisionDamage(damage)
			.AddResource("Bullets/bullet" + size);
	}
	
	public static void CreateExplosion(this Pool pool, float x, float y)
	{
		const float fraction = (2f * Mathf.PI) / 5f;
		
		for(int i = 0; i < 5; ++i)
		{
			pool.CreateEntity()
				.IsExplosion(true)
				.AddPosition(x + Mathf.Sin(i*fraction) * 0.5f, y + Mathf.Cos(i*fraction) * 0.5f)
				.IsRotateView(true)
				.AddSpeed(new Vector2(Random.Range(-0.001f, 0.001f), Random.Range(-0.001f, 0.001f)))
				.AddResource("Explosion");
		}
		
		pool.CreateEntity()
			.IsExplosion(true)
			.AddPosition(x, y)
			.IsRotateView(true)
			.AddSpeed(new Vector2(Random.Range(-0.001f, 0.001f), Random.Range(-0.001f, 0.001f)))
			.AddResource("Explosion");
	}
	
	public static Entity CreateShield(this Pool pool, Entity owner)
	{
		var e = pool.CreateEntity()
			.AddShield(owner)
			.AddHealth(10000)
			.AddImmortal(Time.time + 9999f)
			.IsFriendly(owner.isFriendly)
			.IsNotFriendly(owner.isNotFriendly)
			.AddResource("Shield");
		
		return e.AddCoroutine(ShieldHelper.GetCoroutine(e));
	}
	
	public static Entity CreateCameraShake(this Pool pool, float duration, float magnitude)
	{
		return pool.CreateEntity()
			.AddCoroutine(CameraShakeSystem.DoCameraShake(Time.time, duration, magnitude).GetEnumerator());
	}
	
	public static Entity CreatePlayerEnergy(this Pool pool)
	{
		return pool.CreateEntity()
			.AddEnergy(100f, 0f)
			.AddPosition(-3.75f, -4.25f);
	}
	
	public static Entity CreateMapBackground(this Pool pool, float y, int mapId = -1)
	{
		return pool.CreateEntity()
			.IsMap(true)
			.AddPosition(-5.5f, y)
			.AddSpeed(pool.mapSpeed.value)
			.AddResource("Background0" + (mapId >= 0 ? mapId : (int)Random.Range(1,8)));
	}
	
	public static Entity CreateScoreUiNumber(this Pool pool, float x, float y, char number)
	{
		return pool.CreateEntity()
			.IsScoreNumScreen(true)
			.AddPosition(x, y)
			.AddResource("Numbers/Number" + number);
	}
	
	public static Entity CreateLifeUiNumber(this Pool pool, float x, float y, char number)
	{
		return pool.CreateEntity()
			.IsLifeNumScreen(true)
			.AddPosition(x, y)
			.AddResource("Numbers/Number" + number);
	}
}
