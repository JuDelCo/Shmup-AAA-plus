using System;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class AddViewSystem : IMultiReactiveSystem, IEnsureComponents
{
	public IMatcher[] triggers { get { return new [] {
		Matcher.Resource,
		Matcher.RegenerateView
	}; } }
	
	public GroupEventType[] eventTypes { get { return new [] {
		GroupEventType.OnEntityAdded,
		GroupEventType.OnEntityAdded
	}; } }
	
	public IMatcher ensureComponents { get { return Matcher.Resource; } }
	
	readonly Transform _viewContainer = new GameObject("Views").transform;
	
	public void Execute(List<Entity> entities)
	{
		foreach (var e in entities)
		{
			var res = Resources.Load<GameObject>(e.resource.name);
			GameObject gameObject = null;
			
			try
			{
				gameObject = UnityEngine.Object.Instantiate(res);
				
				if(! gameObject.GetComponent<EntitasComponent>())
				{
					gameObject.AddComponent<EntitasComponent>();
				}
			}
			catch (Exception)
			{
				Debug.Log("Cannot instantiate " + res);
			}
			
			if(e.isRegenerateView)
			{
				e.isRegenerateView = false;
			}
			
			if(gameObject != null)
			{
				if(gameObject.GetComponent<IEntitasComponent>() != null)
				{
					gameObject.GetComponent<IEntitasComponent>().SetEntity(e);
				}
				
				gameObject.transform.parent = _viewContainer;
				
				e.AddView(gameObject);
			}
		}
	}
}
