using UnityEngine;

public class MeatP2Controller : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gameObject.layer = LayerMask.NameToLayer("MeatP2");

        // Ignore other instances of itself
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("MeatP2"), LayerMask.NameToLayer("MeatP2"));
        // Ignore everything other than Killbox
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("MeatP2"), LayerMask.NameToLayer("Default"));
    }

    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
