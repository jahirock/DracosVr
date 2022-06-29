using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public DamageType type = DamageType.enemy;
    private Animator animator;
    [SerializeField]
    private float healthPoints = 100;
    [SerializeField]
    private float maxHealthPoints = 100F;
    [SerializeField]
    private float healthIncreaseTime = 1F;
    [SerializeField]
    private float healthIncreasePoints = 1F;

    private float healthCooldown = 0;

    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("health", HealthPoints);
        }
    }

    private void Update()
    {
        healthCooldown += Time.deltaTime;

        if (HealthPoints > 0 && HealthPoints < MaxHealthPoints && healthCooldown >= healthIncreaseTime)
        {
            HealthPoints += healthIncreasePoints;
            healthCooldown = 0;
        }
    }

    public float HealthPoints 
    {
        get {
            return healthPoints;
        }
        set {
            healthPoints = value;
            if(animator != null)
            {
                animator.SetFloat("health", healthPoints);
            }
            if(healthPoints <= 0)
            {
                //TODO: Gestionar la muerte del personaje/enemigo
                GetComponent<Rigidbody>().useGravity = true;
                if(type == DamageType.enemy)
                {
                    Destroy(gameObject, 5F);
                }
            }
        }
    }

    public float MaxHealthPoints
    {
        get 
        {
            return maxHealthPoints;
        }
        set
        {
            maxHealthPoints = value;
            HealthPoints = value;
        }
    }
}
