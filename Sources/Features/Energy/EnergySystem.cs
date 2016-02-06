using UnityEngine;
using Entitas;

public class EnergySystem : IExecuteSystem, ISetPool
{
	Group _energy;
	GroupObserver _observer;
	
	public void SetPool(Pool pool)
	{
		_energy = pool.GetGroup(Matcher.AllOf(Matcher.Energy));
		_observer = _energy.CreateObserver(GroupEventType.OnEntityAdded);
	}
	
	public void Execute()
	{
		foreach (Entity en in _observer.collectedEntities)
		{
			char energyLevel = '6';
			
				 if(en.energy.level <=  0f) energyLevel = '0';
			else if(en.energy.level <= 17f) energyLevel = '1';
			else if(en.energy.level <= 34f) energyLevel = '2';
			else if(en.energy.level <= 50f) energyLevel = '3';
			else if(en.energy.level <= 75f) energyLevel = '4';
			else if(en.energy.level <= 99f) energyLevel = '5';
			
			en.ReplaceResource("Energy/Energy" + energyLevel);
		}
		_observer.ClearCollectedEntities();
		
		var e = _energy.GetSingleEntity();
		
		if(e.energy.depletedUntil > Time.time || e.energy.level >= 100f)
		{
			return;
		}
		
		var energyRefill = (e.energy.level <= 30f ? 0.3f : (e.energy.level <= 60f ? 0.5f : 1.0f));
		
		e.ReplaceEnergy(Mathf.Clamp(e.energy.level + energyRefill, 0f, 100f), 0f);
	}
}
