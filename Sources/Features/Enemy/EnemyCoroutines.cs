using System.Collections;
using UnityEngine;
using Entitas;

public static class EnemyCoroutines
{
	static public IEnumerator GetCoroutine(string type, Pool pool, Entity e)
	{
		switch(type)
		{
			case "GoDownGoDiagonal": return GoDownGoDiagonal(pool, e, Time.time).GetEnumerator();
			case "GoDownGoDiagonalGoUp": return GoDownGoDiagonalGoUp(pool, e, Time.time).GetEnumerator();
			
			case "GoDownShootPlayer": return GoDownShootPlayer(pool, e, Time.time).GetEnumerator();
			case "GoDownShootPlayerGoUp": return GoDownShootPlayerGoUp(pool, e, Time.time).GetEnumerator();
			
			case "GoDownStopShootCircle": return GoDownStopShootCircle(pool, e, Time.time).GetEnumerator();
			case "GoDownWaitShootCircleRepeated": return GoDownWaitShootCircleRepeated(pool, e, Time.time).GetEnumerator();
			
			case "GoDiagonalGoPlayer": return GoDiagonalGoPlayer(pool, e, Time.time).GetEnumerator();
			case "GoDiagonalGoPlayerRepeated": return GoDiagonalGoPlayerRepeated(pool, e, Time.time).GetEnumerator();
		}
		
		return NullCoroutine(pool, e, Time.time).GetEnumerator();
	}
	
	static IEnumerable WaitMs(Entity e, float ms)
	{
		e.ReplaceCoroutineWait(Time.time + (ms / 1000.0f));
		return null;
	}
	
	static IEnumerable NullCoroutine(Pool pool, Entity e, float startTime)
	{
		yield return WaitMs(e, 1);
		e.isDestroy = true;
	}
	
	// ======================================================================
	
	static IEnumerable GoDownGoDiagonal(Pool pool, Entity e, float startTime)
	{
		EnemyCoroutinesHelper.GoDown(e);
		yield return WaitMs(e, 700);
		EnemyCoroutinesHelper.GoDiagonalDown(e);
	}
	
	static IEnumerable GoDownGoDiagonalGoUp(Pool pool, Entity e, float startTime)
	{
		EnemyCoroutinesHelper.GoDown(e);
		yield return WaitMs(e, 700);
		EnemyCoroutinesHelper.GoDiagonalDown(e);
		yield return WaitMs(e, 1500);
		EnemyCoroutinesHelper.GoLateral(e);
		yield return WaitMs(e, 500);
		EnemyCoroutinesHelper.GoUp(e);
	}
	
	static IEnumerable GoDownShootPlayer(Pool pool, Entity e, float startTime)
	{
		EnemyCoroutinesHelper.GoDown(e);
		yield return WaitMs(e, 500);
		
		EnemyCoroutinesHelper.ShootToPlayer(pool, e);
	}
	
	static IEnumerable GoDownShootPlayerGoUp(Pool pool, Entity e, float startTime)
	{
		EnemyCoroutinesHelper.GoDown(e);
		yield return WaitMs(e, 500);
		
		EnemyCoroutinesHelper.ShootToPlayer(pool, e);
		yield return WaitMs(e, 250);
		EnemyCoroutinesHelper.GoDiagonalDown(e);
		yield return WaitMs(e, 500);
		EnemyCoroutinesHelper.GoLateral(e);
		yield return WaitMs(e, 500);
		EnemyCoroutinesHelper.GoUp(e);
	}
	
	static IEnumerable GoDownStopShootCircle(Pool pool, Entity e, float startTime)
	{
		EnemyCoroutinesHelper.GoDown(e);
		yield return WaitMs(e, 750);
		EnemyCoroutinesHelper.GoStop(e);
		
		for (int i = 0; i < 1; ++i)
		{
			EnemyCoroutinesHelper.ShootCircleBullets(pool, e);
			yield return WaitMs(e, 1000);
		}
		
		EnemyCoroutinesHelper.GoDown(e);
	}
	
	static IEnumerable GoDownWaitShootCircleRepeated(Pool pool, Entity e, float startTime)
	{
		EnemyCoroutinesHelper.GoDown(e);
		yield return WaitMs(e, 500);
		EnemyCoroutinesHelper.GoStop(e);
		yield return WaitMs(e, 200);
		
		for (int i = 0; i < 30; ++i)
		{
			EnemyCoroutinesHelper.ShootCircleBullet(pool, e, i, 15);
			yield return WaitMs(e, 100);
		}
		
		yield return WaitMs(e, 500);
		EnemyCoroutinesHelper.GoDown(e);
	}
	
	static IEnumerable GoDiagonalGoPlayer(Pool pool, Entity e, float startTime)
	{
		EnemyCoroutinesHelper.GoDiagonalDown(e);
		yield return WaitMs(e, 800);
		EnemyCoroutinesHelper.GoToPlayer(pool, e);
	}
	
	static IEnumerable GoDiagonalGoPlayerRepeated(Pool pool, Entity e, float startTime)
	{
		EnemyCoroutinesHelper.GoDiagonalDown(e);
		yield return WaitMs(e, 800);
		EnemyCoroutinesHelper.GoToPlayer(pool, e);
		yield return WaitMs(e, 250);
		EnemyCoroutinesHelper.GoToPlayer(pool, e);
		yield return WaitMs(e, 250);
		EnemyCoroutinesHelper.GoToPlayer(pool, e);
	}
}
