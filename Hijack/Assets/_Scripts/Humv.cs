using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Humv : Vehicle {
	public float speed;
	public Vector3 position;
	//public CharacterController charControl;
	public GameObject cannonBall;
	public GameObject shotLocation;
	public List<GameObject>  cannonBallShots;
	// Use this for initialization
	void Start () {
		speed = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(0, 0, -speed* Time.deltaTime);
		if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(0, 0, speed* Time.deltaTime);
		if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(0, 1, 0);
		if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(0, -1, 0);
		
	}
	
	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Space)){
			GameObject shot = Instantiate(cannonBall) as GameObject;
			shot.transform.position = shotLocation.transform.position;
			shot.rigidbody.AddForce(transform.forward * 500f);
			cannonBallShots.Add(shot);
			
		}
	}
	
}
