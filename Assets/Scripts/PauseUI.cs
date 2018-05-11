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
		commentText.text = _active ?
			"Press 'p' to continue" :
			"Press 'p' to pause";
	}
}
