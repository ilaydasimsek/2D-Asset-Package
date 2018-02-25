using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour {
    [Header("Attributes")]
    public float shootingRange;
       
    [Range(2f,10f)]public float SecondForPerShot;
    [Range(0f,5f)]public float errorMargin;
    public bool isFacingRight;

    [Header("Objects")]
    public GameObject bulletObj;

    private Transform shotPoint;
    private GameObject target;
    private float shotTime;

    void Awake()
    {
        target = GameObject.FindWithTag("Player");
        shotPoint = gameObject.transform.Find("FirePoint");
        shotTime = SecondForPerShot;
    }
    private void Update()
    {
        
        if(shotTime <= 0 && Vector3.Distance(target.transform.position,transform.position) <= shootingRange){
            shotTime = SecondForPerShot;
            Shoot();
        }
        shotTime-= Time.deltaTime;
    }
    void Shoot(){
        if(isFacingRight && target.transform.position.x<shotPoint.transform.position.x){
            return;
        }
        if(!isFacingRight && target.transform.position.x>shotPoint.transform.position.x){
            return;
        }
        GameObject tempbulletObj = Instantiate(bulletObj, shotPoint.position, shotPoint.rotation) as GameObject;
        tempbulletObj.GetComponent<bullet>().ShootBullet(target.transform, shotPoint);

    }


    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
