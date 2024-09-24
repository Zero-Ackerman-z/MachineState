using UnityEngine;

public class CameraMovementXZ : MonoBehaviour
{
    public float movementSpeed = 10f;  // Velocidad de movimiento de la cámara
    public float maxX = 50f;  // Límite máximo en el eje X
    public float minX = -50f;  // Límite mínimo en el eje X
    public float maxZ = 50f;  // Límite máximo en el eje Z
    public float minZ = -50f;  // Límite mínimo en el eje Z

    void Update()
    {
        // Obtener inputs de movimiento
        float horizontalInput = Input.GetAxis("Horizontal");  // A/D o Flechas Izquierda/Derecha
        float verticalInput = Input.GetAxis("Vertical");      // W/S o Flechas Arriba/Abajo

        if (horizontalInput != 0 || verticalInput != 0)
        {
            // Movimiento en el plano XZ
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * movementSpeed * Time.deltaTime;

            // Nueva posición potencial
            Vector3 newPosition = transform.position + movement;

            // Asegurarse de que la cámara no salga de los límites en el plano XZ
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            newPosition.y = transform.position.y;

            // Aplicar el movimiento a la cámara
            transform.position = newPosition;

        }

    }
}
