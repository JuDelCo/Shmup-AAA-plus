using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;

public class GameController : MonoBehaviour
{
	Systems _systems;
	
	void Start()
	{
		Pools.pool.DestroyAllEntities();
		
		_systems = createSystems(Pools.pool);
		_systems.Start();
		
		Pools.pool.StartGame();
	}
	
	void FixedUpdate()
	{
		_systems.Execute();
	}
	
	Systems createSystems(Pool pool)
	{
		#if (UNITY_EDITOR)
		return new DebugSystems()
        #else
        return new Systems()
        #endif
			// Input
			.Add(pool.CreateProcessInputSystem())
			// Update
			.Add(pool.CreateEnemySpawnerSystem())
			.Add(pool.CreatePlayerSystem())
			.Add(pool.CreateEnergySystem())
			.Add(pool.CreateCoroutineSystem())
			.Add(pool.CreateMoveSystem())
			.Add(pool.CreateShieldSystem())
			// Collisions
			.Add(pool.CreateScreenCollisionPositionSystem())
			.Add(pool.CreateScreenCollisionDestroySystem())
			.Add(pool.CreateCollisionDamageSystem())
			// Game Logic
			.Add(pool.CreateImmortalSystem())
			.Add(pool.CreateFlashSystem())
			.Add(pool.CreateDamageSystem())
			.Add(pool.CreateHealthSystem())
			.Add(pool.CreateLifeSystem())
			.Add(pool.CreateScoreSystem())
			// Render
			.Add(pool.CreateLeaderboardSystem())
			.Add(pool.CreateCameraShakeSystem())
			.Add(pool.CreateMapSystem())
			.Add(pool.CreateScoreRenderSystem())
			.Add(pool.CreateLifeRenderSystem())
			.Add(pool.CreateRemoveViewSystem())
			.Add(pool.CreateAddViewSystem())
			.Add(pool.CreateRenderPositionSystem())
			.Add(pool.CreateRotateViewSystem())
			// Cleanup
			.Add(pool.CreateDestroySystem());
	}
}
