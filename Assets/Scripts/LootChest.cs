using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootChest : MonoBehaviour
{

	public int MaxItems;
	int ItemCount;
	List<Item> Items = new List<Item> ();
	private Renderer rd;

	public float Distance;

	public Color HoverColor;
	public Color ClickColor;
	Color DefaultColor;
	bool Selected;

	public List<Item> MyItem {
		get {
			return Items;
		}
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

	void OnGUI ()
	{

	}
}
