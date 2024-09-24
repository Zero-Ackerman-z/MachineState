using UnityEngine;

public class CameraMovementXZ : MonoBehaviour
{
    public float movementSpeed = 10f;  // Velocidad de movimiento de la c�mara
    public float maxX = 50f;  // L�mite m�ximo en el eje X
    public float minX = -50f;  // L�mite m�nimo en el eje X
    public float maxZ = 50f;  // L�mite m�ximo en el eje Z
    public float minZ = -50f;  // L�mite m�nimo en el eje Z

    void Update()
    {
        // Obtener inputs de movimiento
        float horizontalInput = Input.GetAxis("Horizontal");  // A/D o Flechas Izquierda/Derecha
        float verticalInput = Input.GetAxis("Vertical");      // W/S o Flechas Arriba/Abajo

        if (horizontalInput != 0 || verticalInput != 0)
        {
            // Movimiento en el plano XZ
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * movementSpeed * Time.deltaTime;

            // Nueva posici�n potencial
            Vector3 newPosition = transform.position + movement;

            // Asegurarse de que la c�mara no salga de los l�mites en el plano XZ
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            newPosition.y = transform.position.y;

            // Aplicar el movimiento a la c�mara
            transform.position = newPosition;

        }

    }
}
