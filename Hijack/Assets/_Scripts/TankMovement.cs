using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TankMovement : Vehicle {
	public float speed;
	public Vector3 position;
	public CharacterController charControl;
	public GameObject cannonBall;
	public GameObject shotLocation;
	public List<GameObject>  cannonBallShots;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		speed = 4f;
	}

	void Update() {
		if (driver == null) return;
		MyUpdate();
	}
	
	// Update is called once per frame
	void MyUpdate () {
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.rigidbody.velocity = (transform.forward * speed);
		}  else if(Input.GetKey(KeyCode.DownArrow)){
			transform.rigidbody.velocity = -1 * (transform.forward * speed);
		}	

		if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(0, 1, 0);
		if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(0, -1, 0);

	}
	
	void FixedUpdate() {
		if (driver == null) return;
		MyFixedUpdate();
	}

	void MyFixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Space)){
			GameObject shot = Instantiate(cannonBall) as GameObject;
			GameObject boom = Instantiate(explosion) as GameObject;
			boom.transform.position = shot.transform.position = shotLocation.transform.position;
			shot.rigidbody.AddForce(transform.forward * 2000f);
			cannonBallShots.Add(shot);
		}
		for(int i = 0; i < cannonBallShots.Count; ++i){
			if(Mathf.Abs(cannonBallShots[i].transform.position.x - transform.position.x) > 10f && !cannonBallShots[i].renderer.isVisible){
				Destroy(cannonBallShots[i]);
				cannonBallShots.RemoveAt(i);
				i-= 1;
			}
		}	
	}
	
}
