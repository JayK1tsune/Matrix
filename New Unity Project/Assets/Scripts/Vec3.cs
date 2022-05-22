using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec3
{

    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public Vec3()
    {
        x = 0f; y = 0f; z = 0f;
    }

    public Vec3(float a_x, float a_y, float a_z)
    {
        x = a_x; y = a_y; z = a_z;
    }

    //vec3 v = new Vec3(transform. position);
    public Vec3(Vector3 a_v3)
    {
        x = a_v3.x; y = a_v3.y; z = a_v3.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }


    //overload the add (+) operator 
    public static Vec3 operator +(Vec3 a, Vec3 b)
    {
        return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    //overload the add (-) operator 
    public static Vec3 operator -(Vec3 a, Vec3 b)
    {
        return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    //negate operator Vec3 c = -a;
    public static Vec3 operator -(Vec3 a)
    {
        return new Vec3(-a.x, -a.y, -a.z);
    }
    //overload for multiplication 
    public static Vec3 operator *(Vec3 a, Vec3 b)
    {
        return new Vec3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    //overload operators for * by scalar value 
    public static Vec3 operator *(Vec3 a, float s)
    {
        return new Vec3(a.x * s, a.y * s, a.z * s);
    }
    public static Vec3 operator *(float s, Vec3 a)
    {
        return new Vec3(a.x * s, a.y * s, a.z * s);
    }

    //Calculating magnitude can be done in two steps Vector A is added to Vector B to produce Vector C.
    // The components of Vector C are then squared and summed together then the square root is performed.
    public float Magnitude()
    {
        return Mathf.Sqrt(x * x + y * y + z * z);
    }
    public float MagnitudeSquared()
    {
        return x * x + y * y + z * z;
    }

    //dot product 
    public float Dot(Vec3 a_b)
    {
        return x * a_b.x + y * a_b.y + z * a_b.z;
    }

    //calling this way float dp = dotproduct ( a, b )
    public static float DotProduct(Vec3 a, Vec3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    //normalize a vec2 : reduce length of vec2 to unit length
    public float Normalize()
    {
        float mag = Magnitude();
        float fInvMag = (mag != 0f) ? 1f / mag : 1.00e-12f;
        x *= fInvMag;
        y *= fInvMag;
        z *= fInvMag;
        return mag;
    }

    public static Vec3 CrossProduct(Vec3 a, Vec3 b)
    {
        return new Vec3(a.y * b.z - a.z * b.y,
                        a.z * b.x - a.x * b.z,
                        a.x * b.y - a.y * b.x);
    }

    //rotate on z Axis 
    public void RotateZ(float angle)
    {
        float fx = x;
        x = fx * Mathf.Cos(angle) - y * Mathf.Sin(angle);
        y = fx * Mathf.Sin(angle) + y * Mathf.Cos(angle);
    }
    //rotate on Y Axis 
    public void RotateY(float angle)
    {
        float fx = x;
        x = fx * Mathf.Cos(angle) - z * Mathf.Sin(angle);
        z = fx * Mathf.Sin(angle) + z * Mathf.Cos(angle);
    }
    //rotate on X Axis 
    public void RotateX(float angle)
    {
        float fy = y;
        y = fy * Mathf.Cos(angle) - z * Mathf.Sin(angle);
        z = fy * Mathf.Sin(angle) + z * Mathf.Cos(angle);
    }
    
}
