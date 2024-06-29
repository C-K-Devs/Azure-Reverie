using UnityEngine;

public class Flower : MonoBehaviour
{
    public Vector3 followOffset; // Offset position to follow the player
    private Transform playerTransform;
    private bool isFollowing = false;

    void Update()
    {
        if (isFollowing)
        {
            // Move the flower to a position relative to the player
            transform.position = playerTransform.position + followOffset;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            transform.SetParent(playerTransform); // Make the flower a child of the player
            isFollowing = true;
            GetComponent<Collider2D>().enabled = false; // Disable the collider
            GetComponent<SpriteRenderer>().sortingOrder = 1; // Ensure flower is visible above player
        }
    }
}
