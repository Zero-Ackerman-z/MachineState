using System.Collections;
using UnityEngine;

public class Toy : MonoBehaviour
{
    private bool isColliding = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión detectada con: " + other.name);

        if (other.CompareTag("Player"))
        {
            isColliding = true;
            StartCoroutine(DestroyAfterDelay(2f)); 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false; 
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        float elapsed = 0f;

        while (elapsed < delay)
        {
            if (!isColliding) 
            {
                yield break;
            }
            elapsed += Time.deltaTime;
            yield return null; 
        }

        Destroy(gameObject);
    }
}
