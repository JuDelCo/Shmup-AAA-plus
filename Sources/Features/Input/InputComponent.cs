using Entitas;

public class InputComponent : IComponent
{
	public InputButton value;
}

public enum InputButton
{
	Left,
	Right,
	Up,
	Down,
	Fire,
	Escape,
	Delete,
	Return
}
