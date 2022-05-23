using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vec3 v3CurrentVelocity;
    private Vec3 v3Acceleration;
    private float fLifespan = 0f;

    public Vec3 Velocity
    {
        get { return v3CurrentVelocity; }
        set { v3CurrentVelocity = value; }
    }
    public Vec3 Acceleration
    {
        set { v3Acceleration = value; }
        get { return v3Acceleration; }
       
    }
    public float Lifespan
    {
        set { fLifespan = value; }
        get { return fLifespan; }
    }

    public Vec3 Position
    {
        set { transform.position = value.ToVector3(); }
        get { return new Vec3(transform.position); }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float microTimeStep = Time.deltaTime * 0.1f;

        Vec3 currentPos = Position;
        //work our current velocity
        v3CurrentVelocity += v3Acceleration * microTimeStep;
        //work out displacment 
        Vec3 displacment = v3CurrentVelocity * microTimeStep;
        currentPos += displacment;
        Position = currentPos;

        fLifespan -= microTimeStep;
        if (fLifespan < 0f)
        {
            Destroy(gameObject);
        }

    }

}