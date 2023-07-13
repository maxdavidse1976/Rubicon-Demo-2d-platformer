using UnityEngine;

public class Stompbox : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject collectible;
    [SerializeField]
    [Range(0, 100)] 
    private float chanceToDrop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.transform.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, collision.transform.position, collision.transform.rotation);
            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100f);
            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, collision.transform.position, collision.transform.rotation);
            }

            AudioManager.instance.PlaySFX(3);
        }
    }
}
