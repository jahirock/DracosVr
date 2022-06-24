using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    public GameObject rightHand, leftHand;
    Vector3 lastPositionRight, lastPositionLeft;

    public GameObject rightWeapon, leftWeapon, rightWeaponAlt, magicLaunchPoint;
    public GameObject fireMagic;

    public float weaponCooldown, magicCooldown = 0.0F;
    public const float WEAPON_COOLDOWN_TIME = 0.5F;
    public const float MAGIC_COOLDOWN_TIME = 2F;

    public bool shieldActive = false;

    public InputActionReference rightTrigger = null;
    public InputActionReference leftTrigger = null;


    void Start()
    {
        rightWeaponAlt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float rTrigger = rightTrigger.action.ReadValue<float>();
        float lTrigger = leftTrigger.action.ReadValue<float>();
        Debug.Log("R: " + rTrigger);
        Debug.Log("L: " + lTrigger);

        if(rTrigger > 0.1F)
        {
            //TODO: gatillo derecho
        }

        if(lTrigger > 0.1F)
        {
            //TODO: gatillo izquierdo
        }

    }
}
