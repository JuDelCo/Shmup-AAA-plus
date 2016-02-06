using UnityEngine;
using Entitas;

public class EnemyTestSpawnerSystem : IExecuteSystem, ISetPool
{
	const float timeBetweenWaves = 3.0f;
	float timeLastWave = 1.0f;
	//Pool _pool;
	
	public void SetPool(Pool pool)
	{
		//_pool = pool;
	}
	
	public void Execute()
	{
		if(timeLastWave + timeBetweenWaves < Time.time)
		{
			GenerateRandomEnemy();
		}
	}
	
	void GenerateRandomEnemy()
	{
		//_pool.CreateEnemyNeutral((int)Random.Range(-4.5f, 4.5f), 5f, 50, 50, "GoDown");
		//_pool.CreateEnemySuicide((int)Random.Range(-4.5f, 4.5f), 5f, 50, 50, "GoDiagonalGoPlayer");
		//_pool.CreateEnemyFighter((int)Random.Range(-4.5f, 4.5f), 5f, 50, 50, "GoDownShootPlayer");
		//_pool.CreateEnemyBomber ((int)Random.Range(-4.5f, 4.5f), 5f, 50, 50, "GoDiagonalStopShootCircle");
		
		timeLastWave = Time.time;
	}
}
