using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawn : MonoBehaviour
{
    public GameObject tank;
    public float waveTimer = 10, numInWave = 10;
    public static int score;
    public TextMeshProUGUI text;
    //TowerScript[] towers;
    void Start()
    {
        StartCoroutine("SpawnAngels");
        
    }
    private void Update()
    {
       
    }
    IEnumerator SpawnAngels()
    {
        while (true)
        {
            for (int i = 0; i < numInWave; i++)
            {
                Instantiate(tank, transform.position + (Vector3.up * 0.5f), transform.rotation);
                yield return new WaitForSeconds(0.5f);
                
            }
            //increase next wave by 1
            numInWave++;
            yield return new WaitForSeconds(waveTimer);
        }
      
    }
}