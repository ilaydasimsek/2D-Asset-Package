using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject player;
    public string levelname;
	
	// Update is called once per frame
	void Update () {
	
        if (player.GetComponent<PlayerManager>().currentHealth <= 0) {
            SceneManager.LoadScene(levelname);
        }
    }
 
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        SceneManager.LoadScene(levelname);
    }
}
