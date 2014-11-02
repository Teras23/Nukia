using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite icon;
	public Sprite iconBlack;
	public float completeTime;
	public int cost;
	public int damage;
	private int amount;
	[HideInInspector]
	public Text text;
	[HideInInspector]
	public GameObject slider;
	[HideInInspector]
	public GameObject buyButton;
	[HideInInspector]
	public Image image;

	void Start() {
		buyButton.GetComponent<Button>().onClick.AddListener(OnBuy);
	}

	void Update() {
		text.text = "You own: " + amount;
	}

	void OnBuy() {
		if(GameController.game.RemoveMoney(cost)) {
			amount++;
		}
		image.sprite = icon;
	}

	public void WeaponUpdate() {
		image.sprite = iconBlack;
	}

	public void WeaponFixedUpdate() {
		if(amount > 0) {
			if(slider.GetComponent<Slider>().value == 1) {
				slider.GetComponent<Slider>().value = 0;
				GameController.game.Damage(damage * amount);
			}
			slider.GetComponent<Slider>().value += 0.05f / completeTime;
		}
	}

	public int GetAmount() {
		return amount;
	}

	public void SetAmount(int a) {
		amount = a;
		if(a > 0) {
			image.sprite = icon;
		}
	}
}
