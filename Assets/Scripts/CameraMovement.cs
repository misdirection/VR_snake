using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.W)) transform.Rotate(new Vector3(-90, 0, 0));
        if (Input.GetKeyUp(KeyCode.S)) transform.Rotate(new Vector3(90, 0, 0));
        if (Input.GetKeyUp(KeyCode.A)) transform.Rotate(new Vector3(0, -90, 0));
        if (Input.GetKeyUp(KeyCode.D)) transform.Rotate(new Vector3(0, 90, 0));
	}
}
