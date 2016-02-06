using System.Collections;
using UnityEngine;
using Entitas;

public static class EnemySpawnerCoroutines
{
	static public IEnumerator GetCoroutine(string type, Pool pool, Entity e)
	{
		switch(type)
		{
			case "ThreeNeutralsEasy": return ThreeNeutralsEasy(pool, e, Time.time).GetEnumerator();
			case "FourNeutralsNormal": return FourNeutralsNormal(pool, e, Time.time).GetEnumerator();
			
			case "ThreeFightersEasy": return ThreeFightersEasy(pool, e, Time.time).GetEnumerator();
			case "FourFightersNormal": return FourFightersNormal(pool, e, Time.time).GetEnumerator();
			case "ThreeFightersHard": return ThreeFightersHard(pool, e, Time.time).GetEnumerator();
			
			case "TwoBombersEasy": return TwoBombersEasy(pool, e, Time.time).GetEnumerator();
			case "TwoBombersHard": return TwoBombersHard(pool, e, Time.time).GetEnumerator();
			
			case "ThreeSuicidesEasy": return ThreeSuicidesEasy(pool, e, Time.time).GetEnumerator();
			case "FourSuicidesNormal": return FourSuicidesNormal(pool, e, Time.time).GetEnumerator();
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
	
	static IEnumerable ThreeNeutralsEasy(Pool pool, Entity e, float startTime)
	{
		var posX = Random.Range(-4.25f, 4.25f);
		
		for (int i = 0; i < 3; ++i)
		{
			pool.CreateEnemyNeutral(posX, 5f, 10, 50, "GoDownGoDiagonal");
			yield return WaitMs(e, 300);
		}
	}
	
	static IEnumerable FourNeutralsNormal(Pool pool, Entity e, float startTime)
	{
		var posX = Random.Range(-4.25f, 4.25f);
		
		for (int i = 0; i < 4; ++i)
		{
			pool.CreateEnemyNeutral(posX, 5f, 10, 50, "GoDownGoDiagonalGoUp");
			yield return WaitMs(e, 300);
		}
	}
	
	static IEnumerable ThreeFightersEasy(Pool pool, Entity e, float startTime)
	{
		var posX = Random.Range(-4.25f, 4.25f);
		
		for (int i = 0; i < 3; ++i)
		{
			pool.CreateEnemyFighter(posX, 5f, 10, 75, "GoDownShootPlayer");
			yield return WaitMs(e, 300);
		}
	}
	
	static IEnumerable FourFightersNormal(Pool pool, Entity e, float startTime)
	{
		var posX = Random.Range(-4.25f, 4.25f);
		
		for (int i = 0; i < 4; ++i)
		{
			pool.CreateEnemyFighter(posX, 5f, 10, 75, "GoDownShootPlayerGoUp");
			yield return WaitMs(e, 300);
		}
	}
	
	static IEnumerable ThreeFightersHard(Pool pool, Entity e, float startTime)
	{
		float posX;
		
		for (int i = 0; i < 3; ++i)
		{
			posX = Random.Range(-4.25f, 4.25f);
			pool.CreateEnemyFighter(posX, 5f, 10, 75, "GoDownShootPlayerGoUp");
			yield return WaitMs(e, 300);
		}
	}
	
	static IEnumerable TwoBombersEasy(Pool pool, Entity e, float startTime)
	{
		var posX = Random.Range(-4.25f, 2.75f);
		
		pool.CreateEnemyBomber(posX, 5f, 20, 50, "GoDownStopShootCircle");
		pool.CreateEnemyBomber(posX + 1.5f, 5f, 20, 100, "GoDownStopShootCircle");
		yield return null;
	}
	
	static IEnumerable TwoBombersHard(Pool pool, Entity e, float startTime)
	{
		var posX = Random.Range(-4.25f, 2.75f);
		
		pool.CreateEnemyBomber(posX, 5f, 20, 50, "GoDownWaitShootCircleRepeated");
		pool.CreateEnemyBomber(posX + 1.5f, 5f, 20, 100, "GoDownWaitShootCircleRepeated");
		yield return null;
	}
	
	static IEnumerable ThreeSuicidesEasy(Pool pool, Entity e, float startTime)
	{
		var posX = Random.Range(-4.25f, 4.25f);
		
		for (int i = 0; i < 3; ++i)
		{
			pool.CreateEnemySuicide(posX, 5f, 10, 75, "GoDiagonalGoPlayer");
			yield return WaitMs(e, 300);
		}
	}
	
	static IEnumerable FourSuicidesNormal(Pool pool, Entity e, float startTime)
	{
		pool.CreateEnemySuicide(-3.5f, 5f, 10, 75, "GoDiagonalGoPlayerRepeated");
		yield return WaitMs(e, 200);
		pool.CreateEnemySuicide(1.5f, 5f, 10, 75, "GoDiagonalGoPlayerRepeated");
		yield return WaitMs(e, 300);
		pool.CreateEnemySuicide(-1.5f, 5f, 10, 75, "GoDiagonalGoPlayerRepeated");
		yield return WaitMs(e, 200);
		pool.CreateEnemySuicide(3.5f, 5f, 10, 75, "GoDiagonalGoPlayerRepeated");
		yield return WaitMs(e, 300);
	}
}
