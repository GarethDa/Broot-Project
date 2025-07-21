using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 5;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.linearVelocityX = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
