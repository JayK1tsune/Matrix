using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CubeMove : MonoBehaviour
{
    private Vec3 xAxis = new Vec3(1f, 0f, 0f);
    private Vec3 yAxis = new Vec3(0f, 1f, 0f);

    private Vec3 position = new Vec3(0f, 0f, 0f);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(position.ToVector3(), (position + (xAxis * 3)).ToVector3());
        Gizmos.color = Color.green;
        Gizmos.DrawLine(position.ToVector3(), (position + (yAxis * 3)).ToVector3());
        //produce Z axis from crossProduct of X axis and Y axis and color it blue
        Gizmos.color = Color.blue;
        Vec3 zAxis = Vec3.CrossProduct(xAxis, yAxis);
        //axis product should be perpendicular to plane created by vectors xAxis and yAxis 
        Gizmos.DrawLine(position.ToVector3(), (position + (zAxis * 3)).ToVector3());
        Gizmos.color = Color.white;
        Gizmos.DrawLine(position.ToVector3(), (position + Velocity).ToVector3());

    }
    private Vec3 Velocity = new Vec3(0f, 2f, 0f);
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //update position variable
        position += Velocity * Time.deltaTime;
        //set position variable back into transform.position
        transform.position = position.ToVector3();
        
        if (position.y > 5f || position.y < -5f)
        {
            Velocity = -Velocity;
        }
    }
}
