using UnityEngine;
using Entitas;

public class ProcessInputSystem : IStartSystem, IExecuteSystem, ISetPool
{
	Pool _pool;
	Group _inputGroup;
	
	public void SetPool(Pool pool)
	{
		_pool = pool;
		_inputGroup = pool.GetGroup(Matcher.Input);
	}
	
	public void Start()
	{
		ResetInputState();
	}
	
	public void Execute()
	{
		ResetInputState();
		
		if(_inputGroup.Count > 0)
		{
			foreach (var e in _inputGroup.GetEntities())
			{
				switch (e.input.value)
				{
					case InputButton.Left:
						_pool.isKeyLeftPressed = true;
						break;
					case InputButton.Right:
						_pool.isKeyRightPressed = true;
						break;
					case InputButton.Up:
						_pool.isKeyUpPressed = true;
						break;
					case InputButton.Down:
						_pool.isKeyDownPressed = true;
						break;
					case InputButton.Fire:
						_pool.isKeyFirePressed = true;
						break;
					case InputButton.Delete:
						_pool.isKeyDeletePressed = true;
						break;
					case InputButton.Return:
						_pool.isKeyReturnPressed = true;
						break;
					case InputButton.Escape:
						_pool.isKeyEscapePressed = true;
						if(_pool.GetGroup(Matcher.GameOver).Count > 0)
						{
							Application.LoadLevel("Credits");
						}
						else
						{
							Application.LoadLevel("MainMenu");
						}
						break;		
				}
				
				_pool.DestroyEntity(e);
			}
		}
	}
	
	void ResetInputState()
	{
		_pool.isKeyLeftPressed = false;
		_pool.isKeyRightPressed = false;
		_pool.isKeyUpPressed = false;
		_pool.isKeyDownPressed = false;
		_pool.isKeyFirePressed = false;
		
		_pool.isKeyDeletePressed = false;
		_pool.isKeyReturnPressed = false;
		_pool.isKeyEscapePressed = false;
	}
}
