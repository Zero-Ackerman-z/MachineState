using System.Collections;
using UnityEngine;

public class Toy : MonoBehaviour
{
    private bool isColliding = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión detectada con: " + other.name);

        if (other.CompareTag("Player")) // Asegúrate de que el tag sea correcto
        {
            isColliding = true;
            StartCoroutine(DestroyAfterDelay(2f)); // Llama a la coroutine para destruir
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false; // Cambia el estado de colisión a falso al salir
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        float elapsed = 0f;

        while (elapsed < delay)
        {
            if (!isColliding) // Si ya no está colisionando, salimos
            {
                yield break;
            }
            elapsed += Time.deltaTime;
            yield return null; // Espera un frame
        }

        // Si sigue colisionando después del tiempo de espera, destruye el objeto
        Destroy(gameObject);
    }
}
