using UnityEngine;
using System.Collections;

/*
 * Script that controls player ground
 * movement as well as status of the
 * player.
 * When scaling to 4 players I intend
 * to use an array of an array of KeyCodes
 * to allow one script for all players
 */

public class Player : MonoBehaviour {

	/* a reference to the vehicle that the player is currently in */
	public Vehicle vehicle_in;
	public Vehicle vehicle_near;
	public int health;

	/* made protected for the prototype control script */
	protected Camera cam;
	protected Rigidbody rig;
	protected Collider col;
	protected bool w_down;
	protected bool a_down;
	protected bool s_down;
	protected bool d_down;
	protected bool e_down;

	void Start() {
		health = 100;
		rig = transform.rigidbody;
		col = transform.collider;
		cam = GameObject.Find("Camera").camera;
		w_down = a_down = s_down = d_down = e_down = false;
	}

	void FixedUpdate() {
		/* so the player moves in relation to the camera */
		Vector3 cam_forward = cam.transform.forward;
		Vector3 cam_right = cam.transform.right;
		cam_forward.y = 0;
		cam_right.y = 0;
		cam_forward.Normalize();
		cam_right.Normalize();

		/* check if the player wants to get into a vehicle */
		if (e_down && vehicle_near != null && vehicle_in == null) {
			e_down = false;
			vehicle_in = vehicle_near;
			vehicle_near = null;
			vehicle_in.driver = this;
			col.enabled = false;
		} else if (e_down && vehicle_in != null) { /* check if they want to get out */
			e_down = false;
			vehicle_in.driver = null;
			vehicle_in = null;
			col.enabled = true;
			/* try this for now */
		} else if (e_down) e_down = false;

		if (vehicle_in == null) { /* regular player movement */
			Vector3 player_vel = Vector3.zero;
			/* let the player move some */
			if (w_down) player_vel += cam_forward;
			if (a_down) player_vel -= cam_right;
			if (s_down) player_vel -= cam_forward;
			if (d_down) player_vel += cam_right;

			player_vel.y = rig.velocity.y;
			rig.velocity = player_vel;
		} else {
			rig.velocity = Vector3.zero;
			transform.position = vehicle_in.transform.position;
		}
	}

	void Update() {
		/* if not in vehicle poll for input from the player */
		w_down = Input.GetKey(KeyCode.W);
		a_down = Input.GetKey(KeyCode.A);
		s_down = Input.GetKey(KeyCode.S);
		d_down = Input.GetKey(KeyCode.D);
		if (Input.GetKeyDown(KeyCode.E)) e_down = true;

		if (vehicle_in != null) return;

		/* poll to see if a vehicle is in the area */
		RaycastHit[] hits = Physics.SphereCastAll(transform.position, 1.25f,
													transform.forward, 0.01f);
		bool is_vehicle = false;
		foreach (RaycastHit hit in hits) {
			if (hit.transform.tag == "Vehicle") {
				vehicle_near = hit.transform.gameObject.GetComponent<Vehicle>();
				is_vehicle = true;
			}
		}
		if (!is_vehicle) vehicle_near = null;
	}
}
