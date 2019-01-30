using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour, DrivingBehaviour {

	public Transform route;
	int headingIndex;
	Transform headingNode;

	// Use this for initialization
	void Start () {
		headingIndex = findClosestNodeIndex();
		headingNode = route.GetChild(headingIndex);
	}


	// Update is called once per frame
	void Update () {

		Vector3 direction = headingNode.position - transform.position;
		direction.y = 0;

		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*50);
		this.transform.position = Vector3.MoveTowards(this.transform.position, headingNode.position, Time.deltaTime);

		if (Vector3.Distance(transform.position, headingNode.position) < 0.1)
			setNextNode();

		

	}

	private void setNextNode()
	{
		this.headingIndex = (this.headingIndex + 1) % route.childCount;
		this.headingNode = route.GetChild(headingIndex);
	}

	private int findClosestNodeIndex()
	{
		return 0;
	}

}
