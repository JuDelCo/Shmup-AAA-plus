using UnityEngine;

public static class PositionComponentExtensions
{
	public static Vector2 ToVector2(this PositionComponent c)
	{
		return new Vector2(c.x, c.y);
	}
}
