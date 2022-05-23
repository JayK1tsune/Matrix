using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public float launchVelocity = 200f;
    public float Gravity = -9.8f;

    public GameObject projectile; // the bullet that is shooting in the scene 
    public GameObject launchPoint; // where the bullet fires from.
    public Vector3 RotateSpeed = new Vector3(10.0F, 10.0F, 10.0F);

    private Vec3 v3InitalVerlocity;
    
    private Vec3 v3Acceleration;

    private float airTime = 0f;
   
    private float HorisontialDisplacment = 0f;
    private float shotCooldownTimer = 1.0f;

    private List<Vec3> pathPoints;  // lists of points on a path for drawring projectile arc
    private int simulationSteps = 30; //number of points on the path

    private Transform tr;
    private float gunTimer;




    // Start is called before the first frame update
    void Start()
    {
        Vector3 euler = transform.eulerAngles; //Starts the turret in a random pos on the y axis from -90 - 0f. this is apllied to all turrets and start are diffrent locations 
        euler.y = Random.Range(-90f, 0f);
        transform.eulerAngles = euler;


        //https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
        tr = GetComponent("Transform") as Transform;




        pathPoints = new List<Vec3>();
        calculateProjectile();
        caclulatePath();
    }

    private void calculateProjectile()
    {
        Vec3 lp = new Vec3(launchPoint.transform.position);
        Vec3 lf_fwd = new Vec3(launchPoint.transform.forward);
        lf_fwd.Normalize();

        // work out highet offest
        float launchHeight = lp.y;
        //work out Vel as a vector 
        v3InitalVerlocity = lf_fwd * launchVelocity;
        v3Acceleration = new Vec3(0f, Gravity, 0f);
        //use quadratic to find airtime

        airTime = Qaudratic(v3Acceleration.y, v3InitalVerlocity.y * 2f, launchHeight * 2f);
        HorisontialDisplacment = airTime * v3InitalVerlocity.z;
    }

    private float Qaudratic(float a, float b,float c)
    {
        if(0.0001f>Mathf.Abs(a))
        {
            return 0f;
        }
        float bb = b * b;
        float ac = a * c;
        float b4ac = Mathf.Sqrt(bb - 4f * ac);
        float t1 = (-b + b4ac) / (2f * a);
        float t2 = (-b - b4ac) / (2f * a);
        float t = Mathf.Max(t1, t2);
        return t;

    }

    private void caclulatePath()
    {
        Vec3 launchpos = new Vec3(launchPoint.transform.position);
        pathPoints.Add(launchpos);
        for(int i =0; i <= simulationSteps; ++i)
        {
            float simTime = (i / (float)simulationSteps) * airTime;
            // suvat forumal for displacment  s = ut + 1/2 at^2
            Vec3 displacment = (v3InitalVerlocity * simTime) + (0.5f * v3Acceleration * (simTime * simTime));
            Vec3 drawPoint = launchpos + displacment;
            pathPoints.Add(drawPoint);
        }
    }
    void drawPath()
    {
        for(int i = 0; i <pathPoints.Count-1; ++i)
        {
            Debug.DrawLine(pathPoints[i].ToVector3(), pathPoints[i + 1].ToVector3(), Color.green);
        }
    }

    public void FireProjectile()
    {
            GameObject p = Instantiate(projectile, launchPoint.transform.position, launchPoint.transform.rotation);
            p.GetComponent<Projectile>().Velocity = v3InitalVerlocity;
            p.GetComponent<Projectile>().Acceleration = v3Acceleration;
            p.GetComponent<Projectile>().Lifespan = airTime; 
    }
    public void FireProjectile(Vec3 direction)
    {
        gunTimer -= Time.deltaTime;


        transform.rotation = Quaternion.LookRotation(direction.ToVector3());
        if (gunTimer <= 0)
        {
            GameObject p = Instantiate(projectile, launchPoint.transform.position, launchPoint.transform.rotation);
            p.GetComponent<Projectile>().Velocity = v3InitalVerlocity;
            p.GetComponent<Projectile>().Acceleration = v3Acceleration;
            p.GetComponent<Projectile>().Lifespan = airTime;

            gunTimer = 1f;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
       
       
        pathPoints.Clear();
        calculateProjectile();
        caclulatePath();
        drawPath();

        if (gameObject.transform.rotation.eulerAngles.y < -200f || gameObject.transform.rotation.eulerAngles.y > 0f)
        {
            //testing rotation so it can go back on itself when reaching bounders
            //currently not working great. FML
            tr.Rotate(Time.deltaTime * RotateSpeed);
        }





        if (shotCooldownTimer <= 0.0f)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                FireProjectile();
                
            }
            if (Random.Range(0, 200) == Random.Range(0,200)) // random shooting on a CD 
            {
                FireProjectile();
                shotCooldownTimer = 5.0f;
            }
          
        }
        else
        {
            shotCooldownTimer -= Time.deltaTime;
        }





    }
}
