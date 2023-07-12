using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float flightSpeed = 7.5f;
	public float boostSpeed = 15f;

	[Range(0,1)]
	
	
	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;

	//Animator animator;
	Transform cameraT;
	CharacterController controller;

	void Start () {
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController>();
	}
	
	void Update () {
		// input
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;
		bool boost = Input.GetKey(KeyCode.LeftShift);

		// CHANGE DIRECTION TOWARD INPUT
		if (inputDir != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		
		float targetSpeed = ((boost) ? boostSpeed : flightSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		//transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
		//Vector3 velocity = transform.forward * currentSpeed;

		Vector3 velocity = transform.forward * currentSpeed;
		
		controller.Move(velocity * Time.deltaTime);

		//if(controller.isGrounded) { velocityY = 0; }
		
		//float animationSpeedPercent = ((running) ? 1 : 0.5f) * inputDir.magnitude;
		// animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		
		// if(Input.GetKeyDown(KeyCode.Space)) 
		// {

		// }	
	}

	private Vector2 CursorPosition()
    {
        Vector2 cursorPosition = Input.mousePosition;
        cursorPosition.x -= Screen.width/2;
        cursorPosition.y -= Screen.height/2;

        float deadZone = 0.05f;
        float cursorX = cursorPosition.x / (Screen.width / 2f);
        float cursorY = cursorPosition.y / (Screen.height / 2f);

        if(Mathf.Abs(cursorX) < deadZone) cursorX = 0;
        if(Mathf.Abs(cursorY) < deadZone) cursorY = 0;
        
        return new Vector2(cursorX, -cursorY);
    }
}