using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptController : MonoBehaviour
{
    public float fVisionRadius = 10f;
    private List<Projectile> projectiles;

    public ProjectileLauncher launcher;
    // Start is called before the first frame update
    void Start()
    {
        projectiles = new List<Projectile>();
    }
    float findTimeIntercept(float launcherVelocity, Vec3 protecilePos, Vec3 projectileVel)
    {
        Vec3 directionShooterToProjectile = new Vec3(transform.position) - protecilePos;
        float distanceToProjectileSquared = directionShooterToProjectile.MagnitudeSquared();
        Vec3 horizontalProjectileVelocity = new Vec3(projectileVel.x, 0f, projectileVel.z);

        float c = -(distanceToProjectileSquared);
        float b = 2 * Vec3.DotProduct(directionShooterToProjectile, horizontalProjectileVelocity);
        float a = launcherVelocity * launcherVelocity - horizontalProjectileVelocity.Dot(horizontalProjectileVelocity);
        float timeToIntercept = UseQuadraticForumal(a, b, c);
        return timeToIntercept;
    }
    float UseQuadraticForumal(float a, float b, float c)
    {
        if (0.0001f > Mathf.Abs(a))
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
 




    // Update is called once per frame
    void Update()
    {
        




    }
    private void FixedUpdate()
    {
        projectiles.Clear();
        GameObject[] allActiveProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        Vec3 position = new Vec3(transform.position);
        float radSqrd = fVisionRadius * fVisionRadius;
        foreach (GameObject projectile in allActiveProjectiles)
        {
            Vec3 vecToProjectile = new Vec3(projectile.transform.position) - position;
            float distanceToProjectile = vecToProjectile.MagnitudeSquared();
            if (distanceToProjectile < radSqrd)
            {
                Projectile p = projectile.GetComponent<Projectile>();
                projectiles.Add(p);
            }
        }
        List<(int id, float time)> interceptTimes = new List<(int id, float time)>();
        for (int i = 0; i < projectiles.Count; ++i)
        {
            //Debug.Log(launcher.launchVelocity);
            //Debug.Log(projectiles[i].Position);
            //Debug.Log(projectiles[i].Velocity);

            interceptTimes.Add((i, findTimeIntercept(launcher.launchVelocity, projectiles[i].Position, projectiles[i].Velocity)));

            int index = -1;
            float fiTime = float.MaxValue;
            foreach (var intercep in interceptTimes)
            {
                if (intercep.time < fiTime)
                {
                    fiTime = intercep.time;
                    index = intercep.id;
                }
            }

            if (index != -1)
            {
                Debug.Log("Closest intercept time is: " + fiTime + " For Item at index" + index);
                //calculate the pos of the projectile at this time interval
                Projectile p = projectiles[index];
                //get future pos using s = ut + 1/2at^2
                Vec3 predictedPos = p.Position + (p.Velocity * fiTime + p.Acceleration * 0.5f * fiTime * fiTime);
                Vec3 dirToPPos = predictedPos - position;
                float distToTarget = dirToPPos.Normalize();
                float hasFired = 1.0f;
                
               
                launcher.FireProjectile(dirToPPos);
               

            }


        }
    }
}
