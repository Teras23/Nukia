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
		while(health <= 0) {
			health += maxHealth;
			OnDeath();
		}
	}

	public void OnDeath() {
		GameController.game.AddMoney(value);
	}

	public int getHealth() {
		return health;
	}

	public void setHealth(int h) {
		health = h;
	}
}
