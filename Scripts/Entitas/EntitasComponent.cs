using Entitas;
using UnityEngine;

public class EntitasComponent : MonoBehaviour, IEntitasComponent
{
	private Entity _entity;
	
	public Entity GetEntity()
	{
		return _entity;
	}
	
	public void SetEntity(Entity entity)
	{
		_entity = entity;
	}
	
	public void FlagAsDestroy()
	{
		if(_entity != null)
		{
			_entity.isDestroy = true;
		}
	}
}
