using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] 
    private LevelManager.Scenes nextLevel;
    [SerializeField] 
    private Sprite openDoor;
    [SerializeField] 
    private AudioClip openDoorSFX;
    [SerializeField]
    private bool endOfGame = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if (endOfGame) GameManager.Instance.EndOfGame();
            GameManager.Instance.SetHealthAtStartOfLevel(
                other.GetComponent<Health>().CurrentHealth);
            player.IdleAnimation();
            player.enabled = false;
            AudioSource.PlayClipAtPoint(
                    openDoorSFX,
                    Camera.main.transform.position,
                    PlayerPrefsManager.GetMasterVolume());
            GetComponent<SpriteRenderer>().sprite = openDoor;
            LevelManager.Instance.LoadScene(nextLevel);
        }
    }
}
