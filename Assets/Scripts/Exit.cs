using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] Sprite openDoor;
    [SerializeField] AudioClip openDoorSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            FindObjectOfType<GameSession>().SetHealthAtStartOfLevel(
                other.GetComponent<Health>().GetHealth());
            other.GetComponent<Player>().enabled = false;
            AudioSource.PlayClipAtPoint(
                    openDoorSound,
                    Camera.main.transform.position,
                    PlayerPrefsController.GetMasterVolume());
            GetComponent<SpriteRenderer>().sprite = openDoor;
            FindObjectOfType<LevelLoader>().Transition();
        }
    }
}
