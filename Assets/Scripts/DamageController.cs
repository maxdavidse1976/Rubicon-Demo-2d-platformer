using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int damageDealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerHealthController.instance.DealDamage(damageDealth);
        }
        
    }
}
