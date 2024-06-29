using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool movingRight = true;
    private bool isActivated = false;

    void Update()
    {
        if (!isActivated) return;

        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.position, speed * Time.deltaTime);
            if (transform.position == rightPoint.position) movingRight = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint.position, speed * Time.deltaTime);
            if (transform.position == leftPoint.position) movingRight = true;
        }
    }

    public void SetActivated(bool activated)
    {
        isActivated = activated;
    }
}
