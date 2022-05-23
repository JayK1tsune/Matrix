using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OctreeNode rootNode = new OctreeNode(new Vec3(7f, 20f, 8f), new Vec3(20f, -20f, 20f));
        GameObject[] allActiveProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in allActiveProjectiles)
        {
            rootNode.AddObject(projectile);
        }
        rootNode.Draw();
    }
}
