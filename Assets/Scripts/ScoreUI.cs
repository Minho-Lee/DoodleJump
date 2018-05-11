using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour {

	private Text ScoreText;
	// Use this for initialization
	void Awake () {
		ScoreText = GetComponent <Text> ();
	}

	// Update is called once per frame
	void Update () {
		ScoreText.text = "SCORE: " + GameMaster.Score.ToString ();
	}
}
