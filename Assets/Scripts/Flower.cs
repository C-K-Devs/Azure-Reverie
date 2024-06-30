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
            transform.localPosition = followOffset;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            transform.SetParent(playerTransform); // Make the flower a child of the player
            transform.localPosition = followOffset; // Reset position to zero
            isFollowing = true;
            GetComponent<Collider2D>().enabled = false; // Disable the collider
            GetComponent<Animator>().enabled = false; // Disable the animator
            GetComponent<SpriteRenderer>().sortingOrder = 1; // Ensure flower is visible above player
        }
    }
}

