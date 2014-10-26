using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

	public string phoneName;
	public Sprite image;
	public int value;
	public int maxHealth;
	public int health;

	void Awake() {
		health = maxHealth;
	}

	public void Damage(int a, Game_Controller g) {
		health -= a;
		if(health <= 0)
			OnDeath(g);
	}

	public void OnDeath(Game_Controller g) {
		g.money += value;
		Awake();
	}

	public int getHealth() {
		return health;
	}

	public void setHealth(int h) {
		health = h;
	}
}
