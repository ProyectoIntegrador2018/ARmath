using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour {

	public void RotateOpen () {
		transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
	}

	public void RotateClose () {
		transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 0, 1));
	}
}