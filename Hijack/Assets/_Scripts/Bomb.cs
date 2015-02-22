using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : MonoBehaviour {
	public GameObject explosion;
	public List<GameObject> bombs;
	public float explosionTime;
	public bool exploded;
	// Use this for initialization
	void Start () {
		explosionTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Ground"){
			GameObject boom = Instantiate(explosion) as GameObject;
			boom.transform.position = transform.position;
			Destroy(this.gameObject);
			explosionTime = Time.time;
		}
	}
}
