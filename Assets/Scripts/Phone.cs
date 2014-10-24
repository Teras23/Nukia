using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

	public string phoneName;
	public int maxHealth;
	[HideInInspector]
	public int health;
	public int value;

	void Start() {
		health = maxHealth;
	}

	public void Damage(int a, Game_Controller g) {
		health -= a;
		if(health <= 0)
			OnDeath(g);
	}

	public void OnDeath(Game_Controller g) {
		g.money += value;
		Start();
	}
}
