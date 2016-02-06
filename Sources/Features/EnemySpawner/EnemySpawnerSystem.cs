using UnityEngine;
using Entitas;

public class EnemySpawnerSystem : IStartSystem, IExecuteSystem, ISetPool
{
	Pool _pool;
	Group _gameover;
	
	// A system shouldn't have state ! My bad...
	float timeStart;
	float timeLastWave;
	float timeToNextWave;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
		_gameover = pool.GetGroup(Matcher.GameOver);
	}
	
	public void Start()
	{
		timeStart = Time.time;
		timeLastWave = timeStart;
		timeToNextWave = 3.0f;
	}
	
	public void Execute()
	{
		if(_gameover.Count > 0)
		{
			return;
		}
		
		if(timeLastWave + timeToNextWave < Time.time)
		{
			_pool.CreateEnemyWave(GetNextWaveType());
			
			if(Random.value < 0.07f)
			{
				return; // Another free wave! Bad luck!
			}
			
			UpdateTimeToNextWave();
			timeLastWave = Time.time;
		}
	}
	
	int GetDifficultyLevel()
	{
		var timeSinceStart = Time.time - timeStart;
		
		if(timeSinceStart >= (4*60)) return 5; // 4m
		if(timeSinceStart >= (3*60)) return 4; // 3m
		if(timeSinceStart >= (2*60)) return 3; // 2m
		if(timeSinceStart >= (1*60)) return 2; // 1m
		if(timeSinceStart >= (1*30)) return 1; // 30s
		
		return 0;
	}
	
	string GetRandomWaveIn(string[] waves)
	{
		return waves[Mathf.Min(waves.Length - 1, Random.Range(0, waves.Length))];
	}
	
	string GetNextWaveType()
	{
		var difficultyLevel = GetDifficultyLevel();
		
		string[] waves = { "ThreeNeutralsEasy","ThreeFightersEasy","ThreeSuicidesEasy" };
		
			 if(difficultyLevel == 5) waves = new []{ "ThreeFightersHard","TwoBombersEasy","TwoBombersHard","FourSuicidesNormal" };
		else if(difficultyLevel == 4) waves = new []{ "ThreeFightersHard","TwoBombersHard","FourSuicidesNormal" };
		else if(difficultyLevel == 3) waves = new []{ "FourNeutralsNormal","FourFightersNormal","TwoBombersEasy","FourSuicidesNormal" };
		else if(difficultyLevel == 2) waves = new []{ "FourNeutralsNormal","FourFightersNormal","TwoBombersEasy","FourSuicidesNormal" };
		else if(difficultyLevel == 1) waves = new []{ "ThreeNeutralsEasy","ThreeFightersEasy","TwoBombersEasy","ThreeSuicidesEasy" };
		
		return GetRandomWaveIn(waves);
	}
	
	void UpdateTimeToNextWave()
	{
		var difficultyLevel = GetDifficultyLevel();
		
		timeToNextWave = 2.5f; // base
		
		     if(difficultyLevel == 5) timeToNextWave -= 0.8f;
		else if(difficultyLevel == 4) timeToNextWave -= 0.6f;
		else if(difficultyLevel == 3) timeToNextWave -= 0.4f;
		else if(difficultyLevel == 2) timeToNextWave -= 0.2f;
		else if(difficultyLevel == 1) timeToNextWave -= 0.1f;
	}
}
