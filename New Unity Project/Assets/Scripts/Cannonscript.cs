// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// 
// public class Cannonscript : MonoBehaviour
// {
//     public float launchVelocity = 900f;
//     public float Gravity = -9.8f;
//     public float launchAngle;
// 
//     public Vec3 v3InitalVerlocity = new Vec3();
//     
//     private Vec3 v3Acceleration;
// 
//     private float airTime = 0f;
//     private float horizontalDisplacement = 0f;
// 
//     private bool simulate = false;
//     private List<Vec3> pathPoints;
//     private int simulationSteps = 30;
// 
//     public GameObject projectile;
//     public GameObject launchPoint;
// 
// 
//     // Start is called before the first frame update
//     void Start()
//     {
//         pathPoints = new List<Vec3>();
//         
//         calculateProjectile();
//         calculatePath();
//     }
// 
//     private void calculateProjectile()
//     {
//         launchAngle = transform.parent.eulerAngles.x;
//         float launchHeight = launchPoint.transform.position.y;
// 
//         v3InitalVerlocity.x = 0f;
//         v3InitalVerlocity.z = launchVelocity * Mathf.Cos(launchAngle * Mathf.Deg2Rad);
//         v3InitalVerlocity.y = launchVelocity * Mathf.Sin(launchAngle * Mathf.Deg2Rad);
// 
//         Vector3 txDirection = launchPoint.transform.TransformDirection(v3InitalVerlocity.ToVector3());
//         v3InitalVerlocity = new Vec3(txDirection);
//         v3Acceleration = new Vec3(0f, Gravity, 0f);
// 
//         
// 
//         airTime = useQuadraticForumal(v3Acceleration.y, v3InitalVerlocity.y * 2f, launchHeight * 2f);
//         horizontalDisplacement = airTime * v3InitalVerlocity.z;
//     }
// 
//     float useQuadraticForumal(float a,float b, float c)
//     {
//         if (0.0001f > Mathf.Abs(a))
//         {
//             return 0f;
//         }
//         float bb = b * b;
//         float ac = a * c;
//         float b4ac = Mathf.Sqrt(bb - 4f * ac);
//         float t1 = (-b + b4ac) / (2f * a);
//         float t2 = (-b - b4ac) / (2f * a);
//         float t = Mathf.Max(t1, t2);
//         return t;
//     }
//     private void calculatePath()
//     {
//         Vec3 launchPos = new Vec3(launchPoint.transform.position);
//         pathPoints.Add(launchPos);
// 
//         for (int i = 0; i<= simulationSteps; ++i)
//         {
//             float simTime = (i / (float)simulationSteps * airTime);
//             Vec3 displacment = v3InitalVerlocity * simTime + v3Acceleration * simTime * simTime * 0.5f;
//             Vec3 drawPoint = launchPos + displacment;
//             pathPoints.Add(drawPoint);
//         }
//     }
//     void drawPath()
//     {
//         for (int i=0; i < pathPoints.Count-1; ++i)
//         {
//             Debug.DrawLine(pathPoints[i].ToVector3(), pathPoints[i + 1].ToVector3(), Color.green);
//         }
//     }
// 
// 
// 
//     // Update is called once per frame
//     void Update()
//     {
//         
//             pathPoints = new List<Vec3>();
//             calculateProjectile();
//             calculatePath();
//         
//         drawPath();
// 
//         if(Input.GetKeyDown(KeyCode.Space))
//         {
//             simulate = true;
// 
//             GameObject p = Instantiate(projectile, launchPoint.transform.position, launchPoint.transform.rotation);
//             p.GetComponent<Projectile>().SetVelocity(v3InitalVerlocity);
//             p.GetComponent<Projectile>().SetAcceleration(v3Acceleration);
//             p.GetComponent<Projectile>().SetLifeTime(airTime);
//       
//         }
//     }
// }
