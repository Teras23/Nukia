using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite icon;
	public Sprite iconBlack;
	public Text text;
	public GameObject slider;
	public GameObject buyButton;
	public float completeTime;
	public int cost;
	public int damage;
	private int amount;

	void Start() {
		buyButton.GetComponent<Button>().onClick.AddListener(OnBuy);
	}

	void Update() {
		text.text = "You own: " + amount;
	}

	void FixedUpdate() {
		if(amount > 0) {
			if(slider.GetComponent<Slider>().value == 1) {
				slider.GetComponent<Slider>().value = 0;
				GameController.game.Damage(damage * amount);
				print("dmg");
			}
			slider.GetComponent<Slider>().value += 0.05f / completeTime;
		}
	}

	void OnBuy() {
		print("buy");
		if(GameController.game.RemoveMoney(cost)) {
			amount++;
		}
	}
}
