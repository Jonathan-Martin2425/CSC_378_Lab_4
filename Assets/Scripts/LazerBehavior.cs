using UnityEngine;

public class LazerBehavior : MonoBehaviour
{
    public float killTimer = 1f;

    // Update is called once per frame
    void Update()
    {
        killTimer -= Time.deltaTime;
        if(killTimer <= 0){
            Destroy(gameObject);
        }
    }
}
