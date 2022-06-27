using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public DamageType type = DamageType.enemy;

    private Animator animator;
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

    [SerializeField]
    private float healthPoints = 100;

    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("health", HealthPoints);
        }
    }

}
