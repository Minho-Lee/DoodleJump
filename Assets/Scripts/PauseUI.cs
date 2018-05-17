using UnityEngine.UI;
using UnityEngine;

public class PauseUI : MonoBehaviour {

	[SerializeField]
	Text pauseText;

	[SerializeField]
	Text commentText;

	void Awake()
	{
		GameMaster.gm.onTogglePauseMenu += OnPauseMenuToggle;
	}
	void Start () {
		if (pauseText == null)
		{
			Debug.LogError ("pauseText reference is null!");
		}
		if (commentText == null)
		{
			Debug.LogError ("commentText reference is null!");
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnPauseMenuToggle(bool _active)
	{
		if (Application.platform.ToString () == "OSXEditor" || 
			Application.platform.ToString () == "OSXPlayer")
		{
			commentText.text = _active ?
				"Press 'p' to continue" :
				"Press 'p' to pause";	
		}
		else
		{
			commentText.text = _active ?
				"Tap here to continue" :
				"Tap here to pause";
		}

	}
}
