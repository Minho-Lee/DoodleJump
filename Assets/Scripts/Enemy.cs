using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public void DamageEnemy(int damage)
	{
		// TODO: If it takes more than one bullet to kill enemy, implement health
		GameMaster.KillEnemy (this);
	}
	private void OnCollisionEnter2D(Collision2D _colInfo)
	{
		string name = _colInfo.collider.name;
		if (name == "Doodler")
		{
			Player _player = _colInfo.collider.GetComponent <Player> ();
			if (_player != null)
			{
				GameMaster.KillPlayer (_player);
			}
			DamageEnemy (100);
		}
		else if (name.Contains ("Bullet1"))
		{
			DamageEnemy (100);
			Destroy (_colInfo.gameObject);
		}
	}
}
