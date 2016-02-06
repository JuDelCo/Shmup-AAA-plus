using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CameraShakeSystem : IReactiveSystem, ISetPool
{
	public IMatcher trigger { get { return Matcher.Killed; } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }
	
	Pool _pool;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
	}
	
	public void Execute(List<Entity> entities)
	{
		if(entities[0].hasPlayer)
		{
			_pool.CreateCameraShake(0.5f, 0.2f);
		}
		else
		{
			_pool.CreateCameraShake(0.15f, 0.05f);
		}
	}
	
	public static IEnumerable DoCameraShake(float startTime, float duration, float magnitude)
	{
		var camera = GameObject.Find("Game Camera");
		
		float randomStart = Random.value + 10.0f;
		
		while(startTime + duration >= Time.time)
		{
			float percentComplete = (Time.time - startTime) / duration;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
			float alpha = randomStart * percentComplete;
			
			// Noise-based
			float x = (Mathf.PerlinNoise(alpha, 0.0f) * 2.0f - 1.0f) * magnitude * damper * 1.7f;
			float y = (Mathf.PerlinNoise(0.0f, alpha) * 2.0f - 1.0f) * magnitude * damper * 1.7f;
			
			// Random-based
			//float x = (Random.value * 2.0f - 1.0f) * magnitude * damper;
			//float y = (Random.value * 2.0f - 1.0f) * magnitude * damper;
			
			camera.transform.position = new Vector3(x, y, -5f);
			
			yield return null;
		}
		
		camera.transform.position = new Vector3(0f, 0f, -5f);
	}
}
