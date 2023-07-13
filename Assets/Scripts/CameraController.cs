using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target;
    public Transform farBackground, middleBackground;
    private Vector2 lastPosition;
    public float minHeight, maxHeight;
    public bool stopFollow;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (!stopFollow)
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
            Vector2 amountToMove = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);

            farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;
            lastPosition = transform.position;
        }
    }
}
