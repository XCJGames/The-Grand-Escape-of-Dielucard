using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private Sprite openChest;
    [SerializeField]
    private AudioClip openChestSFX;

    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player) && !isOpen)
        {
            GameManager.Instance.IncreaseTreasures();
            AudioSource.PlayClipAtPoint(
                    openChestSFX,
                    Camera.main.transform.position,
                    PlayerPrefsManager.GetMasterVolume());
            GetComponent<SpriteRenderer>().sprite = openChest;
            isOpen = true;
        }
    }
}
