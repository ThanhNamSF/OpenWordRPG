using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootChest : MonoBehaviour
{

	public int MaxItems;
	int ItemCount;
	List<Item> Items = new List<Item> ();

	public SerilizableChest SChest;

	public int ID;

	private Renderer rd;

	public float Distance;

	public Color HoverColor;
	public Color ClickColor;
	Color DefaultColor;
	bool Selected;
	public bool HasGenerated;

	public List<Item> MyItem {
		get {
			return Items;
		}
	}

	public void UpdateSC ()
	{
		SChest.MyItems = MyItem;
	}



	// Use this for initialization
	void Start ()
	{
		rd = GetComponent<Renderer> ();
		ItemCount = Random.Range (1, MaxItems);
		for (int i = 0; i < ItemCount; i++) {
			int r = Random.Range (0, GameManager.Instance.AllItem.Count - 1);
			Items.Add (GameManager.Instance.AllItem [r]);
		}
		DefaultColor = rd.material.color;
		SChest = new SerilizableChest (MyItem, ID);
		SaveManager.Instance.chests.Add (SChest);
		HasGenerated = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance (transform.position, GameManager.Instance.currentCharacter.Instance.transform.position) > Distance) {
			OnMouseExit ();
		}
		if (Selected) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				DeSelect ();
			}
		}
//		if (Items.Count <= 0) {
//			Destroy (gameObject);
//		}
	}

	public void LoadItem (List<string> i)
	{
		MyItem.Clear ();
		foreach (string str in i) {
			Items.Add (GameManager.Instance.FindItem (str));
		}
		UpdateSC ();
	}

	public void OnMouseOver ()
	{
		if (Vector3.Distance (transform.position, GameManager.Instance.currentCharacter.Instance.transform.position) < Distance) {
			rd.material.color = HoverColor;
		}
	}

	public void OnMouseExit ()
	{
		rd.material.color = DefaultColor;
	}

	public void OnMouseDown ()
	{
		if (Vector3.Distance (transform.position, GameManager.Instance.currentCharacter.Instance.transform.position) < Distance) {
			Selected = true;
			GameManager.Instance.SelectedChest = this;
		}
	}

	public void DeSelect ()
	{
		Selected = false;
		GameManager.Instance.SelectedChest = null;
	}
}

[System.Serializable]
public class SerilizableChest
{
	
	[System.NonSerialized]
	List<Item> Items = new List<Item> ();

	public List<Item> MyItems {
		get {
			return Items;
		}
		set {
			Items = value;
			_MyItemString.Clear ();
			foreach (Item i in MyItems) {
				Debug.Log (i.Name);
				_MyItemString.Add (i.Name);
			}

		}
	}

	public List<string> _MyItemString = new List<string> ();

	public int ID;

	public SerilizableChest (List<Item> items, int id)
	{
		MyItems = items;
		ID = id;
	}
}
