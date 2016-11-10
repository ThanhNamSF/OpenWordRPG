using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public List<Character> Characters = new List<Character> ();
	public List<Item> AllItem = new List<Item> ();
	bool ShowCharView;
	public int SelectedCharacter;
	public Character currentCharacter;
	int LastCharacter;
	public static GameManager Instance;
	public bool CanShowSwitch = true;
	public LootChest SelectedChest;


	void Awake ()
	{
		Instance = this;
		foreach (Character c in Characters) {
			GameObject go = Instantiate (c.PlayerPrefab, c.HomeSpawn.position, c.HomeSpawn.rotation) as GameObject;
			c.Instance = go.GetComponent<PlayerController> ();
			c.Instance.localCharacter = c;
		}
		ChangeCharacterStart (Characters [PlayerPrefs.GetInt ("SelectedChar")]);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.C)) {
			ShowCharView = true;
			Time.timeScale = 0.5f;
		} else {
			ShowCharView = false;
			Time.timeScale = 1;
		}
	}

	void OnGUI ()
	{
		if (ShowCharView && CanShowSwitch) {
			GUILayout.BeginArea (new Rect (Screen.width - 64, Screen.height - 192, 64, 192));
			foreach (Character c in Characters) {
				if (GUILayout.Button (c.Icon, GUILayout.Width (64), GUILayout.Height (64))) {
					ChangeCharacter (c);
				}
			}

			GUILayout.EndArea ();
		}
	}

	void ChangeCharacterStart (Character c)
	{
		LastCharacter = SelectedCharacter;
		SelectedCharacter = Characters.IndexOf (c);
		currentCharacter = c;
		Characters [LastCharacter].Instance.GetComponent<PlayerController> ().CanPlay = false;
		Characters [SelectedCharacter].Instance.GetComponent<PlayerController> ().CanPlay = true;
		Camera.main.GetComponent<SmoothFollow> ().target = Characters [SelectedCharacter].Instance.transform;
		PlayerPrefs.SetInt ("SelectedChar", SelectedCharacter);
	}

	void ChangeCharacter (Character c)
	{
		c.Instance.GetComponent<AI> ().DoneHome = false;
		if (Vector3.Distance (Characters [SelectedCharacter].Instance.transform.position, c.Instance.transform.position) > 10) {
			SequenceManager.Instance.StartCoroutine ("DoCharSwitch", c);
			CanShowSwitch = false;
			LastCharacter = SelectedCharacter;
			SelectedCharacter = Characters.IndexOf (c);
			currentCharacter = c;
			Characters [LastCharacter].Instance.GetComponent<PlayerController> ().CanPlay = false;
			Characters [SelectedCharacter].Instance.GetComponent<PlayerController> ().CanPlay = true;
			PlayerPrefs.SetInt ("SelectedChar", SelectedCharacter);
		} else {
			LastCharacter = SelectedCharacter;
			SelectedCharacter = Characters.IndexOf (c);
			currentCharacter = c;
			Characters [LastCharacter].Instance.GetComponent<PlayerController> ().CanPlay = false;
			Characters [SelectedCharacter].Instance.GetComponent<PlayerController> ().CanPlay = true;
			PlayerPrefs.SetInt ("SelectedChar", SelectedCharacter);
			Camera.main.GetComponent<SmoothFollow> ().target = Characters [SelectedCharacter].Instance.transform;
		}

	}
}



[System.Serializable]
public class Character
{
	public string Name;
	public Texture2D Icon;
	public GameObject PlayerPrefab;
	public PlayerController Instance;
	public Transform HomeSpawn;
}

[System.Serializable]
public class Item
{
	public string Name;
	public Texture Icon;
	public bool Selectable;
	public ItemInstance InstancePrefab;
}

