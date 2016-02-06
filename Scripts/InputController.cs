using UnityEngine;
using GamepadInput;

public class InputController : MonoBehaviour
{
	const float axisDeadZone = 0.4f;
	
	void Update()
	{
		if(Input.GetKey("left") || GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any).x <= -axisDeadZone)
		{
			Pools.pool.CreateEntity().AddInput(InputButton.Left);
        }
		else if(Input.GetKey("right") || GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any).x >= axisDeadZone)
		{
			Pools.pool.CreateEntity().AddInput(InputButton.Right);
		}
		
		if(Input.GetKey("up") || GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any).y >= axisDeadZone)
		{
			Pools.pool.CreateEntity().AddInput(InputButton.Up);
		}
		else if(Input.GetKey("down") || GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any).y <= -axisDeadZone)
		{
			Pools.pool.CreateEntity().AddInput(InputButton.Down);
		}
		
		if(Input.GetKey(KeyCode.X) || GamePad.GetButton(GamePad.Button.A, GamePad.Index.Any))
		{
			Pools.pool.CreateEntity().AddInput(InputButton.Fire);
        }
		
		if(Input.GetKeyDown("escape") || GamePad.GetButton(GamePad.Button.Back, GamePad.Index.Any))
		{
			Pools.pool.CreateEntity().AddInput(InputButton.Escape);
        }
		
		if(Input.GetKey("return"))
		{
			Pools.pool.CreateEntity().AddInput(InputButton.Return);
        }
		else if(Input.GetKey("delete"))
		{
			Pools.pool.CreateEntity().AddInput(InputButton.Delete);
        }
		
		if(! string.IsNullOrEmpty(Input.inputString))
		{
			Pools.pool.CreateEntity().AddInputString(Input.inputString);
		}
	}
}
