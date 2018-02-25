using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {
    private Animator anim;
    private bool _isInteracting;
    private GameObject _player;

    public GameObject[] doors;
    public float delayTime = 0.2f;
    private float InteractionTime;
    private float InteractionCountDown;
    void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("IsClosed", true);
        InteractionTime = delayTime * (doors.Length + 1);
    }
    private void FixedUpdate()
    {
        if (_isInteracting && Input.GetKeyDown("space"))
        {
            if (doors.Length == 1)
            {
                ChangeAnimationSingle();
            }
            else if(InteractionCountDown <= 0f)
            {
                InteractionCountDown = InteractionTime;
                StartCoroutine(ChangeAnimationMulti());
            }
        }
        if(doors.Length!=1)
        InteractionCountDown -= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject;
            _isInteracting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = null;
            _isInteracting = false;
        }
    }
    private IEnumerator ChangeAnimationMulti()
    {
        bool isclosed = anim.GetBool("IsClosed");

        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].transform.GetComponent<DoorControl>()._isActing)
            {
                goto end;// preserve the current state
            }

            if (isclosed)
            {
                
                doors[i].transform.GetComponent<DoorControl>().Open();
            }

            else if (!isclosed)
            {

                doors[i].transform.GetComponent<DoorControl>().Close();

            }
            else
            {
                Debug.Log("Exception found in SwitchControl.");
            }
            yield return new WaitForSeconds(delayTime);
        }
        //yield return new WaitForSeconds(delayTime);
        anim.SetBool("IsClosed", !isclosed);
        anim.SetBool("IsOpened", isclosed);
    end:
        yield return null;
    }
    private void ChangeAnimationSingle()
    {
        
        if (doors[0].transform.GetComponent<DoorControl>()._isActing)
        {
            return;
        }

        if (anim.GetBool("IsClosed"))
        {
            anim.SetBool("IsOpened", true);
            anim.SetBool("IsClosed", false);
            doors[0].transform.GetComponent<DoorControl>().Open();
        }

        else if (anim.GetBool("IsOpened"))
        {
            anim.SetBool("IsOpened", false);
            anim.SetBool("IsClosed", true);
            doors[0].transform.GetComponent<DoorControl>().Close();

        }
        else
        {
            Debug.Log("Exception found in SwitchControl.");
        }
        
    }
}
