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
public class AdvancedMovement : MonoBehaviour {
	public enum State{
		Idle,
		Init,
		Setup,
		Run
	}

	public enum Turn{
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward{
		back = -1,
		none = 0,
		forward = 1
	}

	public float RotationalSpeed = 250;			// The speed the player turns
	public float strafeSpeed = 5f;				// The speed the player strafes
	public float walkSpeed = 10;				// The speed the player walks
	public float runSpeed = 2;					// The speed the player will run at
	public float gravity = 10;					// A variable for the downward forces of the game
	public float airTime = 0;					// The set amount of time since the player last touched the ground
	public float fallTime = .5f;				// Amount fo time before a drop is considered a fall
	public float jumpHeight = 2;				// The set height a player can jump
	public float jumpTime = 1.5f;				// The length of time it takes to eecute a jump

	public CollisionFlags _collisionFlags;		// Collisions detectec by this variable
	private Vector3 _moveDirection;				// Vector 3 to control the direction a player is moving
	private Transform _myTransform;				// Cached transform for perfomance purposes
	private CharacterController _controller;	// Cached version of the Character Controller

	private Turn _turn;
	private Forward _forward;
	private Turn _strafe;
	private bool _run;
	private bool _jump;

	private State _state;

	public void Awake(){
		_myTransform = transform;
		_controller = GetComponent<CharacterController>();

		_state = AdvancedMovement.State.Init;
	}

	// Use this for initialization
	IEnumerator Start () {
		while(true){
			switch(_state){
			case State.Init:
				Init();
				break;
			case State.Setup:
				Setup();
				break;
			case State.Run:
				ActionCommands();
				break;
			}

			yield return null;
		}
	}

	private void Init(){
		if(!GetComponent<CharacterController>()) return;
		if(!GetComponent<Animation>()) return;

		_state = AdvancedMovement.State.Setup;
	}
	private void Setup(){
		_moveDirection = Vector3.zero;
		animation.Stop();								// Halt any animatiosn set to play automatically
		animation.wrapMode = WrapMode.Loop;				// Loop animations continuosly
		animation["jump"].layer = -1;
		animation["jump"].wrapMode = WrapMode.Once;
		animation.Play ("idle");						// Set animation to idle on script and game start

		_turn = AdvancedMovement.Turn.none;
		_forward = AdvancedMovement.Forward.none;
		_strafe = AdvancedMovement.Turn.none;
		_run = false;
		_jump = false;

		_state = AdvancedMovement.State.Run;
	}

	private void ActionCommands(){
		// Allows for left/right movement
			_myTransform.Rotate(0, (int)_turn * Time.deltaTime * RotationalSpeed, 0);

		// Checks to see if the player grounded
		if(_controller.isGrounded){
			airTime = 0;
			
			_moveDirection = new Vector3((int)_strafe, 0, (int)_forward);
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;

			// Allows movement forward and backward
			if(_forward != Forward.none){
				if(_run){								// Checks for run command 
					_moveDirection *= runSpeed;			// Sets the run speed
					Run ();								// Plays the run animation
				}
				else{
					Walk ();							// Plasy the walk animation
				}
			}
			else if(_strafe != AdvancedMovement.Turn.none){			// Checks for the strafe command
				Strafe ();
			}
			else{
				Idle();									// If nothing is active return to idle mode
			}
			
			// Controls the Jump animation
			if(_jump){
				if(airTime < jumpTime){
					_moveDirection.y += jumpHeight;
					Jump();
				}
			}
		}
		else{
			//If we have detected collisons
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0){
				airTime += Time.deltaTime;		// Increase the airtime
				if(airTime > fallTime){			// If character been airborne too long
				Fall();							// play the fall animation
				}
			}
		}
		
		_moveDirection.y -= gravity * Time.deltaTime;
		//Find and store collisions
		_collisionFlags = _controller.Move (_moveDirection * Time.deltaTime);
	}

	public void MoveMeForward(Forward z){
		_forward = z;
	}
	public void Turning(Turn y){
		_turn = y;
	}
	public void Strafe(Turn x){
		_strafe = x;
	}
	public void ToggleRun(){
		_run = !_run;
	}
	public void JumpUp(){
		_jump = true;
	}

// Selected animations that will be executed when called by the secondary scripts
	public void Idle(){
		animation.CrossFade("idle");
	}

	public void Walk(){
		animation.CrossFade("walk");
	}

	public void Run(){
		animation["walk"].speed = 1.5f;
		animation.CrossFade("walk");
	}

	public void Strafe(){
		animation.CrossFade("walk");
	}

	public void Jump(){
		animation.CrossFade("jump");
	}

	public void Fall(){
		animation.CrossFade("");
	}
}
