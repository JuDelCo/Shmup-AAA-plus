using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class RotateViewSystem : IReactiveSystem, IEnsureComponents
{
	public IMatcher trigger { get { return Matcher.AllOf(Matcher.Speed, Matcher.RotateView); } }
	
	public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }
	
	// View component could be destroyed or non existent so we ensure it
	public IMatcher ensureComponents { get { return Matcher.View; } }
	
	// Diagonal rotations don't look very well in this game so...
	static readonly int[] angleValues = { 0, /*45,*/ 90, /*135,*/ 180, /*225,*/ 270, /*315,*/ 360 };
	
	public void Execute(List<Entity> entities)
	{
		foreach (var e in entities)
		{
			float searchValue = Vector2.Angle(e.speed.value, Vector2.down);
			Vector3 cross = Vector3.Cross(e.speed.value, Vector2.down);
			
			if(cross.z > 0)
			{
				searchValue = 360 - searchValue;
			}
			
			var currentNearest = angleValues[0];
			float currentDifference = Mathf.Abs(currentNearest - searchValue);
			
			for(int i = 1; i < angleValues.Length; ++i)
			{
			    float diff = Mathf.Abs(angleValues[i] - searchValue);
			    
			    if(diff < currentDifference)
			    {
			        currentDifference = diff;
			        currentNearest = angleValues[i];
			    }
			}
			
			e.view.gameObject.transform.rotation = Quaternion.AngleAxis(currentNearest, Vector3.forward);
		}
	}
}
