using UnityEngine;
using AK.UnitsStats;

public class HealthBarUpdater : MonoBehaviour
{
    [SerializeField] float heartSize = 9f;
    [SerializeField] RectTransform heartBarFilled = null;
    [SerializeField] RectTransform heartBarBlank = null;

    Stats stats;
    Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        stats = player.GetComponent<Stats>();

        InitilizeHearts();
    }

    private void InitilizeHearts()
    {
        UpdateHealthBar(heartBarBlank);
        UpdateFilledBar();
    }

    private void OnEnable() { stats.OnHealthChange += UpdateFilledBar; }
    private void OnDisable() { stats.OnHealthChange -= UpdateFilledBar; }

    private void UpdateFilledBar() { UpdateHealthBar(heartBarFilled); }

    private void UpdateHealthBar(RectTransform barToUpdate)
    {
        Vector2 barUpdatedSize = barToUpdate.sizeDelta;
        barUpdatedSize.x = stats.GetCurrentHealth * heartSize;
        barToUpdate.sizeDelta = barUpdatedSize;
    }
}
