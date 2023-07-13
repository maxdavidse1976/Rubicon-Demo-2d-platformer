using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth;
    public int currentHealth;
    public float invincibilityPeriod;
    private float invincibleTimer;
    public SpriteRenderer spriteRenderer;
    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }

    public void DealDamage(int damage)
    {
        if (invincibleTimer <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Instantiate(deathEffect, transform.position, transform.rotation);
                LevelController.instance.RespawnPlayer();
            }
            else
            {
                AudioManager.instance.PlaySFX(9);
                invincibleTimer = invincibilityPeriod;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, .5f);
                PlayerController.instance.KnockBack();
            }
            UIController.instance.UpdateHealthUI();
        }
    }
    public void HealPlayer(int healthAdded)
    {
        currentHealth+=healthAdded;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UIController.instance.UpdateHealthUI();
    }
}
