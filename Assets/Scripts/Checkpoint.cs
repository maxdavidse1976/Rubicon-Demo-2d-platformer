using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite checkpointOn, checkpointOff;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip checkpointReached;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (spriteRenderer.sprite == checkpointOff)
            {
                audioSource.PlayOneShot(checkpointReached);
            }
            CheckpointController.instance.DeactivateCheckpoints();
            spriteRenderer.sprite = checkpointOn;
            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        spriteRenderer.sprite = checkpointOff;
    }


}
