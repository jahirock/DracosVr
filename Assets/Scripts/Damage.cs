using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    player,
    enemy
}

public class Damage : MonoBehaviour
{
    public DamageType type = DamageType.enemy;
    public float damage = 10F;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != null)
        {
            if(other.GetComponent<Health>().type != type)
            {
                //El tipo de da�o es diferente a quien se lo hacemos
                //Player o enemigo
                //Que los enemigos no se hagan da�o entre ellos y el player no se haga da�o a si mismo
                float currentDamage = damage;
                if(other.GetComponent<Weapons>() != null)
                {
                    //El otro collider lleva armas y escudos
                    if(other.GetComponent<Weapons>().shieldActive)
                    {
                        //Si tiene el escudo activo se reduce el da�o
                        currentDamage /= 5;
                    }
                }
                other.GetComponent<Health>().HealthPoints -= currentDamage;
            }
        }
    }
}
