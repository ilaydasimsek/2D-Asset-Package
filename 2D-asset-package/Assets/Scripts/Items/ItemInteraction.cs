using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    public string itemName;
    [Range(0f,1f)] public float destructionTime = 0.75f; // the time needed to be destroyed.

    private GameObject playerObject=null;
    private bool _isFlying = false;
    private bool _isInteracting = false;

    void Update()
    {
        if (_isInteracting && playerObject)
        {
            _isFlying = playerObject.transform.GetComponent<PlayerMovement>().isFlying;
            if (Input.GetKey(KeyCode.Space) && !_isFlying)
            {
                playerObject.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
                playerObject.transform.GetComponent<PlayerMovement>().itemInteraction = true;
                StartCoroutine(Stun());

            }
        }      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isInteracting = true;
            playerObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isInteracting = false;
            playerObject = null;
        }
    }
    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(destructionTime);
        playerObject.transform.GetComponent<PlayerMovement>().itemInteraction = false;
        if(itemName == "Red Potion" ||
           itemName == "Green Potion" ||
           itemName == "Purple Potion")
        {
            gameObject.transform.GetComponent<PotionAction>().PotionEffect(itemName, playerObject);
        }
        else
        {
        Destroy(this.gameObject);
        }
    }
}
