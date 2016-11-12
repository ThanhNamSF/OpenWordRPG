using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{

	public List<WayPoint> Waypoints = new List<WayPoint> ();
	public static PathManager Instance;

	void Awake ()
	{
		Instance = this;
	}

	public WayPoint FindClosestWaypoint (Transform obj, AI character)
	{
		WayPoint curClosests = null;
		float distance = Mathf.Infinity;
		Vector3 pos = obj.position;

		foreach (WayPoint wp in Waypoints) {
			Vector3 difference = wp.transform.position - pos;
			float magitude = difference.sqrMagnitude;
			if (magitude < distance && magitude > 0.2f) {
				if (wp != character.LastWaypoint) {
					curClosests = wp;
					distance = magitude;
				}
			}
		}
		return curClosests;
	}
}
