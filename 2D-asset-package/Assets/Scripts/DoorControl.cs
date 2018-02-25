using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorControl : MonoBehaviour {

    [Range(0.5f,4f)]public float doorSpeed = 2f;

    [HideInInspector]
    public bool _isActing = false;

    private Transform upper, lower;
    private Vector3 newPos=Vector3.zero;
    private void Start()
    {
        upper = transform.Find("UpperEnd");
        lower = transform.Find("LowerEnd");
    }
    private void FixedUpdate()
    {
        if (newPos == Vector3.zero)
        {
            _isActing = false;
        }
        else if (Vector3.Distance(newPos, transform.position) <= 0.02f)
        {
            Debug.Log("Door has become closed/opened.");
            newPos = Vector3.zero;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, newPos, doorSpeed*Time.deltaTime);
        }
    }
    public void Open()
    {
        if (!_isActing)
        {
            _isActing = true;
            float newPosY = (-upper.position.y + lower.position.y) / 2;
            newPos = new Vector3(transform.position.x, newPosY, transform.position.z);
        }
    } 

    public void Close()
    {
        if (!_isActing)
        {
            _isActing = true;
            float newPosY = (upper.position.y - lower.position.y) / 2;
            newPos = new Vector3(transform.position.x, newPosY, transform.position.z);
        }
    }
}
