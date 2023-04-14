using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float reduceSpeed = 2;
    private float target;
    private Camera cam;
    void Start() => cam = Camera.main;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

    public void UpdateHealthBar(float health, float maxHealth) => target = health / maxHealth;
}
