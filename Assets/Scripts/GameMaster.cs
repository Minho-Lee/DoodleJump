using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;


	void Awake () 
	{
		// Ensure Singleton class
		if (gm == null)
		{
			gm = this;
		}
	}

	public static void KillPlayer(Player player)
	{
		Destroy (player.gameObject);
	}
}
