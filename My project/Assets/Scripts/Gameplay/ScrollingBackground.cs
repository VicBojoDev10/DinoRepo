using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollingSpeed = 2f;
    
    private float imageWidth;

    private Vector2 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        imageWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * scrollingSpeed * Time.deltaTime;
        float distanceTraveled = startPos.x - transform.position.x;
        if (distanceTraveled >= imageWidth)
        {
            transform.position = new Vector3(startPos.x + (imageWidth - distanceTraveled), transform.position.y, transform.position.z);
        }
    }
}
