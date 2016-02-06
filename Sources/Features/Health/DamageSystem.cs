using System.Collections.Generic;
using Entitas;

public class DamageSystem : IReactiveSystem, IEnsureComponents
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Health, Matcher.Damage); } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }
	
	// Maybe entity was destroyed so we ensure it still have damage
	public IMatcher ensureComponents { get { return Matcher.Damage; } }
	
	public void Execute(List<Entity> entities)
	{
		foreach (var e in entities)
		{
			if(! e.hasImmortal)
			{
				e.ReplaceHealth(e.health.value - e.damage.value);
				
				AudioController.PlayHit();
			}
			
			e.RemoveDamage();
		}
	}
}
