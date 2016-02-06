using UnityEngine;
using Entitas;

public class PlayerSystem : IExecuteSystem, ISetPool
{
	Pool _pool;
	Group _playerGroup;
	Group _energy;
	float timeLastShoot;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
		_playerGroup = _pool.GetGroup(Matcher.Player);
		_energy = _pool.GetGroup(Matcher.Energy);
	}
	
	public void Execute()
	{
		var player = _playerGroup.GetSingleEntity();
		
		if(player == null)
		{
			if(_pool.life.value < 0)
			{
				return;
			}
			
			// Resurrect !
			player = _pool.CreatePlayer();
			_pool.CreateShield(player);
			_energy.GetSingleEntity().ReplaceEnergy(100f, 0f);
		}
		
		var maxSpeed = player.speedMovement.value;
		var newSpeed = Vector2.zero;
		
		if(_pool.isKeyLeftPressed)
		{
			newSpeed.x = -maxSpeed.x;
		}
		else if(_pool.isKeyRightPressed)
		{
			newSpeed.x = maxSpeed.x;
		}
		
		if(_pool.isKeyUpPressed)
		{
			newSpeed.y = maxSpeed.y;
		}
		else if(_pool.isKeyDownPressed)
		{
			newSpeed.y = -maxSpeed.y;
		}
		
		if(_pool.isKeyFirePressed)
		{
			newSpeed *= 0.5f;
		}
		
		player.ReplaceSpeed(newSpeed);
		
		// Shoot logic
		if(_pool.isKeyFirePressed && timeLastShoot + player.player.shootSpeed < Time.time)
		{
			Entity energy = _energy.GetSingleEntity();
			float newEnergy = 0f;
			
			if(energy.energy.level <= 0f)
			{
				_pool.CreateBullet(player.position.x, player.position.y + 0.2f, new Vector2(0f, (newSpeed.y / 3f) + 0.2f), 5, 8, true);
			}
			else
			{
				_pool.CreateBullet(player.position.x, player.position.y + 0.2f, new Vector2(0f, (newSpeed.y / 3f) + 0.2f), 10, 10, true);
				newEnergy = Mathf.Clamp(energy.energy.level - 9f, 0f, 100f);
			}
			
			energy.ReplaceEnergy(newEnergy, newEnergy <= 0f ? Time.time + 1.0f : 0f);
			
			if(newEnergy >= 50f)
			{
				timeLastShoot = Time.time - (player.player.shootSpeed / 2f);
			}
			else
			{
				timeLastShoot = Time.time;
			}
			
			// Recoil
			player.ReplacePosition(player.position.x, player.position.y - 0.05f);
		}
	}
}
