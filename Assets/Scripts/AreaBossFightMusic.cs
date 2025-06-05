using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBossFightMusic : MonoBehaviour
{
    [SerializeField] private int bgmIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (!AudioManager.instance.playBgm || AudioManager.instance.bgmIndex != bgmIndex)
            {
                AudioManager.instance.PlayBGM(bgmIndex);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            AudioManager.instance.StopAllBGM();
        }
    }
}
