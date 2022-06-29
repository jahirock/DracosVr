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
                //El tipo de daño es diferente a quien se lo hacemos
                //Player o enemigo
                //Que los enemigos no se hagan daño entre ellos y el player no se haga daño a si mismo
                float currentDamage = damage;
                if(other.GetComponent<Weapons>() != null)
                {
                    //El otro collider lleva armas y escudos
                    if(other.GetComponent<Weapons>().shieldActive)
                    {
                        //Si tiene el escudo activo se reduce el daño
                        currentDamage /= 5;
                    }
                }
                other.GetComponent<Health>().HealthPoints -= currentDamage;
            }
        }
    }
}
