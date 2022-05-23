using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]


public class LinePlotter : MonoBehaviour
{
    private List<Vec3> points;




    public float x1 = 0f;
    public float y1 = 0f;
    public float m = 1f;
    public float c = 0f;

    private float calcualteY(float y1, float x, float x1, float m, float c)
    {
        return m * (x - x1) + c + y1;
    }

    private void PlotPoints()
    {
        if (points == null)
        {
            points = new List<Vec3>();

        }
        points.Clear();
        float x = -10f;

        for (float xPos = x; xPos < 10f; xPos += 0.2f)
        {
          // points.Add(new Vec3(xPos, calcualteY(y1, xPos, x1, m, c) ) );

        }
    }

    void Start()
    {
        points = new List<Vec3>();
        PlotPoints();

    }

    private void OnDrawGizmos()
    {
        if (points != null)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < points.Count - 1; ++i)
            {
                Gizmos.DrawLine(points[i].ToVector3(), points[i + 1].ToVector3());
            }
        }
    }

    void OnValidate()
    {
        PlotPoints();
        Debug.Log("Re-Populating gizmo's positions");
    }

}