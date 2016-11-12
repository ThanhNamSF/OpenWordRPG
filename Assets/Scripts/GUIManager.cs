using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
	public int InventoryColumns = 7;
	public int InventoryRows = 6;
	public int ButtonWidth = 32;
	public int ButtonHeight = 32;
	public int ButtonOffset = 5;
	int _inventoryID = 0;
	int _chestID = 1;
	Rect InventoryRect;
	Rect ChestRect;
	public bool ShowInventory = false;
	// Use this for initialization
	void Start ()
	{
		InventoryRect = new Rect (0, 0, (ButtonWidth + 5) * InventoryColumns, (ButtonHeight + 5) * InventoryRows);
		ChestRect = new Rect (0, 0, (ButtonWidth + 5) * InventoryColumns, (ButtonHeight + 5) * InventoryRows);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Z))
			ShowInventory = !ShowInventory;
	
	}

	void OnGUI ()
	{
		
		if (ShowInventory) {
			InventoryRect = GUI.Window (_inventoryID, InventoryRect, InventoryWindow, "Inventory");
			Time.timeScale = 0.5f;
		} else {
			Time.timeScale = 1;
		}
		if (GameManager.Instance.SelectedChest != null) {
			ChestRect = GUI.Window (_chestID, ChestRect, ChestWindow, "Chest");
		}
		if (GameManager.Instance.currentCharacter.Instance.InHand != null)
			GUI.Box (new Rect (Screen.width - Screen.width / 7.5f, 0f, Screen.width / 7.5f, Screen.height / 8.4f), new GUIContent ("Selected Item: \n " + GameManager.Instance.currentCharacter.Instance.InHand.Name));
		else
			GUI.Box (new Rect (Screen.width - Screen.width / 7.5f, 0f, Screen.width / 7.5f, Screen.height / 8.4f), new GUIContent ("Selected Item: \n None"));
		if (GUI.Button (new Rect (0, 0, 128, 128), "Save")) {
			SaveManager.Instance.Save ();
		}
	}

	void InventoryWindow (int id)
	{
		int c = 0;
		int cu = 0;
		for (int x = 0; x < InventoryColumns; x++) {
			for (int y = 0; y < InventoryRows; y++) {
				if (c < GameManager.Instance.currentCharacter.Instance.Inventory.Count) {
					if (GUI.Button (new Rect (ButtonOffset + (ButtonWidth * x), ButtonOffset + (ButtonHeight * y), ButtonWidth, ButtonHeight), GameManager.Instance.currentCharacter.Instance.Inventory [cu].Name)) {
						if (Event.current.button == 0) {
							if (GameManager.Instance.currentCharacter.Instance.Inventory [cu].Selectable) {
								GameManager.Instance.currentCharacter.Instance.InHand = GameManager.Instance.currentCharacter.Instance.Inventory [cu];
							}
						} else if (Event.current.button == 1) {
							if (GameManager.Instance.SelectedChest != null) {
								if (GameManager.Instance.SelectedChest.MyItem.Count < GameManager.Instance.SelectedChest.MaxItems) {
									GameManager.Instance.SelectedChest.MyItem.Add (GameManager.Instance.currentCharacter.Instance.Inventory [cu]);
									if (GameManager.Instance.currentCharacter.Instance.InHand == GameManager.Instance.currentCharacter.Instance.Inventory [cu])
										GameManager.Instance.currentCharacter.Instance.InHand = null;
									GameManager.Instance.currentCharacter.Instance.Inventory.Remove (GameManager.Instance.currentCharacter.Instance.Inventory [cu]);
								}
							}
						}
						GameManager.Instance.SelectedChest.UpdateSC ();
					}
					cu++;
				} else {
					GUI.Label (new Rect (ButtonOffset + (ButtonWidth * x), ButtonOffset + (ButtonHeight * y), ButtonWidth, ButtonHeight), (x + (y * InventoryColumns) + 1).ToString (), "box");
				}
				c++;
			}
		}
		GUI.DragWindow ();
	}

	void ChestWindow (int id)
	{
		int c = 0;
		for (int x = 0; x < InventoryColumns; x++) {
			for (int y = 0; y < InventoryRows; y++) {
				if (c <= GameManager.Instance.SelectedChest.MyItem.Count - 1) {
					if (GUI.Button (new Rect (ButtonOffset + (ButtonWidth * x), ButtonOffset + (ButtonHeight * y), ButtonWidth, ButtonHeight), GameManager.Instance.SelectedChest.MyItem [c].Name)) {
						GameManager.Instance.currentCharacter.Instance.Inventory.Add (GameManager.Instance.SelectedChest.MyItem [c]);
						GameManager.Instance.SelectedChest.MyItem.Remove (GameManager.Instance.SelectedChest.MyItem [c]);
						GameManager.Instance.SelectedChest.UpdateSC ();
					}
					c++;
				}
			}
		}
		GUI.DragWindow ();
	}

}
