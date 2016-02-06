using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class RenderPositionSystem : IReactiveSystem, IEnsureComponents
{
	// View too because maybe a entity change his view but doesn't move (EnergyBar!)
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.View); } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }
	
	// View component could be destroyed or non existent so we ensure it
	public IMatcher ensureComponents { get { return Matcher.View; } }
	
	public void Execute(List<Entity> entities)
	{
		foreach (var e in entities)
		{
			var pos = e.position;
			
			e.view.gameObject.transform.position = new Vector3(pos.x, pos.y, 0f);
		}
	}
}
