using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOnSight : MonoBehaviour, ShootingBehaviour {
	public GameObject BulletPrefab;


	TankMovementController controller;
	GameObject playerTank;

	// Use this for initialization
	void Start () {
		controller = GetComponent<TankMovementController>();
		playerTank = GameObject.Find("PlayerTank");
		StartCoroutine(behaviour());
	}

	// Update is called once per frame
	void Update () {
	}


	IEnumerator behaviour()
	{
		while(true)
		{
			if (isPlayerVisible())
			{
				float allowedDifference = 3;
				while (Mathf.Abs(controller.getTurretAngleDifference(playerTank.transform)) > allowedDifference && isPlayerVisible())
				{
					yield return controller.rotateGunTowardsAsync(playerTank.transform);
					yield return new WaitForSeconds(0.5f);
					allowedDifference *= 1.5f;
				}

				if(isPlayerVisible())
				{
					var bullet = controller.shootBullet(BulletPrefab);
					yield return new WaitForSeconds(5.0f);
				}
			}
			yield return new WaitForEndOfFrame();
		}
	}


	//todo: move to superclass or compose this shit
	bool isPlayerVisible()
	{
		var playerBounds = playerTank.GetComponent<Collider>().bounds;
		var points = new List<Vector3>();

		float div = 5;

		for (float x = 0; x < 1; x += 1 / div)
			for (float y = 0; y < 1; y += 1 / div)
				for (float z = 0; z < 1; z += 1 / div)
					points.Add(new Vector3(
						Mathf.Lerp(playerBounds.min.x, playerBounds.max.x, x),
						Mathf.Lerp(playerBounds.min.y, playerBounds.max.y, y),
						Mathf.Lerp(playerBounds.min.z, playerBounds.max.z, z)
						));


		var forward = -controller.turret.transform.right;
		var origin = controller.turret.transform.position;

		int hitRays = 0;
		int playerRays = 0;
		int camoRays = 0;

		double distance = Vector3.Distance(origin, playerTank.transform.position);

		foreach (var point in points)
		{
			Vector3 direction = point - origin;
			if (Vector3.Angle(new Vector3(forward.x, 0, forward.z), new Vector3(direction.x, 0, direction.z)) > 60)
				continue;

			//Color color = Color.red;

			RaycastHit hitInfo;
			if (Physics.Raycast(origin, direction, out hitInfo, 100000))
			{
				if (hitInfo.transform.name == "PlayerTank")
				{
				//	color = Color.cyan;
					playerRays++;
				}
				else if (Vector3.Distance(hitInfo.point, origin) < distance)
				{
			//		color = Color.magenta;
					camoRays++;
				}
			//	else
			//		color = Color.blue;
				hitRays++;
			}
			//Debug.DrawLine(origin, origin + direction * 100, color);
		}
		//Debug.Log("Distance: " + distance + ", Hit: " + hitRays + ", player: " + playerRays + ", camo: " + camoRays);
		if (playerRays > distance)
		{
			return true;
		}
		return false;
	}



}
