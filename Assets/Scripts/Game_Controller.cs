using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game_Controller : MonoBehaviour {

	public int money;
	public GameObject[] phones;

	public Text damageText;
	public Text healthText;
	public Text moneyText;

	public Canvas optionsCanvas;
	public Canvas statsCanvas;
	public Canvas achevementsCanvas;
	public Canvas upgradesCanvas;
	public Canvas mainScreenCanvas;

	public GameObject phone;
	private int selectedPhone = 0;
	
	void Start() {
		money = 0;
		UpdateText();
		OpenMainScreen();
		Screen.SetResolution(Screen.width, Screen.height, false);
		//phone = phones[0];
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			OpenMainScreen();
		}
	}

	void UpdateText() {
		healthText.text = phone.GetComponent<Phone>().health + "/" + phone.GetComponent<Phone>().maxHealth + "hp";
		moneyText.text = "Money: " + money;
	}

	void UpdatePhone() {
		phone.GetComponent<Phone>().health = phones[selectedPhone].GetComponent<Phone>().health;
		phone.GetComponent<Phone>().maxHealth = phones[selectedPhone].GetComponent<Phone>().maxHealth;
		phone.GetComponent<Phone>().value = phones[selectedPhone].GetComponent<Phone>().value;
		phone.GetComponent<Image>().sprite = phones[selectedPhone].GetComponent<Image>().sprite;
		phone.GetComponent<Image>().color = phones[selectedPhone].GetComponent<Image>().color;
	}

	public void NextPhone() {
		selectedPhone = 1;
		UpdatePhone();
		UpdateText();
	}

	public void PrevoiusPhone() {
		selectedPhone = 0;
		UpdatePhone();
		UpdateText();
	}

	public void Click() {
		phone.GetComponent<Phone>().Damage(1, this);
		UpdateText();
	}

	public void OpenOptions() {
		optionsCanvas.gameObject.SetActive(true);
		mainScreenCanvas.gameObject.SetActive(false);
	}

	public void OpenStats() {
		statsCanvas.gameObject.SetActive(true);
		mainScreenCanvas.gameObject.SetActive(false);
	}

	public void OpenAchevements() {
		achevementsCanvas.gameObject.SetActive(true);
		mainScreenCanvas.gameObject.SetActive(false);
	}

	public void OpenUpgrades() {
		upgradesCanvas.gameObject.SetActive(true);
		mainScreenCanvas.gameObject.SetActive(false);
	}

	public void OpenMainScreen() {
		optionsCanvas.gameObject.SetActive(false);
		statsCanvas.gameObject.SetActive(false);
		achevementsCanvas.gameObject.SetActive(false);
		upgradesCanvas.gameObject.SetActive(false);
		mainScreenCanvas.gameObject.SetActive(true);
	}
}
