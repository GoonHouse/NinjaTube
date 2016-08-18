using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Input : MonoBehaviour {
	public Transform world;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		transform.position = world.transform.position + Vector3.Normalize(new Vector3 (h, v, 0)); //using the CrossPlatformInputManager smooths out the key presses but using Normalize on the vector means it 'holds' the input as the total vector goes over 1. Without normalize it's possible to move farther with the keyboard than it is with an xbox controller though
	}
}
