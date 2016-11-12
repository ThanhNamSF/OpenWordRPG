using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveManager : MonoBehaviour
{
	public List<object> SaveObjs = new List<object> ();

	public Item p1_Hand;
	public Item p2_Hand;
	public Item p3_Hand;

	public List<Item> p1_Inventory = new List<Item> ();
	public List<Item> p2_Inventory = new List<Item> ();
	public List<Item> p3_Inventory = new List<Item> ();

	public List<SerilizableChest> chests = new List<SerilizableChest> ();

	string path = @"\Save.tgtayt";

	public static SaveManager Instance;

	void Awake ()
	{
		Instance = this;
		StartCoroutine ("Load");
	}

	public void Save ()
	{
		SaveObjs.Add (p1_Hand.Name);
		SaveObjs.Add (p2_Hand.Name);
		SaveObjs.Add (p3_Hand.Name);

		List<string> p1_String = new List<string> ();
		foreach (Item i in p1_Inventory) {
			p1_String.Add (i.Name);
		}
		List<string> p2_String = new List<string> ();
		foreach (Item i in p2_Inventory) {
			p2_String.Add (i.Name);
		}
		List<string> p3_String = new List<string> ();
		foreach (Item i in p3_Inventory) {
			p3_String.Add (i.Name);
		}

		SaveObjs.Add (p1_String);
		SaveObjs.Add (p2_String);
		SaveObjs.Add (p3_String);
		SaveObjs.Add (chests);
		byte[] b = Serialize ();
		File.WriteAllBytes (Application.persistentDataPath + path, b);
		Debug.Log (Application.persistentDataPath + path);
	}

	public IEnumerator Load ()
	{
		byte[] b = File.ReadAllBytes (Application.persistentDataPath + path);
		List<object> o = DeSerialize (b);
		p1_Hand = GameManager.Instance.FindItem ((string)o [0]);
		p2_Hand = GameManager.Instance.FindItem ((string)o [1]);
		p3_Hand = GameManager.Instance.FindItem ((string)o [2]);

		List<string> p1_string = new List<string> ();
		p1_string = (List<string>)o [3];
		List<Item> p1_i = new List<Item> ();
		foreach (string str in p1_string) {
			p1_i.Add (GameManager.Instance.FindItem (str));
		}
		p1_Inventory = p1_i;

		List<string> p2_string = new List<string> ();
		p2_string = (List<string>)o [4];
		List<Item> p2_i = new List<Item> ();
		foreach (string str in p2_string) {
			p2_i.Add (GameManager.Instance.FindItem (str));
		}
		p2_Inventory = p2_i;

		List<string> p3_string = new List<string> ();
		p3_string = (List<string>)o [5];
		List<Item> p3_i = new List<Item> ();
		foreach (string str in p3_string) {
			p3_i.Add (GameManager.Instance.FindItem (str));
		}
		p3_Inventory = p3_i;

		List<SerilizableChest> chestss = (List<SerilizableChest>)o [6];

		foreach (SerilizableChest sc in chestss) {
			while (!GameManager.Instance.FindLootChestWithID (sc.ID).HasGenerated)
				yield return new WaitForEndOfFrame ();
			GameManager.Instance.FindLootChestWithID (sc.ID).LoadItem (sc._MyItemString);
		}

		GameManager.Instance.Characters [0].Instance.Inventory = p1_Inventory;
		GameManager.Instance.Characters [0].Instance.InHand = p1_Hand;

		GameManager.Instance.Characters [1].Instance.Inventory = p2_Inventory;
		GameManager.Instance.Characters [1].Instance.InHand = p2_Hand;

		GameManager.Instance.Characters [2].Instance.Inventory = p3_Inventory;
		GameManager.Instance.Characters [2].Instance.InHand = p3_Hand;

		StopCoroutine ("Load");
	}

	byte[] Serialize ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		MemoryStream ms = new MemoryStream ();
		bf.Serialize (ms, SaveObjs);
		return ms.ToArray ();
	}

	List<object> DeSerialize (byte[] b)
	{
		MemoryStream ms = new MemoryStream ();
		BinaryFormatter bf = new BinaryFormatter ();
		ms.Write (b, 0, b.Length);
		ms.Seek (0, SeekOrigin.Begin);
		List<object> obj = (List<object>)bf.Deserialize (ms);
		return obj;
	}

}
