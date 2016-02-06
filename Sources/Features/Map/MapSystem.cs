using UnityEngine;
using Entitas;

public class MapSystem : IStartSystem, IExecuteSystem, ISetPool
{
	Pool _pool;
	Group _backgrounds;
	Group _gameover;
	float startTime;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
		_backgrounds = _pool.GetGroup(Matcher.AllOf(Matcher.Map, Matcher.Position));
		_gameover = pool.GetGroup(Matcher.GameOver);
	}
	
	public void Start()
	{
		startTime = Time.time;
	}
	
	public void Execute()
	{
		float maxHeightPos = 0f;
		bool createNewMap = false;
		
		if(_gameover.Count == 0)
		{
			var elapsed = Time.time - startTime;
			var percentageAccel = Mathf.Clamp(elapsed / (4f*60f), 0f, 1f); // 4m to max speed
			
			_pool.ReplaceMapSpeed(new Vector2(0f, -0.01f - (percentageAccel * 0.25f))); // -0.25f max speed
		}
		
		foreach (var e in _backgrounds.GetEntities())
		{
			e.ReplaceSpeed(_pool.mapSpeed.value);
			
			if(e.position.y > maxHeightPos)
			{
				maxHeightPos = e.position.y;
			}
			
			if(e.position.y <= -5f)
			{
				createNewMap = true;
				e.isDestroy = true;
			}
		}
		
		if(createNewMap)
		{
			foreach (var e in _backgrounds.GetEntities())
			{
				// Regenerate view needed to sync animation of Tiled2Unity prefabs
				e.isRegenerateView = true;
			}
			
			_pool.CreateMapBackground(maxHeightPos + 8f);
		}
	}
}
