using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 10.8f;
    [SerializeField] private float scrollDistance = -10.8f;
    private SpriteRenderer topSprite;
    private SpriteRenderer bottomSprite;

    void Start()
    {
        topSprite = transform.Find("TopReference").GetComponent<SpriteRenderer>();
        bottomSprite = transform.Find("BottomReference").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        if (transform.position.y <= scrollDistance)
        {
            transform.position = new Vector2(0f, 0f);
            topSprite.flipX = !topSprite.flipX;
            bottomSprite.flipX = !bottomSprite.flipX;
        }
    }
}
