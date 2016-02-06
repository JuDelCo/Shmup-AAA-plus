using UnityEngine;
using GamepadInput;

public class MainMenuController : MonoBehaviour
{
	float startTime;
	float startTransition;
	GameObject transition;
	
	void Start()
	{
		Screen.SetResolution(480, 432, false);
		
		startTime = Time.time;
		startTransition = 0f;
		transition = GameObject.Find("Transition");
	}
	
	void Update()
	{
		if(startTransition <= 0f && (Input.GetKeyDown(KeyCode.X) || GamePad.GetButton(GamePad.Button.A, GamePad.Index.Any)))
		{
			startTransition = Time.time + Mathf.Max(2.0f - (Time.time - startTime), 0f);
			GameObject.Find("Text").SetActive(false);
		}
		
		if(startTransition > 0f)
		{
			var elapsed = Time.time - startTransition;
			
			transition.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, elapsed);
			
			if(elapsed > 1f)
			{
				Application.LoadLevel("Game");
			}
		}
	}
}
