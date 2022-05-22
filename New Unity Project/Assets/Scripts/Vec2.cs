using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec2
{

    public float x = 0f;
    public float y = 0f;
    public Vec2()
    {
        x = 0f; y = 0f;
    }

    public Vec2(float a_x, float a_y)
    {
        x = a_x; y = a_y;
    }
    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, 0f);
    }


    //overload the add (+) operator 
    public static Vec2 operator +(Vec2 a, Vec2 b)
    {
        return new Vec2(a.x + b.x, a.y + b.y);
    }

    //overload the add (-) operator 
    public static Vec2 operator -(Vec2 a, Vec2 b)
    {
        return new Vec2(a.x - b.x, a.y - b.y);
    }

    public static Vec2 operator -( Vec2 a)
    {
        return new Vec2(-a.x, -a.y);
    }

    //Calculating magnitude can be done in two steps Vector A is added to Vector B to produce Vector C.
    // The components of Vector C are then squared and summed together then the square root is performed.
    public float Magnitude()
    {
        return Mathf.Sqrt(x * x + y * y);
    }
    public float MagnitudeSquared()
    {
        return x * x + y * y;
    }

    //dot product 
    public float Dot(Vec2 a_b)
    {
        return x * a_b.x + y * a_b.y;
    }

    //calling this way float dp = dotproduct ( a, b )
    public static float DotProduct( Vec2 a, Vec2 b)
    {
        return a.x * b.x + a.y * b.y;
    }

    //normalize a vec2 : reduce length of vec2 to unit length
    public float normalize()
    {
        float mag = Magnitude();
        float fInvMag = (mag != 0f) ? 1f / mag : 1.00e-12f;
        x *= fInvMag;
        y *= fInvMag;
        return mag;
    }

    public Vec2 Perp()
    {
        return new Vec2(-y, x);
    }

    public void Rotate ( float angle)
    {
        float fx = x;
        x = fx * Mathf.Cos(angle) - y * Mathf.Sin(angle);
        y = fx * Mathf.Sin(angle) + y * Mathf.Cos(angle);
    }
}
