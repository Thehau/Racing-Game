using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 10.0f;
	public float reverseSpeed = 5.0f;
	public float turnSpeed = 0.6f;

	private float moveDirection = 0.0f;
	private float turnDirection = 0.0f;

	public float currentSpeed = 0.0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// Drag and AngularDrag variables
		currentSpeed = Mathf.Abs (transform.InverseTransformDirection (GetComponent<Rigidbody> ().velocity).z);
		
		float maxAngularDrag = 2.5f;
		float currentAngularDrag = 1.0f;
		float aDragLerpTime = currentSpeed * 0.1f;
		
		float maxDrag = 1.0f;
		float currentDrag = 2.5f;
		float dragLerpTime = currentSpeed * 0.1f;
		
		float myAngularDrag = Mathf.Lerp (currentAngularDrag, maxAngularDrag, aDragLerpTime);
		float myDrag = Mathf.Lerp (currentDrag, maxDrag, dragLerpTime);

		//Acceleration movement
		if (Input.GetAxis ("Vertical") > 0.0f)
		{
			moveDirection = Input.GetAxis ("Vertical") * speed;
			GetComponent<Rigidbody> ().AddRelativeForce (0, 0, moveDirection);
			if(currentSpeed > 0.05f)
			{
				turnDirection = Input.GetAxis ("Horizontal") * turnSpeed;
				GetComponent<Rigidbody> ().AddRelativeTorque(0, turnDirection, 0);
			}
		}
		//Reverse movement
		if(Input.GetAxis("Vertical") < 0.0f)
		{
			moveDirection = Input.GetAxis ("Vertical") * reverseSpeed;
			GetComponent<Rigidbody> ().AddRelativeForce (0, 0, moveDirection);
			if(currentSpeed > 0.05f)
			{
				turnDirection = Input.GetAxis ("Horizontal") * turnSpeed;
				GetComponent<Rigidbody> ().AddRelativeTorque(0, -turnDirection, 0);
			}
		}

		//calculate drag and angulardrag
		GetComponent<Rigidbody> ().angularDrag = myAngularDrag;
		GetComponent<Rigidbody> ().drag = myDrag;
	}
}
