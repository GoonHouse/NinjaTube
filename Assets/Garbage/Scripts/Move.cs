using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position - Vector3.forward * Time.deltaTime * speed;
	}
}
