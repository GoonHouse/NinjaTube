using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Input : MonoBehaviour {
	public Transform world;					//the center of the world
	public Transform playerMoveTarget;		//the target object the player aims for
	public Transform player;				//the player object
	public float targetSpeed = 10f;			//how fast the target moves
	public float returnSpeed = 1f;			//how hard the target tries to get back to the center
	public float playerSpeed = 1f;			//how fast the player moves
	public float playerRotationSpeed = 1f;	//how fast the player turns to face the center

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal"); //get the input
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		Vector3 inputVector = Vector3.ClampMagnitude (new Vector3 (h, v, 0), 1f); //make a vector out of the input

		transform.position = world.transform.position + inputVector; //move the input object on screen to visualize the input
		playerMoveTarget.transform.position += inputVector * Time.deltaTime * targetSpeed; //move the playerMoveTarget around the screen based on input

		Vector3 moveTargetGravity = Vector3.MoveTowards (playerMoveTarget.transform.position, world.transform.position, 0.01f); //make a vector pointing back to the center of the screen from the playerMoveTarget
		playerMoveTarget.transform.position -= moveTargetGravity * Time.deltaTime * returnSpeed; //have the playerMoveTarget go back to the center

		Vector3 gravity = Vector3.MoveTowards (player.transform.position, world.transform.position, 0.01f); //this breaks if the player is exactly center!

		Vector3 directionToMoveTarget = Vector3.Project (moveTargetGravity, Vector3.Cross(gravity, Vector3.forward)); //fuck you if you think I know why this works

		player.transform.position += directionToMoveTarget * Time.deltaTime * playerSpeed; //move the player
		player.transform.rotation = Quaternion.LookRotation (Vector3.forward, -gravity); //rotate the player
	}
}
