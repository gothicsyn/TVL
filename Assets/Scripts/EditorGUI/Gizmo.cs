using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour {
	public void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawCube(transform.position, new Vector3(2, 2, 2));
	}
}
