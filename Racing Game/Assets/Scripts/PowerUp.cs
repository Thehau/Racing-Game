using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour 
{
	public enum PowerType
	{
		Projectile = 0,
		Trap = 1,
		Boost = 2
	}

	public PowerType powerUpType = PowerType.Projectile;
	private GameObject playerGameObject;

	// Use this for initialization
	void Start () 
	{
		playerGameObject = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("We have a poerup !");
			ApplyPowerUp();
		}
	}

	void ApplyPowerUp()
	{
		switch (powerUpType) 
		{
		case PowerType.Projectile :
			Debug.Log("We have a projectile!");
			break;
		case PowerType.Trap :
			Debug.Log("We have a trap!");
			break;
		case PowerType.Boost :
			Debug.Log("We have a boost!");
			break;
		}
	}
}
