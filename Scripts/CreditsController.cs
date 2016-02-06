using UnityEngine;
using GamepadInput;

public class CreditsController : MonoBehaviour
{
	float startTime;
	bool skipRequested;
	
	void Start()
	{
		startTime = Time.time;
		skipRequested = false;
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.X) || GamePad.GetButton(GamePad.Button.A, GamePad.Index.Any))
		{
			skipRequested = true;
		}
		
		if(skipRequested && Time.time - startTime > 2.5f)
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
