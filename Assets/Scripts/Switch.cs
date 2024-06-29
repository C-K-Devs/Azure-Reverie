using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject movingPlatform;
    private bool isActivated = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return))
        {
            isActivated = !isActivated;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            movingPlatform.GetComponent<MovingPlatform>().SetActivated(isActivated);
        }
    }
}
