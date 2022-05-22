using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public GameObject boomEffect;

    public void Chase(Transform _target)
    {
        target = _target;
    }    

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //the distance from the bullet to the target is equal to the dir.magnite
        if(dir.magnitude >= distanceThisFrame)
        {
            HitAngel();
            return;
        }
        //we normalize to make sure it moves ar a constant speed
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitAngel()
    {
        GameObject effectIns = (GameObject)Instantiate(boomEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);

    }
}
