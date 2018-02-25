using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public float maxHealth = 100f;
    [Range(0.2f, 3f)] public float showTime = 1.5f;
    public Canvas healthBarCanvas;

    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public bool isProtected = false;
    private Transform healthBar;
    private Transform barPoint;
    private GameObject healthBarObject;

    private void Awake()
    {
        barPoint = transform.Find("BarPoint");
        healthBar = healthBarCanvas.transform.Find("HealthBar");
        currentHealth = maxHealth;
    }
    private void FixedUpdate()
    {
        if (healthBarObject != null)
        {
            healthBarObject.transform.position = barPoint.transform.position;
        }
    }


    public void DamageTaken(float dealtDamage)
    {

        currentHealth -= dealtDamage;
        showHealthBar();
        
    }
    public void Regeneration(float healthTaken){
        currentHealth+= healthTaken;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        showHealthBar();
    }
    public IEnumerator SpeedUp(float changeRate, float effectTime)
    {
        //increase rate is between -1 and 1. Negative means decreasing.
        float _maxSpeed = gameObject.transform.GetComponent<PlayerMovement>().maxSpeed;
        float _acceleration = gameObject.transform.GetComponent<PlayerMovement>().acceleration;
        float _jumpForce = gameObject.transform.GetComponent<PlayerMovement>().jumpForce;
        gameObject.transform.GetComponent<PlayerMovement>().maxSpeed += _maxSpeed * changeRate;
        gameObject.transform.GetComponent<PlayerMovement>().acceleration += _acceleration * changeRate;
        gameObject.transform.GetComponent<PlayerMovement>().jumpForce += _jumpForce * changeRate;
        yield return new WaitForSeconds(effectTime);
        Debug.Log("Returning back to old speed");
        //returning back to old values after `effecTime` seconds.
        gameObject.transform.GetComponent<PlayerMovement>().maxSpeed = _maxSpeed;
        gameObject.transform.GetComponent<PlayerMovement>().acceleration = _acceleration;
        gameObject.transform.GetComponent<PlayerMovement>().jumpForce = _jumpForce;

    }
    private void showHealthBar(){
        GameObject[] images = GameObject.FindGameObjectsWithTag("HealthBar");
        for(int i = 0; i < images.Length; i++) {
            Destroy(images[i]);
        }
        healthBar.GetComponent<Image>().fillAmount = (currentHealth) / maxHealth;
        healthBarObject = Instantiate(healthBarCanvas.gameObject, barPoint.position,barPoint.rotation) as GameObject;
        Destroy(healthBarObject, showTime);
    }

    
}
