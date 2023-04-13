using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private float startDelay = 2f;
    private float repeatRate = 2f;

    private PlayerController playerControllerScript; // Declaramos la variable vacía

    private void Start()
    {
        playerControllerScript = FindObjectOfType<PlayerController>(); // Asignamos la referencia
        
        // Llamamos repetidas veces la función SpawnObstacle
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    private void Update()
    {
        if (playerControllerScript.gameOver)
        {
            CancelInvoke("SpawnObstacle");
        }
    }

    private void SpawnObstacle()
    {
        int randomIdx = Random.Range(0, obstaclePrefabs.Length); // Calculamos un elemento aleatorio
        Instantiate(obstaclePrefabs[randomIdx], transform.position,
            obstaclePrefabs[randomIdx].transform.rotation);
    }
}
