using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleControl : MonoBehaviour {
	
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

	public GameObject bulletPrefab;


	// Use this for initialization
	void Start () {
		
	}

	float rotY = 0;
	float rotZ = 0;
	
	// Update is called once per frame
	void Update () {
		//Keyboard moves =======================================//
        //Forward Move
        if (Input.GetKey(KeyCode.W) || Input.GetAxis("AxisMoveH") < 0)
        {
            //Left wheels rotate
            foreach (GameObject wheelL in LeftWheels)
            {
                wheelL.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
            }
            //Right wheels rotate
            foreach (GameObject wheelR in RightWheels)
            {
                wheelR.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
            }
            //left track texture offset
            LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f,Time.deltaTime*tracksSpeed);
            //right track texture offset
            RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);

            //Move Tank
            transform.Translate(new Vector3(0f, 0f, forwardSpeed));
        }
        //Back Move
        if (Input.GetKey(KeyCode.S) || Input.GetAxis("AxisMoveH") > 0)
        {
            //Left wheels rotate
            foreach (GameObject wheelL in LeftWheels)
            {
                wheelL.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
            }
            //Right wheels rotate
            foreach (GameObject wheelR in RightWheels)
            {
                wheelR.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
            }
            //left track texture offset
            LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
            //right track texture offset
            RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
            //Move Tank
            transform.Translate(new Vector3(0f, 0f, -forwardSpeed));
        }
        //On Left
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("AxisMoveV") < 0)
        {
            //Left wheels rotate
            foreach (GameObject wheelL in LeftWheels)
            {
                wheelL.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
            }
            //Right wheels rotate
            foreach (GameObject wheelR in RightWheels)
            {
                wheelR.transform.Rotate(new Vector3(wheelsSpeed, 0f, 0f));
            }
            //left track texture offset
            LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);
            //right track texture offset
            RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
            //Rotate Tank
            transform.Rotate(new Vector3(0f,-rotateSpeed,0f));
        }
        //On Right
        if (Input.GetKey(KeyCode.D) || Input.GetAxis("AxisMoveV") > 0)
        {
            //Left wheels rotate
            foreach (GameObject wheelL in LeftWheels)
            {
                wheelL.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
            }
            //Right wheels rotate
            foreach (GameObject wheelR in RightWheels)
            {
                wheelR.transform.Rotate(new Vector3(-wheelsSpeed, 0f, 0f));
            }
            //left track texture offset
            LeftTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * -tracksSpeed);
            //right track texture offset
            RightTrack.transform.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0f, Time.deltaTime * tracksSpeed);
            //Rotate Tank
            transform.Rotate(new Vector3(0f, rotateSpeed, 0f));
        }


		float newAngle = gun.transform.localEulerAngles.y - Input.GetAxis("Vertical");
		while(newAngle > 180)
			newAngle -= 360;
		while (newAngle < -180)
			newAngle += 360;
		newAngle = Mathf.Clamp(newAngle, -30, 20);

		gun.transform.localEulerAngles = new Vector3(0, newAngle, 0);

		turret.transform.Rotate(0, 0, -Input.GetAxis("Horizontal"));

		
		if(Input.GetButtonDown("Fire1"))
		{
			GameObject bullet = GameObject.Instantiate(bulletPrefab, this.gun.transform.position, this.gun.transform.rotation * Quaternion.Euler(90, 90, 0));

			bullet.transform.position += -bullet.transform.up * 1;

			bullet.GetComponent<Rigidbody>().AddForce(-1000 * bullet.transform.up);


		}





	}
}
