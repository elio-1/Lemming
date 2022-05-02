using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingSpawner : MonoBehaviour
{
    [SerializeField] GameObject lemming;
    [SerializeField] float spawnRate = 1f;
    [SerializeField] int lemmingsToSpawn = 10;

    private int counter = 0;
    private float timer= 0;

    private void Awake()
    {
        counter = 0;
    }
    private void Update()
    {
        if (timer>spawnRate && counter !=  lemmingsToSpawn)
        {
            Instantiate(lemming, transform.position, transform.rotation);
            timer = 0;
            counter++;
        }
        timer += Time.deltaTime;
    }
}
    