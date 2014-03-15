// <summary>
/// ModifiedStat.cs
/// 05-12-13
/// M A Plant
/// 
/// Script that is responsible for character and enemy motion in game
/// </summary>
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour {
	public float RotationalSpeed = 250;			// The speed the player turns
	public float strafeSpeed = 5f;				// The speed the player strafes
	public float moveSpeed = 10;				// The speed the player walks
	public float runSpeed = 2;					// The speed the player will run at

	private Transform _myTransform;				// Cached transform for perfomance purposes
	private CharacterController _controller;	// Cahced version of the Character Controller

	public void Awake(){
		_myTransform = transform;
		_controller = GetComponent<CharacterController>();
	}
	// Use this for initialization
	void Start () {
		animation.wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		if(!_controller.isGrounded){
			_controller.Move(Vector3.down * Time.deltaTime);
		}
		Turn();
		Walk();
		Strafe();
	}

	private void Turn(){
// This code allows for Horizontal (Left/Right) movement
	if(Mathf.Abs(Input.GetAxis("Horizontal Movement")) > 0){
//			Debug.Log ("Rotate: " + Input.GetAxis("Horizontal Movement"));
			_myTransform.Rotate(0, Input.GetAxis("Horizontal Movement") * Time.deltaTime * RotationalSpeed, 0);
		}
	}

	private void Walk(){
// This code allows for Vertical (Forward/Backward) movement
		if(Mathf.Abs(Input.GetAxis("Vertical Movement")) > 0){
			if(Input.GetButton("Run")){
				animation.CrossFade("walk");
				_controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical Movement") * moveSpeed * runSpeed);
			}
			else{
				animation.CrossFade("walk");
				_controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical Movement") * moveSpeed);

			}
		}
		else{
			animation.CrossFade("idle");
		}
	}

	private void Strafe(){
// This code allows for Strafing (Quick Left/Right) movement
		if(Mathf.Abs(Input.GetAxis("Strafe")) > 0){
//			Debug.Log ("Strafing: " + Input.GetAxis("Strafe"));
			_controller.SimpleMove(_myTransform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe") * strafeSpeed);
		} 
	}
}
