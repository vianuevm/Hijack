using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	public float vehicleHealth;
	public float vehicleSpeed;
	public float vehicleArmor;

	public Player driver;

	// Use this for initialization

	protected void Attack(){

	}

	protected void Hurt(){
		print ("HELLO");
	}
}
