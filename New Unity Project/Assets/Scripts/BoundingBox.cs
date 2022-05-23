using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox
{
    private static Vec3 xAxis = new Vec3(1f, 0f, 0f);
    private static Vec3 yAxis = new Vec3(0f, 1f, 0f);
    private static Vec3 zAxis = new Vec3(0f, 0f, 1f);



    private float xMin = 0f;
    private float xMax = 0f;
    private float yMin = 0f;
    private float yMax = 0f;
    private float zMin = 0f;
    private float zMax = 0f;


    private Vec3 v3Position = new Vec3(0f, 0f, 0f);
    public Vec3 Position
    {
        set { v3Position = value; calcExtremes(); }
        get { return v3Position; }
    }
    private Vec3 v3Extents = new Vec3(0f, 0f, 0f);
    public Vec3 Extents
    {
        set { v3Extents = value; calcExtremes(); }
        get { return v3Extents; }
    }

    private void calcExtremes()
    {
        xMin = v3Position.x - v3Extents.x;
        xMax = v3Position.x + v3Extents.x;
        yMin = v3Position.y - v3Extents.y;
        yMax = v3Position.y + v3Extents.y;
        zMin = v3Position.z - v3Extents.z;
        zMax = v3Position.z + v3Extents.z;
    }
    public bool containsObject(Vec3 a_pos, Vec3 a_bounds)
    {
        Vec3 half_a_bounds = a_bounds * 0.5f;
        if (a_pos.x - half_a_bounds.x < xMax && a_pos.x + half_a_bounds.x > xMin &&
            a_pos.y - half_a_bounds.y < yMax && a_pos.y + half_a_bounds.y > yMin &&
            a_pos.z - half_a_bounds.z < zMax && a_pos.z + half_a_bounds.z > zMin)
        {
            return true;
        }
        return false;


    }
    
    public void Draw()
    {
        
        Debug.DrawLine(new Vector3(xMin, yMax, zMin), new Vector3(xMin, yMin, zMin), Color.green);
        Debug.DrawLine(new Vector3(xMin, yMax, zMax), new Vector3(xMin, yMin, zMax), Color.green);
        Debug.DrawLine(new Vector3(xMax, yMax, zMin), new Vector3(xMax, yMin, zMin), Color.green);
        Debug.DrawLine(new Vector3(xMax, yMax, zMax), new Vector3(xMax, yMin, zMax), Color.green);

        Debug.DrawLine(new Vector3(xMin, yMax, zMin), new Vector3(xMin, yMax, zMax), Color.green);
        Debug.DrawLine(new Vector3(xMax, yMax, zMin), new Vector3(xMax, yMax, zMax), Color.green);
        Debug.DrawLine(new Vector3(xMin, yMax, zMin), new Vector3(xMax, yMax, zMin), Color.green);
        Debug.DrawLine(new Vector3(xMin, yMax, zMax), new Vector3(xMax, yMax, zMax), Color.green);

        Debug.DrawLine(new Vector3(xMin, yMin, zMin), new Vector3(xMin, yMin, zMax), Color.green);
        Debug.DrawLine(new Vector3(xMax, yMin, zMin), new Vector3(xMax, yMin, zMax), Color.green);
        Debug.DrawLine(new Vector3(xMin, yMin, zMin), new Vector3(xMax, yMin, zMin), Color.green);
        Debug.DrawLine(new Vector3(xMin, yMin, zMax), new Vector3(xMax, yMin, zMax), Color.green);
        

    }
    public BoundingBox(Vec3 a_origin, Vec3 a_extents)
    {
        v3Position = a_origin;
        Extents = a_extents;
    }

}
