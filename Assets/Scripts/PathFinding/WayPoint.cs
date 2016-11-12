using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		PathManager.Instance.Waypoints.Add (this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public float Distance (Transform obj)
	{
		return Vector3.Distance (obj.position, transform.position);
	}
}
