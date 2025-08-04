using UnityEngine;

public class killZoneScript : MonoBehaviour
{
    Rigidbody2D bounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
