using UnityEngine;

public class MeatP2TriggerHandler : MonoBehaviour
{
    public Vector2 checkCenter;
    public float checkRadius = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, checkRadius);

        if (overlaps.Length > 0)
        {
            foreach (var overlap in overlaps)
            {
                if (overlap.CompareTag("Player")) 
                {
                    overlap.GetComponent<PlayerHealth>().takeDamage();
                    Debug.Log("Player hit");
                }
            }
        }
    }
}
