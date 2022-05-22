using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Shooting")]
    private Transform target;
    public float range = 15f;
   
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public string enemiesTag = "Angel";
    public float turnSpeed = 10f;
    public Transform pRotate;

    public GameObject bulletPrefab;
    //where the bullet will spawn from
    public Transform firePoint;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemiesTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {

            //get the distance to angel - stored as a float
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                //finding all enemies , get the distance, check to see if is the shortest distance and set that as the current enemy 
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            else
            {
                //added as the turret did not lose target past viable shooting range 
                target = null;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if we don't have a target we do nothing. 
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //convert to eulerAngles as we want to only rotate on the YAxis & smooth out the turning 
        Vector3 rotation = Quaternion.Lerp(pRotate.rotation,lookRotation,Time.deltaTime * turnSpeed).eulerAngles;
        pRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);

        if(fireCountdown<=0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        //eversencond it will be reduced by 1
        fireCountdown -= Time.deltaTime;

    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        Debug.Log("HasHit");

        if (bullet != null)
            bullet.Chase(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
