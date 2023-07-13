using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    [SerializeField] private float respawnWaitTimer;
    public int gemsCollected;
    public string levelToLoad;
    private void Awake()
    {
        instance = this;   
    }
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        PlayerController.instance.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX(8);

        yield return new WaitForSeconds(respawnWaitTimer - (1f / UIController.instance.fadeSpeed));
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);
        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCoroutine());
    }

    public IEnumerator EndLevelCoroutine()
    {
        PlayerController.instance.stopInput = true;
        CameraController.instance.stopFollow = true;
        UIController.instance.levelCompleteText.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1 / UIController.instance.fadeSpeed) + 0.25f);
        SceneManager.LoadScene(levelToLoad);
    }
}
