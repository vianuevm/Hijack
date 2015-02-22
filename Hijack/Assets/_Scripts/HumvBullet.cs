using UnityEngine;
using System.Collections;

public class HumvBullet : MonoBehaviour {
	public float damage = 1f;
	private float time_fired;

	// Use this for initialization
	void Start () {
		time_fired = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - time_fired > 5f || this.renderer.isVisible == false) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Wall") {
			Destroy (this.gameObject);
		}
	}
}
