using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

	public string phoneName;
	public Sprite image;
	public int value;
	public int maxHealth;
	public int health;

	void Start() {
		health = maxHealth;
	}

	public void Damage(int a) {
		health -= a;
		if(health <= 0)
			OnDeath();
	}

	public void OnDeath() {
		GameController.game.AddMoney(value);
		Start();
	}

	public int getHealth() {
		return health;
	}

	public void setHealth(int h) {
		health = h;
	}
}
