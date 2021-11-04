using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] Sprite openDoor;
    [SerializeField] AudioClip openDoorSound;
    [SerializeField] LevelManager.Scenes nextLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            /*GameManager.Instance.SetHealthAtStartOfLevel(
                other.GetComponent<Health>().GetHealth());*/,
            player.IdleAnimation();
            player.enabled = false;
            AudioSource.PlayClipAtPoint(
                    openDoorSound,
                    Camera.main.transform.position,
                    PlayerPrefsManager.GetMasterVolume());
            GetComponent<SpriteRenderer>().sprite = openDoor;
            LevelManager.Instance.LoadScene(nextLevel);
        }
    }
}
