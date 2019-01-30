using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;
	public Material destroyedMaterial;
	public bool alive {  get { return health > 0; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Damage(double angle)
	{
		if (!alive)
			return;

		health -= 50;

		if (!alive)
		{
			var renderer = transform.Find("MC-1_Base").GetComponent<SkinnedMeshRenderer>().GetComponent<SkinnedMeshRenderer>();

			var materials = renderer.materials;
			materials[0] = destroyedMaterial;
			materials[1] = destroyedMaterial;
			renderer.materials = materials;
		}
	}


}
