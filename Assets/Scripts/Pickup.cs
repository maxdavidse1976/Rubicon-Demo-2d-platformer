using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private bool isGem;
    [SerializeField] private bool isHealth;
    [SerializeField] private int healAmount;
    [SerializeField] private GameObject pickupEffect;
    private bool isCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                AudioManager.instance.PlaySFX(6);
                LevelController.instance.gemsCollected++;
                isCollected = true;
                Destroy(gameObject);
                Instantiate(pickupEffect, transform.position, transform.rotation);
                UIController.instance.DisplayAmountOfGems();
            }
            if (isHealth && !(PlayerHealthController.instance.currentHealth == PlayerHealthController.instance.maxHealth))
            {
                AudioManager.instance.PlaySFX(7);
                PlayerHealthController.instance.HealPlayer(healAmount);
                isCollected = true;
                Destroy(gameObject);
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }
        }
    }
}
