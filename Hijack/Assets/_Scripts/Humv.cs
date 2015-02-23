using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Humv : Vehicle {
	public float speed;
	public float rotate_speed;
	public Vector3 position;
	//public CharacterController charControl;
	public GameObject bullet;
	public GameObject shotLocation;
	//public List<GameObject>  cannonBallShots;
	// Use this for initialization
	void Start () {
		speed = 10f;
		rotate_speed = 3f;
	}

	void Update() {
		if (driver == null) return;
		MyUpdate();
	}

	void MyUpdate() {
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.rigidbody.velocity = (transform.forward * speed);
		}  else if(Input.GetKey(KeyCode.DownArrow)){
			transform.rigidbody.velocity = -1 * (transform.forward * speed);
		}	
		if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(0, rotate_speed, 0);
		if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(0, -rotate_speed, 0);


		if(Input.GetKeyDown(KeyCode.Space)){
			GameObject shot = Instantiate(bullet) as GameObject;
			shot.transform.position = shotLocation.transform.position;
			shot.rigidbody.velocity = transform.forward * 18f;
			
		}
	}
}
