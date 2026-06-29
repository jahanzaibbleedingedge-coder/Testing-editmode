using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float moveRange = 5f; // Optional: limits movement range
    
    private Vector3 startPosition;
    private Rigidbody rb;
    
    void Start()
    {
        // Store the starting position
        startPosition = transform.position;
        
        // Try to get Rigidbody component (optional, works without it too)
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        // Get horizontal input (A/D or Left/Right arrows)
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Calculate movement
        Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
        
        // Apply movement
        if (rb != null && !rb.isKinematic)
        {
            // Use Rigidbody if available (for physics)
            rb.MovePosition(transform.position + movement);
        }
        else
        {
            // Simple transform movement
            transform.Translate(movement, Space.World);
        }
        
        // Optional: Clamp position within range
        if (moveRange > 0)
        {
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, 
                startPosition.x - moveRange, 
                startPosition.x + moveRange);
            transform.position = clampedPosition;
        }
    }
}