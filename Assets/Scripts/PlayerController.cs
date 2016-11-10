using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]

public class PlayerController : MonoBehaviour
{
	public Animator Anim;
	public bool CanPlay;
	public Character localCharacter;

	private List<Item> _inventory = new List<Item> ();

	public float v;
	public float h;
	public Transform GunSpawnPoint;

	public List<Item> Inventory {
		get {
			return _inventory;
		}
	}

	private Item _InHand;

	public Item InHand {
		get {
			return _InHand;
		}
		set {
			_InHand = value;
			Destroy (InHandInstance);
			InHandInstance = Instantiate (value.InstancePrefab.gameObject, Hand.position, Hand.rotation) as GameObject;
			InHandInstance.transform.parent = Hand;
		}
	}

	public Transform Hand;
	private GameObject InHandInstance;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (CanPlay) {
			v = Input.GetAxis ("Vertical");
			h = Input.GetAxis ("Horizontal");
			Anim.SetFloat ("Speed", v * 2);
			Anim.SetFloat ("Direction", h);
			Anim.SetBool ("Running", Input.GetKey (KeyCode.LeftShift));
		} else {
			Anim.SetFloat ("Speed", v * 2);
			Anim.SetFloat ("Direction", h);
			Anim.SetBool ("Running", false);
		}
	}
}
