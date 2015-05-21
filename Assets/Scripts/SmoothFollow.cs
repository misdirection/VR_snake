// Smooth Follow from Standard Assets
using UnityEngine;
using System.Collections;
 
public class SmoothFollow : MonoBehaviour {
    
	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// How much we 
	public float rotationDamping = 3.0f;
 
	// Place the script in the Camera-Control group in the component menu
	[AddComponentMenu("Camera-Control/Smooth Follow")]
 
	void LateUpdate () {
		// Early out if we don't have a target
		if (!target) return;
 
		// Calculate the current rotation angles
		float wantedRotationAngleY = target.eulerAngles.y;
        float wantedRotationAngleX = target.eulerAngles.x;
 
		float currentRotationAngleY = transform.eulerAngles.y;
        float currentRotationAngleX = transform.eulerAngles.x;
 
		// Damp the rotation around the y-axis
		currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, rotationDamping * Time.deltaTime);

        // Damp the rotation around the x-axis
        currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX, rotationDamping * Time.deltaTime);
        
		// Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);

	
		// Set the position of the camera on the x-z plane to:
        transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Always look at the target
		transform.LookAt(target);
	}
}