using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] IntData totalLemmings;
    void Update()
    {
        if (totalLemmings.variable <= totalLemmings.variable * .2f)
        {
            Debug.Log("win");
        }   
    }
}
