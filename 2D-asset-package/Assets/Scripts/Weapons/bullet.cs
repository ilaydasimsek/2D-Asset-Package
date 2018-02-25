using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float bulletSpeed;
    public float bulletDamage;
    public float bulletLifeTime = 5f;

    private Rigidbody2D _rigidbody;
    private Vector3 shotDirection = new Vector3(0f,0f,0f);
    private void Awake()
    {
        _rigidbody = gameObject.transform.GetComponent<Rigidbody2D>();
        Destroy(gameObject,bulletLifeTime);
    }
    public void ShootBullet(Transform targetTransform, Transform FirePoint){
        shotDirection = Vector3.Normalize(targetTransform.position - FirePoint.transform.position);
        _rigidbody.velocity = shotDirection*bulletSpeed*Time.deltaTime;                     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().DamageTaken(bulletDamage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Bullet"|| collision.gameObject.tag=="Item" )//collision between two bullets are allowed.
        {
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
