using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EnemyLevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    private EnemyStats stats;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        UpdateLevelDisplay();
    }

    private void UpdateLevelDisplay()
    {
        if (levelText != null && stats != null)
        {
            levelText.text = "Lv. " + stats.GetLevel();
        }
    }
}

