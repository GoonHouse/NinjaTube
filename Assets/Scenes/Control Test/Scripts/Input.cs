using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Input : MonoBehaviour {
	public Transform world;
	public Transform playerMoveTarget;
	public float targetSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		transform.position = world.transform.position + Vector3.ClampMagnitude(new Vector3 (h, v, 0), 1f); 
		playerMoveTarget.transform.position += Vector3.ClampMagnitude(new Vector3 (h, v, 0), 1f) * Time.deltaTime * targetSpeed;

	}
}
