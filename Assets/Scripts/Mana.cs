using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField]
    private float manaPoints = 100F;
    [SerializeField]
    private float maxManaPoints = 100F;
    [SerializeField]
    private float manaIncreaseTime = 1F;
    [SerializeField]
    private float manaIncreasePoints = 1F;

    private float manaCooldown = 0;

    private void Update()
    {
        manaCooldown += Time.deltaTime;
        
        if(ManaPoints < MaxManaPoints && manaCooldown >= manaIncreaseTime)
        {
            ManaPoints += manaIncreasePoints;
            manaCooldown = 0;
        }
    }

    public float ManaPoints 
    {
        get {
            return manaPoints;
        }
        set {
            manaPoints = value;
            if(manaPoints > MaxManaPoints)
            {
                manaPoints = MaxManaPoints;
            }
        }
    }

    public float MaxManaPoints
    {
        get
        {
            return maxManaPoints;
        }
        set
        {
            MaxManaPoints = value;
            manaPoints = value;
        }
    }
}
