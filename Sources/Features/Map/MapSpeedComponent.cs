using UnityEngine;
using Entitas;
using Entitas.CodeGenerator;

[SingleEntity]
public class MapSpeedComponent : IComponent
{
	public Vector2 value;
}
