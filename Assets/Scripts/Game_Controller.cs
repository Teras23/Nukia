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
		Load();
		UpdateText();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			OpenMainScreen();
		}
	}

	void UpdateText() {
		healthText.text = phone.GetComponent<Phone>().getHealth() + "/" + phone.GetComponent<Phone>().maxHealth + "hp";
		moneyText.text = "Money: " + money;
	}

	void UpdatePhone() {
		phone.GetComponent<Phone>().setHealth(phones[selectedPhone].GetComponent<Phone>().getHealth());
		phone.GetComponent<Phone>().maxHealth = phones[selectedPhone].GetComponent<Phone>().maxHealth;
		phone.GetComponent<Phone>().value = phones[selectedPhone].GetComponent<Phone>().value;
		phone.GetComponent<Image>().sprite = phones[selectedPhone].GetComponent<Phone>().image;
	}

	public void NextPhone() {
		if(selectedPhone + 1 < phones.Length) {
			if(phones[selectedPhone + 1] != null) {
				selectedPhone += 1;
				UpdatePhone();
				UpdateText();
			}
		}
	}

	public void PrevoiusPhone() {
		if(selectedPhone - 1 >= 0) {
			if(phones[selectedPhone - 1] != null) {
				selectedPhone -= 1;
				UpdatePhone();
				UpdateText();
			}
		}
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

	void OnApplicationFocus(bool focusStatus) {
		if(focusStatus) {
			Load();
		}
	}

	void OnApplicationPause(bool pauseStatus) {
		if(pauseStatus) {
			Save();
		}
	}

	void OnApplicationQuit() {
		Save();
	}

	public void Save() {
		PlayerPrefs.SetInt("Money", money);
		PlayerPrefs.Save();
	}

	public void Load() {
		if(PlayerPrefs.HasKey("Money")) {
			money = PlayerPrefs.GetInt("Money");
		}
		else {
			PlayerPrefs.SetInt("Money", 0);
			PlayerPrefs.Save();
			print("set0");
		}
	}
}
