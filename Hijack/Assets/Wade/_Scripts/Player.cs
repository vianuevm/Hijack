using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	/* a reference to the vehicle that the player is currently in */
	//public Vehicle vehicle;
	public int health;
	
	private Camera cam;
	private Rigidbody rig;
	private bool w_down;
	private bool a_down;
	private bool s_down;
	private bool d_down;

	void Start() {
		health = 100;
		rig = transform.rigidbody;
		cam = GameObject.Find("Camera").camera;
		w_down = a_down = s_down = d_down = false;
	}

	void FixedUpdate() {
		/* so the player moves in relation to the camera */
		Vector3 cam_forward = cam.transform.forward;
		Vector3 cam_right = cam.transform.right;
		cam_forward.y = 0;
		cam_right.y = 0;
		cam_forward.Normalize();
		cam_right.Normalize();

		Vector3 player_vel = Vector3.zero;
		/* let the player move some */
		if (w_down) player_vel += cam_forward;
		if (a_down) player_vel -= cam_right;
		if (s_down) player_vel -= cam_forward;
		if (d_down) player_vel += cam_right;

		rig.velocity = player_vel;
	}

	void Update() {
		/* if not in vehicle poll for input from the player */
		w_down = Input.GetKey(KeyCode.W);
		a_down = Input.GetKey(KeyCode.A);
		s_down = Input.GetKey(KeyCode.S);
		d_down = Input.GetKey(KeyCode.D);		
	}

}
