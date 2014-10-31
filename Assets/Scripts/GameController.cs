using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController game;

	public int money;
	public GameObject[] phones;
	public GameObject[] weapons;

	public Text damageText;
	public Text healthText;
	public Text moneyText;

	public Canvas optionsCanvas;
	public Canvas statsCanvas;
	public Canvas achevementsCanvas;
	public Canvas upgradesCanvas;
	public Canvas mainScreenCanvas;

	public GameObject phoneImage;
	private int selectedPhone = 0;
	
	void Start() {
		Screen.SetResolution(Screen.width, Screen.height, false);
		game = this;
		money = 0;
		UpdateText();
		OpenMainScreen();
		Load();
		UpdateText();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			OpenMainScreen();
		}
	}

	void FixedUpdate() {

	}

	void UpdateText() {
		healthText.text = phoneImage.GetComponent<Phone>().getHealth() + "/" + phoneImage.GetComponent<Phone>().maxHealth + "hp";
		moneyText.text = "Money: " + money;
	}

	void UpdatePhone() {
		phoneImage.GetComponent<Phone>().setHealth(phones[selectedPhone].GetComponent<Phone>().getHealth());
		phoneImage.GetComponent<Phone>().maxHealth = phones[selectedPhone].GetComponent<Phone>().maxHealth;
		phoneImage.GetComponent<Phone>().value = phones[selectedPhone].GetComponent<Phone>().value;
		phoneImage.GetComponent<Image>().sprite = phones[selectedPhone].GetComponent<Phone>().image;
	}

	public void AddMoney(int a) {
		money += a;
		UpdateText();
	}

	public bool RemoveMoney(int a) {
		if(money - a >= 0) {
			money -= a;
			UpdateText();
			return true;
		}
		else 
			return false;
	}

	public void Damage(int a) {
		phoneImage.GetComponent<Phone>().Damage(a);
		UpdateText();
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
		Damage(1);
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
