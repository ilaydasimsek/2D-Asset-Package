using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour {
    public bool isSmooth = true;
    [Range(1f , 5f)] public float cameraSpeed = 0.1f;
    [Range(0f , 1f)] public float zoomDistance = 0.1f;
    public Transform lookAt;
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    void FixedUpdate () {
        if (isSmooth)
        {
            Vector3 velocity = lookAt.gameObject.transform.GetComponent<Rigidbody2D>().velocity;
            Vector3 zoom = new Vector3(0f, 0f, -Mathf.Sqrt(velocity.x*velocity.x)*zoomDistance);
            transform.position = Vector3.Lerp(transform.position, lookAt.transform.position+offset+zoom, cameraSpeed*Time.deltaTime);            
        }
        else
        {
            transform.position = lookAt.transform.position+offset;
        }
	}
}
