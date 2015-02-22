using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100f;
	public float resetAfterDeathTime = 5f;
	public AudioClip deathClip;

	private Player playerMovement;
	private float timer;
	private bool playerDead;

	void Awake()
	{
		playerMovement = GetComponent<Player>();
	}
	void Update()
	{
		if (health <= 0f)
		{
			if(!playerDead)
			{
				PlayerDying();
			}
			else
			{
				PlayerReset();
			}
		}
	}

	// to use this:
	//	GameObject player = GameObject.FindGameObjectWithTag("Player");
	//	PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
	//	if (condition) playerHealth.TakeDamage(30f);
	public void TakeDamage( float damage)
	{
		health -= damage; 
	}
	void PlayerDying ()
	{
		playerDead = true;
		AudioSource.PlayClipAtPoint(deathClip, transform.position);
		playerMovement.enabled = false;
	}

	void PlayerReset()
	{
		timer+=Time.deltaTime;
		if(timer >=resetAfterDeathTime)
		{
			playerDead = false;
			health = 100;
			timer = 0;
			playerMovement.enabled = true;
		}
	}
}