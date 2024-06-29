using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpBoost = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.jumpForce += jumpBoost;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.jumpForce -= jumpBoost;
        }
    }
}
