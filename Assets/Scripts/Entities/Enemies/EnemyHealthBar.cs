using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Image fillImage;

    public Enemy enemy;

    public Color fullColor = Color.green;
    public Color emptyColor = Color.red;

    public float initalHealth = 0;

    void Start()
    {
        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }
        initalHealth = enemy.health;
    }

    void Update()
    {
        if (enemy != null)
        {
            float healthPercent = Mathf.Clamp01(enemy.health / initalHealth);
            fillImage.fillAmount = healthPercent;
            fillImage.color = Color.Lerp(emptyColor, fullColor, healthPercent);
        }
    }
}
