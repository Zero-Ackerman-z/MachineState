using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ToySpawner : MonoBehaviour
{
    public GameObject[] toyPrefabs; 
    public SteeringBehavior steeringBehavior; 
    public int numberOfToys; 

    void Start()
    {
        if (steeringBehavior != null)
        {
            SpawnToys();
        }
        else
        {
            Debug.LogWarning("SteeringBehavior no asignado.");
        }
    }
    void SpawnToys()
    {
        for (int i = 0; i < numberOfToys; i++)
        {
            Vector3 center = steeringBehavior.wanderAreaCenter.position;
            float radius = steeringBehavior.wanderAreaRadius;

            Vector3 randomPosition = new Vector3(
                Random.Range(center.x - radius, center.x + radius) , 0 , Random.Range(center.z - radius, center.z + radius) );

            GameObject toyPrefab = toyPrefabs[Random.Range(0, toyPrefabs.Length)];

            Instantiate(toyPrefab, randomPosition, Quaternion.identity);
        }
    }
}


