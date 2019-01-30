﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementController : MonoBehaviour {
	//all left wheels
	public GameObject[] LeftWheels;
	//all right wheels
	public GameObject[] RightWheels;

	public GameObject LeftTrack;

	public GameObject RightTrack;

	public float wheelsSpeed = 2f;
	public float tracksSpeed = 2f;
	public float forwardSpeed = 1f;
	public float rotateSpeed = 1f;

	public GameObject turret;
	public GameObject gun;


	public float leftTrackSpeed = 0;
	public float rightTrackSpeed = 0;

	public IEnumerator rotateGunTowardsAsync(Transform other)
	{
		while (true)
		{
			Vector3 targetDirection = other.position - transform.position;
			targetDirection.y = 0;
			targetDirection = transform.InverseTransformDirection(targetDirection);
			float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(targetDirection.z, targetDirection.x) - 90;
			float angle = turret.transform.localEulerAngles.z;

			float diff = targetAngle - angle;
			while (diff > 180)
				diff -= 360;
			while (diff < -180)
				diff += 360;

			if (Mathf.Abs(diff) < 1)
			{
				turret.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, targetAngle); break;
			}
			if(diff > 0)
				turret.transform.localRotation = Quaternion.Euler(0.0f,0.0f,angle + Time.deltaTime * 50);
			else
				turret.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle - Time.deltaTime * 50);
			yield return new WaitForEndOfFrame();
		}
	}

	internal GameObject shootBullet(GameObject bulletPrefab)
	{
		GameObject bullet = GameObject.Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation * Quaternion.Euler(90, 90, 0));
		bullet.transform.position += -bullet.transform.up * 1;
		bullet.GetComponent<Rigidbody>().AddForce(-1000 * bullet.transform.up);
		return bullet;
	}

	public float getTurretAngleDifference(Transform other)
	{
		Vector3 targetDirection = other.position - transform.position;
		targetDirection.y = 0;
		targetDirection = transform.InverseTransformDirection(targetDirection);
		float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(targetDirection.z, targetDirection.x) - 90;
		float angle = turret.transform.localEulerAngles.z;

		float diff = targetAngle - angle;
		while (diff > 180)
			diff -= 360;
		while (diff < -180)
			diff += 360;
		return diff;
	}



}
