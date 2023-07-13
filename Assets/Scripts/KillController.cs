using UnityEngine;

public class KillController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LevelController.instance.RespawnPlayer();
        }
    }
}
