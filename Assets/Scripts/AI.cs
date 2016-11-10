using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
	
	public PlayerController Controller;
	public bool DoneHome;
	public Vector3 MoveVector = Vector3.zero;
	public bool GeneratedVector;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!Controller.CanPlay) {
			if (Vector3.Distance (transform.position, Controller.localCharacter.HomeSpawn.position) > 1) {
				transform.LookAt (Controller.localCharacter.HomeSpawn);
				Controller.v = 1;
			} else {
				DoneHome = true;
				Controller.v = 0;
			}
			if (DoneHome) {
				transform.LookAt (MoveVector);
				Controller.v = 1;
				if (Vector3.Distance (transform.position, MoveVector) < 0.2f) {
					GeneratedVector = false;
				}
				if (!GeneratedVector) {
					float x = Random.Range (0, 10);
					float z = Random.Range (0, 10);
					MoveVector = new Vector3 (x, 0, z);
					GeneratedVector = true;
				}
			}
		}
	
	}
}
