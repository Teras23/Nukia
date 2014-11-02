using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController game;

	public float money;
	public GameObject[] phones;
	public GameObject[] weapons;
    private GameObject[] weaponPanels;

    public Text damageText;
	public Text healthText;
	public Text moneyText;

	public Canvas optionsCanvas;
	public Canvas statsCanvas;
	public Canvas achevementsCanvas;
	public Canvas upgradesCanvas;
	public Canvas mainScreenCanvas;

    public GameObject weaponPanelPrefab;
	public GameObject weaponScrollPanel;

	public GameObject phoneImage;
	private int selectedPhone = 0;
	
	void Start() {
		Screen.SetResolution(Screen.width, Screen.height, false);
		weaponPanels = new GameObject[weapons.Length];
		game = this;
		money = 0;
		OpenMainScreen();
		CreateWeaponPanels();
		Load();
		UpdateText();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			OpenMainScreen();
		}
        UpdateText(); 
	}

	void FixedUpdate() {
		for(int i = 0; i < weaponPanels.Length; i++) {
			if(weaponPanels[i] != null) {
				weaponPanels[i].GetComponent<Weapon>().WeaponFixedUpdate();
			}
		}
	}

	void CreateWeaponPanels() {
		for(int i = 0; i < weapons.Length; i++) {
			if(weapons[i] != null) {
				weaponPanels[i] = (GameObject)GameObject.Instantiate(weaponPanelPrefab);
				weaponPanels[i].transform.parent = weaponScrollPanel.transform;
				weaponPanels[i].name = "Weapon " + (i + 1);
				weaponPanels[i].transform.localScale = new Vector3(1, 1, 1);
				weaponPanels[i].GetComponent<Weapon>().icon = weapons[i].GetComponent<Weapon>().icon;
				weaponPanels[i].GetComponent<Weapon>().iconBlack = weapons[i].GetComponent<Weapon>().iconBlack;
				weaponPanels[i].GetComponent<Weapon>().completeTime = weapons[i].GetComponent<Weapon>().completeTime;
				weaponPanels[i].GetComponent<Weapon>().cost = weapons[i].GetComponent<Weapon>().cost;
				weaponPanels[i].GetComponent<Weapon>().damage = weapons[i].GetComponent<Weapon>().damage;
				weaponPanels[i].GetComponent<Weapon>().WeaponUpdate();
			}
		}
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
	}

	public bool RemoveMoney(int a) {
		if(money - a >= 0) {
			money -= a;
			return true;
		}
		else 
			return false;
	}

	public void Damage(int a) {
		phoneImage.GetComponent<Phone>().Damage(a);
	}

	public void NextPhone() {
		if(selectedPhone + 1 < phones.Length) {
			if(phones[selectedPhone + 1] != null) {
				selectedPhone += 1;
				UpdatePhone(); 
			}
		}
	}

	public void PrevoiusPhone() {
		if(selectedPhone - 1 >= 0) {
			if(phones[selectedPhone - 1] != null) {
				selectedPhone -= 1;
				UpdatePhone(); 
			}
		}
	}

	public void Click() {
		Damage(1);
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

	/*void OnApplicationFocus(bool focusStatus) {
		if(focusStatus) {
			Load();
		}
	}

	void OnApplicationPause(bool pauseStatus) {
		if(pauseStatus) {
			Save();
		}
	}*/

	void OnApplicationQuit() {
		Save();
	}

	public void Save() {
		PlayerPrefs.SetFloat("Money", money);
		for(int i = 0; i < weaponPanels.Length; i++) {
			if(weaponPanels[i] != null) {
				PlayerPrefs.SetInt(weaponPanels[i].name + "A", weaponPanels[i].GetComponent<Weapon>().GetAmount());
			}
		}
		PlayerPrefs.Save();
	}

	public void Load() {
		if(PlayerPrefs.HasKey("Money")) {
			money = PlayerPrefs.GetFloat("Money");
		}
		for(int i = 0; i < weaponPanels.Length; i++) {
			if(weaponPanels[i] != null) {
				if(PlayerPrefs.HasKey(weaponPanels[i].name + "A"))
					weaponPanels[i].GetComponent<Weapon>().SetAmount(PlayerPrefs.GetInt(weaponPanels[i].name + "A"));
			}
		}
	}
}
