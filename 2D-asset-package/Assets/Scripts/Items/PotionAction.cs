using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAction : MonoBehaviour {

    [Header("Red&Green Potion")]//increases health
    public float healthIncrease;

    [Header("Purple&Green Potion")]//increases speed and jump height.
    [Range(0f, 1f)] public float increaseRate;
    [Range(1f, 10f)] public float increaseTime;

    [Header("Only Green Potion")]//works randomly.
    [Range(0f, 8f)] public float stunTime; //The player cannot move for a certain time.
    [Range(0f, 8f)] public float protectedTime; // No damage can be dealt to the player for a certain time.
    [Range(0f, 8f)] public float slowTime; //decreases speed and jump height for a certain time.

    private bool hasUsed = false;
    public void PotionEffect(string _itemName, GameObject playerObject)
    {
        if (hasUsed)
        {
            return;
        }
        hasUsed = true;
        if(_itemName == "Red Potion")
        {
            playerObject.transform.GetComponent<PlayerManager>().Regeneration(healthIncrease);
        }
        else if (_itemName == "Purple Potion")
        {
            StartCoroutine(playerObject.transform.GetComponent<PlayerManager>().SpeedUp(increaseRate, increaseTime));
        }
        else if(_itemName == "Green Potion")
        {
            GreenPotion(playerObject);
        }
        else
        {
            Debug.Log("Exception!");
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ItemInteraction>().enabled = false;
        Destroy(this.gameObject,10f);
    }

    private void GreenPotion(GameObject playerObject)
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue >= 0 && randomValue <= 20)// increasing health
        {
            playerObject.transform.GetComponent<PlayerManager>().Regeneration(healthIncrease);
            Debug.Log("Green Pot-> Regeneration");
        }
        else if (randomValue > 20 && randomValue <= 40)// speeding up
        {
            StartCoroutine(playerObject.transform.GetComponent<PlayerManager>().SpeedUp(increaseRate, increaseTime));
            Debug.Log("Green Pot-> Speeding Up");
        }
        else if (randomValue > 40 && randomValue <= 60)// speeding down, notice the minus sign.
        {
            StartCoroutine(playerObject.transform.GetComponent<PlayerManager>().SpeedUp(-increaseRate, slowTime));
            Debug.Log("Green Pot-> Slow Down");
        }
        else if (randomValue > 60 && randomValue <= 80)//Stun, the player cannot move
        {
            StartCoroutine(Stun(playerObject));
            Debug.Log("Green Pot-> Stun! Can't move");
        }
        else // No damage can be dealt to the player.
        {
            StartCoroutine(ProtectPlayer(playerObject));
            Debug.Log("Green Pot->No damage can be taken!");
        }
    }
    private IEnumerator Stun(GameObject playerObject)
    {
        Debug.Log("Stunned!");
        playerObject.transform.GetComponent<PlayerMovement>().isStunned = true;
        yield return new WaitForSeconds(stunTime);
        playerObject.transform.GetComponent<PlayerMovement>().isStunned = false;
    }
    private IEnumerator ProtectPlayer(GameObject playerObject)
    {
        playerObject.transform.GetComponent<PlayerManager>().isProtected = true;
        yield return new WaitForSeconds(protectedTime);
        playerObject.transform.GetComponent<PlayerManager>().isProtected = false;

    }
}

