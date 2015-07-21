using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {

	public Transform playerSpawn;
	public Vector3 currentTrackPosition;

	public bool activeRespawnTimer = false;
	public float respawnTimer = 1.0f;
	public float resetRespawnTimer = 1.0f;


	// Use this for initialization
	void Start ()
	{
		if (playerSpawn != null) 
		{
			transform.position = playerSpawn.position;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (activeRespawnTimer) 
		{
			respawnTimer -= Time.deltaTime;
		}

		if (respawnTimer <= 0.0f) 
		{
			transform.position = currentTrackPosition;
			respawnTimer = resetRespawnTimer;
			activeRespawnTimer = false;
		}
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "CheckPoint")
		{
			currentTrackPosition = transform.position;
		}

		if (other.tag == "DeadZone")
		{
			activeRespawnTimer = true;
		}
	}
}
