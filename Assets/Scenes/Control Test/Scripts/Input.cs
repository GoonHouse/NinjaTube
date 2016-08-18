using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Input : MonoBehaviour {
	public Transform world;
	public Transform playerMoveTarget;
	public Transform player;
	public float targetSpeed = 10f;
	public float returnSpeed = 1f;
	public float playerSpeed = 1f;
	public float playerRotationSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		Vector3 inputVector = Vector3.ClampMagnitude (new Vector3 (h, v, 0), 1f);

		transform.position = world.transform.position + inputVector; 
		playerMoveTarget.transform.position += inputVector * Time.deltaTime * targetSpeed;

		Vector3 moveTargetGravity = Vector3.MoveTowards (playerMoveTarget.transform.position, world.transform.position, 0.01f);
		playerMoveTarget.transform.position -= moveTargetGravity * Time.deltaTime * returnSpeed;

		Vector3 gravity = Vector3.MoveTowards (player.transform.position, world.transform.position, 0.01f);

		Vector3 directionToMoveTarget = Vector3.Cross (moveTargetGravity, Vector3.forward); //this is wrong, needs work

		player.transform.position += directionToMoveTarget * Time.deltaTime * playerSpeed;
		player.transform.rotation = Quaternion.LookRotation (Vector3.forward, -gravity);
	}
}
