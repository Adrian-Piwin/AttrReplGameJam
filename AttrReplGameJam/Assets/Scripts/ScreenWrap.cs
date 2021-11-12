using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
	public float buffer;
	public float wrapDelay = 0f;
	public bool canWrap = true;

	private Camera cam;
	private float distanceZ, leftConstraint, rightConstraint, bottomConstraint, topConstraint;

     void Start () {
         cam = Camera.main;
         distanceZ = Mathf.Abs (cam.transform.position.z + transform.position.z);
 
         leftConstraint = cam.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, distanceZ)).x;
         rightConstraint = cam.ScreenToWorldPoint (new Vector3 (Screen.width, 0.0f, distanceZ)).x;
         bottomConstraint = cam.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, distanceZ)).y;
         topConstraint = cam.ScreenToWorldPoint (new Vector3 (0.0f, Screen.height, distanceZ)).y;
	 }
	 
	 void FixedUpdate (){
		// Left constriant
		if (transform.position.x < leftConstraint - buffer) {
			StartCoroutine(wrapObject(new Vector3 (rightConstraint + buffer, transform.position.y, transform.position.z)));
		}
		// Right constriant
		else if (transform.position.x > rightConstraint + buffer) {
			StartCoroutine(wrapObject(new Vector3 (leftConstraint - buffer, transform.position.y, transform.position.z)));
		}
		// Bottom constriant
		else if (transform.position.y < bottomConstraint - buffer) {
			StartCoroutine(wrapObject(new Vector3 (transform.position.x, topConstraint + buffer, transform.position.z)));
		}
		// Top constriant
		else if (transform.position.y > topConstraint + buffer) {
			StartCoroutine(wrapObject(new Vector3 (transform.position.x, bottomConstraint - buffer, transform.position.z)));
		}
	 }

	 IEnumerator wrapObject(Vector3 newPos){
	 	yield return new WaitForSeconds(wrapDelay);

	 	if (canWrap){
	 		transform.position = newPos;
	 	}
	 }
}
