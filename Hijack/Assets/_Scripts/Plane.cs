using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Plane : Vehicle {
	public float speed;
	public Vector3 position;
	public CharacterController charControl;
	public GameObject cannonBall;
	public GameObject shotLocation;
	public List<GameObject>  cannonBallShots;
	public bool hijacked = true;
	// Use this for initialization
	void Start () {
		speed = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		if(driver != null){
			rigidbody.useGravity = true;
		}
		if(Input.GetKey(KeyCode.UpArrow) && driver != null){
			transform.rigidbody.velocity = -1*(transform.right * speed);
		}  else if(Input.GetKey(KeyCode.DownArrow) && driver != null){
			transform.rigidbody.velocity = (transform.right * speed);
		} else if(driver != null) {
			transform.rigidbody.velocity = Vector3.zero;
		}
		
		if (Input.GetKey(KeyCode.RightArrow) && hijacked) transform.Rotate(0, 1, 0);
		if (Input.GetKey(KeyCode.LeftArrow) && hijacked) transform.Rotate(0, -1, 0);
	}
	
	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Space) && driver != null){
			GameObject shot = Instantiate(cannonBall) as GameObject;
			shot.transform.position = shotLocation.transform.position;
		}	
	}
	
}
