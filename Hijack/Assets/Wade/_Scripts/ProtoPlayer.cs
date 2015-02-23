using UnityEngine;
using System.Collections;

/*
 * For the prototype going to implement two player
 * control using the WASD and arrow control scheme
 * that was laid out for P1
 */

/* WARNING:	hacky code below (for prototype) */

/* dirty inheritance hack to make other scripts work */
public class ProtoPlayer : Player {

	/* which player is this? */
	public int PlayerNumber = 0;

	/* private bools for the second player */
	private bool up_arrow;
	private bool left_arrow;
	private bool down_arrow;
	private bool right_arrow;
	private bool rctrl_down;

	void Start() {
		health = 100;
		rig = transform.rigidbody;
		col = transform.collider;
		cam = GameObject.Find("Camera").camera;
		w_down = a_down = s_down = d_down = e_down = false;
		up_arrow = left_arrow = down_arrow = right_arrow = rctrl_down = false;
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
		if ((e_down && PlayerNumber == 0 || rctrl_down && PlayerNumber == 1) 
		    && vehicle_near != null && vehicle_in == null) {
			if (PlayerNumber == 0) e_down = false;
			else if (PlayerNumber == 1) rctrl_down = false;
			vehicle_in = vehicle_near;
			vehicle_near = null;
			vehicle_in.driver = this;
			col.enabled = false;
		} else if ((e_down && PlayerNumber == 0 || rctrl_down && PlayerNumber == 1) 
		           && vehicle_in != null) { /* check if they want to get out */
			if (PlayerNumber == 0) e_down = false;
			else if (PlayerNumber == 1) rctrl_down = false;
			vehicle_in.driver = null;
			vehicle_in = null;
			col.enabled = true;
			/* try this for now */
		} else if (e_down) e_down = false;
		else if (rctrl_down) rctrl_down = false;

		if (vehicle_in == null) { /* regular player movement */
			Vector3 player_vel = Vector3.zero;
			/* let the player move some */
			if (PlayerNumber == 0) { /* player 1 */
				if (w_down) player_vel += cam_forward;
				if (a_down) player_vel -= cam_right;
				if (s_down) player_vel -= cam_forward;
				if (d_down) player_vel += cam_right;
			} else if (PlayerNumber == 1) { /* player 2 */
				if (up_arrow) player_vel += cam_forward;
				if (left_arrow) player_vel -= cam_right;
				if (down_arrow) player_vel -= cam_forward;
				if (right_arrow) player_vel += cam_right;
			}

			player_vel.y = rig.velocity.y;
			rig.velocity = player_vel;
		} else {
			rig.velocity = Vector3.zero;
			transform.position = vehicle_in.transform.position;
		}
	}

	void Update() {
		/* if not in vehicle poll for input from the player1 */
		w_down = Input.GetKey(KeyCode.W);
		a_down = Input.GetKey(KeyCode.A);
		s_down = Input.GetKey(KeyCode.S);
		d_down = Input.GetKey(KeyCode.D);
		if (Input.GetKeyDown(KeyCode.E)) e_down = true;

		/* if not in vehicle poll for input from the player2 */
		up_arrow = Input.GetKey(KeyCode.UpArrow);
		left_arrow = Input.GetKey(KeyCode.LeftArrow);
		down_arrow = Input.GetKey(KeyCode.DownArrow);
		right_arrow = Input.GetKey(KeyCode.RightArrow);
		if (Input.GetKeyDown(KeyCode.RightControl)) rctrl_down = true;

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
