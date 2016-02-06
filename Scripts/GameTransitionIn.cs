using UnityEngine;

public class GameTransitionIn : MonoBehaviour
{
	float startTransition;
	
	void Start()
	{
		startTransition = Time.time;
	}
	
	void Update()
	{
		var elapsed = Time.time - startTransition;
		
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, 1f - elapsed);
		
		if(elapsed > 1f)
		{
			Object.Destroy(gameObject);
		}
	}
}
