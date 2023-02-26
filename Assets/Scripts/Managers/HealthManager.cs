using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;

   public void TakeDamage(float dmg) // when we take damage 
    {
        switch (health - dmg)
        {
            case <= 0:
                health = 0;
                Death();
                Debug.Log("Object Dead");
                break;
            default:
                health -= dmg;
                Debug.Log("Taking damage");
                break;
        }
    }

    public void Death() => Destroy(gameObject);
}
