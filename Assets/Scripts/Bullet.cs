using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosionEffect;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -100 || transform.position.y > 1000)
			Destroy(gameObject);
	}


	private void OnCollisionEnter(Collision collision)
	{
		if (gameObject.activeSelf)
		{
			if (collision.gameObject.name == "PlayerTank")
				return;

			Debug.Log("Biem: " + collision.gameObject.name);

			var enemy = collision.gameObject.GetComponent<Enemy>();
			if (enemy != null)
			{
				Vector3 normal = collision.contacts[0].normal;
				Vector3 vel = GetComponent<Rigidbody>().velocity;
				float angle = Vector3.Angle(vel, -normal);
				enemy.Damage(angle);
			}
			gameObject.SetActive(false);
			GameObject explosion = GameObject.Instantiate(explosionEffect, collision.transform.position, collision.transform.rotation);
			Destroy(gameObject);
			Destroy(explosion, 4);
		}
	}
}
