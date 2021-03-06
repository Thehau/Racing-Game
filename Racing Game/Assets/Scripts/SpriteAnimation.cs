﻿using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour 
{
	public int columns = 8;
	public int rows = 8;

	//animation control variables
	public int currentFrame = 1;
	public int currentAnim = 0;
	public float animTime = 0.0f;
	public float fps = 10.0f;

	public bool explode = false;

	private Vector2 framePosition;
	private Vector2 frameSize;
	private Vector2 frameOffset;
	private int i;

	private float carVelocity;

	//animation frames (min and max) varables
	private int idle = 17;
	private int idleLeft = 1;
	private int idleRight = 2;
	private int driveMin =3;
	private int driveMax = 4;
	private int driveLeftMin = 5;
	private int driveLeftMax = 6;
	private int driveRightMin = 7;
	private int driveRightMax = 8;
	private int spin = 9;
	private int explosionMin = 10;
	private int explosionMax = 16;

	//animation id variables
	private int animIdle = 0;
	private int animIdleLeft = 1;
	private int animIdleRight = 2;
	private int animDrive = 3;
	private int animDriveLeft = 4;
	private int animDriveRight = 5;
	private int animSpin = 6;
	private int animexlosion = 7;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleAnimation ();

	}

	void HandleAnimation()
	{
		FindAnimation ();
		PlayAnimation ();
	}

	void FindAnimation()
	{
		PlayerMovement playerMovement = transform.parent.GetComponent<PlayerMovement> ();
		carVelocity = playerMovement.currentSpeed;

		if (carVelocity > 0.1f) 
		{
			currentAnim = animDrive;
			if(Input.GetAxis("Horizontal") < 0)
			{
				currentAnim = animDriveLeft;
			}
			if(Input.GetAxis("Horizontal") > 0)
			{
				currentAnim = animDriveRight;
			}
		} 
		if (carVelocity < 0.1f) 
		{
			currentAnim = animIdle;
		}

		if (explode)
		{
			currentAnim = animexlosion;
		}


	}

	void PlayAnimation()
	{
		animTime -= Time.deltaTime;
		if (animTime <= 0) 
		{
			currentFrame += 1;
			animTime += 1.0f / fps;
		}
		//one-off animations
		if (currentAnim == animexlosion)
		{
			currentFrame = Mathf.Clamp(currentFrame, explosionMin, explosionMax + 1);
			if(currentFrame > explosionMax)
			{
				explode = false;
			}
		}

		//looping animations
		if (currentAnim == animIdle)
		{
			currentFrame = Mathf.Clamp(currentFrame, idle, idle);
		}

		if (currentAnim == animDrive)
		{
			currentFrame = Mathf.Clamp(currentFrame, driveMin, driveMax + 1);
			if(currentFrame > driveMax)
			{
				currentFrame = driveMin;
			}
		}

		if (currentAnim == animDriveLeft)
		{
			currentFrame = Mathf.Clamp(currentFrame, driveLeftMin, driveLeftMax + 1);
			if(currentFrame > driveLeftMax)
			{
				currentFrame = driveLeftMin;
			}
		}

		if (currentAnim == animDriveRight)
		{
			currentFrame = Mathf.Clamp(currentFrame, driveRightMin, driveRightMax + 1);
			if(currentFrame > driveRightMax)
			{
				currentFrame = driveRightMin;
			}
		}

		framePosition.y = 1;
		for (i = currentFrame; i > columns; i -= rows) 
		{
			framePosition.y += 1;
		}
		framePosition.x = i - 1;

		frameSize = new Vector2 (1.0f / columns, 1.0f / rows);
		frameOffset = new Vector2 (framePosition.x / columns,1.0f - (framePosition.y / rows));
		
		GetComponent<Renderer> ().material.SetTextureScale ("_MainTex", frameSize);
		GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", frameOffset);
	}
}
