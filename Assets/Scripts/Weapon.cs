using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

	public WeaponType TypeOfWeapon;

	public GameObject Sparks;
	Transform SpawnPoint;

	// Use this for initialization
	void Start ()
	{
		if (TypeOfWeapon == WeaponType.Gun) {
			SpawnPoint = GameManager.Instance.currentCharacter.Instance.GunSpawnPoint;
		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (TypeOfWeapon == WeaponType.Gun) {
			if (Input.GetMouseButtonDown (0)) {
				Fire ();
			}
		}
	}

	void Fire ()
	{
		RaycastHit hit;
		if (Physics.Raycast (SpawnPoint.transform.position, SpawnPoint.forward, out hit, 800)) {
			Instantiate (Sparks, hit.point, Quaternion.FromToRotation (Vector3.forward, hit.normal));
		}

	}
}

public enum WeaponType
{
	Gun,
	Melee
}
