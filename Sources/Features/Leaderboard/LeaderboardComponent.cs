using System.Collections;
using UnityEngine;
using Entitas;
using Entitas.CodeGenerator;

[SingleEntity]
public class LeaderboardComponent : IComponent
{
	public string userName;
}

public static class LeaderboardHelper
{
	static public IEnumerator GetCoroutine(Pool pool, Entity e)
	{
		return LeaderboardCoroutine(pool, e, Time.time).GetEnumerator();
	}
	
	static IEnumerable WaitMs(Entity e, float ms)
	{
		e.ReplaceCoroutineWait(Time.time + (ms / 1000.0f));
		return null;
	}
	
	static IEnumerable LeaderboardCoroutine(Pool pool, Entity e, float startTime)
	{
		var audioSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
		
		for (int i = 3; i >= 0; --i)
		{
			audioSource.volume = 0.25f * (i / 3.0f);
			yield return WaitMs(e, 200);
		}
		
		audioSource.Stop();
		if(pool.hasLowestScoreLeaderboard && pool.lowestScoreLeaderboard.value <= pool.score.value)
		{
			audioSource.clip = (AudioClip)Resources.Load("Music/GameOverHighScore");
		}
		else
		{
			audioSource.clip = (AudioClip)Resources.Load("Music/GameOver");
		}
		audioSource.volume = 0.25f;
		audioSource.loop = false;
		audioSource.Play();
		
		yield return WaitMs(e, 1200);
		
		e.AddLeaderboard("");
		e.AddResource("Leaderboard");
	}
}
