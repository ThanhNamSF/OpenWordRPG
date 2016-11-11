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

		SaveObjs.Add (p1_Inventory);
		SaveObjs.Add (p2_Inventory);
		SaveObjs.Add (p3_Inventory);
		SaveObjs.Add (chests);
		byte[] b = Serialize ();
		File.WriteAllBytes (Application.persistentDataPath + path, b);
		Debug.Log (Application.persistentDataPath + path);
	}

	public void Load ()
	{

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
