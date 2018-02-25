using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour {

    public bool IsContinuous;
    public GameObject bulletObj;

    [Header("Sudden Shooting Mode!")]
    [Range(1f, 10f)] public float SecondBetweenGroups;//the time between two group of bullets.
    public int numberOfBullets;// number of bullets in each group.

    [Header("Continuous Shooting Mode!")]
    [Range(1f, 10f)] public float SecondBetweenShots;


    private float shotTime;
    private Transform LeftshotPoint;
    private Transform RightshotPoint;
    private Transform transformRight;
    private Transform transformLeft;

    // Use this for initialization
    void Start() {
        LeftshotPoint = gameObject.transform.Find("LeftFirePoint");
        RightshotPoint = gameObject.transform.Find("RightFirePoint");
        transformRight = gameObject.transform.Find("RightEnd");
        transformLeft = gameObject.transform.Find("LeftEnd");
        if (IsContinuous)
        {
            shotTime = SecondBetweenShots;
        }
        else//will shoot as groups of bullets.
        {
            shotTime = SecondBetweenGroups;
        }
    }

    // Update is called once per frame
    void Update() {
        if (shotTime <= 0)
        {
            if (IsContinuous)
            {
                shotTime = SecondBetweenShots;
                ShootContinuously();
            }
            else
            {
                shotTime = SecondBetweenGroups;
                ShootDiscretely();
            }
        }
        shotTime -= Time.deltaTime;
    }
    private void ShootContinuously()
    {

        GameObject tempbulletObj = Instantiate(bulletObj, RightshotPoint.position, RightshotPoint.rotation) as GameObject;
        tempbulletObj.GetComponent<bullet>().ShootBullet(transformRight, RightshotPoint);

        GameObject tempbulletObj_2 = Instantiate(bulletObj, LeftshotPoint.position, LeftshotPoint.rotation) as GameObject;
        tempbulletObj_2.GetComponent<bullet>().ShootBullet(transformLeft, LeftshotPoint);
    }
    private void ShootDiscretely()
    {
        StartCoroutine(CreateGroup());
        
    }
    IEnumerator CreateGroup()
    {
        GameObject[] leftBullets = new GameObject[numberOfBullets];
        GameObject[] rightBullets = new GameObject[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            leftBullets[i] = Instantiate(bulletObj, RightshotPoint.position, RightshotPoint.rotation) as GameObject;
            leftBullets[i].GetComponent<bullet>().ShootBullet(transformRight, RightshotPoint);

            rightBullets[i] = Instantiate(bulletObj, LeftshotPoint.position, LeftshotPoint.rotation) as GameObject;
            rightBullets[i].GetComponent<bullet>().ShootBullet(transformLeft, LeftshotPoint);
            yield return new WaitForSeconds(0.2f);

        }

    }
}
