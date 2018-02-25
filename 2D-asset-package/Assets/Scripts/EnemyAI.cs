using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour {
    
    public GameObject target;
    public bool isExplosive;
    public float interpolant = 0.05f;
    public float minDist = 0.80f;
    public float waitTime = 2f;
    public float AIDamage=10f;
    [Range(0f, 8f)] public float range = 2f;
    public ParticleSystem explosion;

    private bool isSleeping = true;
    private bool isCollided = false;
    private Vector3 defaultPoint;
    private Vector3 randomPoint;




    void Start()
    {
        if(target == null)
        {
            return;
        }
        defaultPoint = transform.position;
        randomPoint = transform.position;

    }

    private void Update()
    {
        
        float dist = Vector3.Distance(this.transform.position, target.transform.position);
        if (dist <= minDist)
        {
            if (isExplosive)
            {
                Explosion();
                return;// protection
            }
            isCollided = true;
            target.GetComponent<PlayerManager>().DamageTaken(AIDamage);
            StartCoroutine("Wait");
        }
    }
    void FixedUpdate()
    {
        float distance = Vector3.Distance(defaultPoint, target.transform.position);
        if (distance <= range)
        {
            Attack();
        }
        else
        {
            GoToSleep();
        }
        
        


        

    }
    private void Attack()
    {
        float dist = Vector3.Distance(this.transform.position, target.transform.position);
        if (target == null || dist>range)
        {
            return;
        }

        if (isCollided == true && !isExplosive)
        {
            dist = Vector3.Distance(this.transform.position, target.transform.position + randomPoint);
            transform.position = Vector3.Lerp(transform.position, target.transform.position + randomPoint, interpolant / dist);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, interpolant / dist);
        }
    }
    private void GoToSleep()
    {
        if (!isCollided)
        {
            float dist = Vector3.Distance(this.transform.position, defaultPoint);
            transform.position = Vector3.Lerp(transform.position, defaultPoint, interpolant / dist);
        }
    }
    IEnumerator Wait()
    {
        randomPoint = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(3f, 8f), 0);
        yield return new WaitForSeconds(waitTime);
        isCollided = false;

    }
    private void Explosion()
    {
        target.GetComponent<PlayerManager>().DamageTaken(AIDamage);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(defaultPoint, range);
    }

}
