using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    public const float WEAPON_COOLDOWN_TIME = 0.5F;
    public const float MAGIC_COOLDOWN_TIME = 0.5F;

    public GameObject rightWeapon, leftWeapon, rightWeaponAlt, magicLaunchPoint;
    public GameObject fireMagic;
    public GameObject currentMagic;
    public AudioClip fireShootClip;

    public float weaponCooldown, magicCooldown = 0.0F;

    public bool shieldActive = false;

    //Inputs de VR
    public InputActionReference rightTrigger = null;
    public InputActionReference leftTrigger = null;
    public InputActionReference leftGrip = null;
    public InputActionReference rightGrip = null;

    Vector3 lastPositionMagicLaunchPoint = Vector3.zero;

    Mana mana;

    public float manaCost = 10;

    [SerializeField]
    float forceMultiplyLaunch = 20F;
    void Start()
    {
        mana = GetComponent<Mana>();

        rightWeaponAlt.SetActive(false);

        lastPositionMagicLaunchPoint = magicLaunchPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
	    weaponCooldown += Time.deltaTime;
	    magicCooldown += Time.deltaTime;
        float rTrigger = rightTrigger.action.ReadValue<float>();
        float lTrigger = leftTrigger.action.ReadValue<float>();
        float lGrip = leftGrip.action.ReadValue<float>();
        float rGrip = rightGrip.action.ReadValue<float>();

        if (rTrigger > 0.1F && magicCooldown > MAGIC_COOLDOWN_TIME)
        {
            if (currentMagic != null && mana.ManaPoints >= manaCost)
            {
                magicCooldown = 0;
                //Se obtiene la fuerza con la que se va a lanzar el proyectil
                Vector3 force = forceMultiplyLaunch * (magicLaunchPoint.transform.position - lastPositionMagicLaunchPoint) / Time.deltaTime;

                GameObject fire = ObjectPool.SharedInstance.GetPooledObject();
                fire.SetActive(true);
                fire.transform.position = currentMagic.transform.position;
                fire.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                fire.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                fire.GetComponent<AudioSource>().PlayOneShot(fireShootClip);

                mana.ManaPoints -= manaCost;
            }
        }

        //Si el trigger izquierdo esta presionado y esta activo el escudo
        shieldActive = (lTrigger > 0.1F && leftWeapon.activeInHierarchy);

        //Si se presiona el agarre derecho y se pasa el cooldown de armas, se cambia entre espada y baston
        if (rGrip > 0.1F && weaponCooldown > WEAPON_COOLDOWN_TIME)
        {
		    weaponCooldown = 0;
		    rightWeapon.SetActive(!rightWeapon.activeInHierarchy);
		    rightWeaponAlt.SetActive(!rightWeaponAlt.activeInHierarchy);

		    if(rightWeaponAlt.activeInHierarchy)
		    {
			    LoadMagic();
		    }
		    else
		    {
			    Destroy(currentMagic);
		    }
        }

        //Si se presiona el agarre izquierdo y se pasa el cooldown de armas, se activa o se desactiva el escudo
        if(lGrip > 0.1F && weaponCooldown > WEAPON_COOLDOWN_TIME)
        {
		    weaponCooldown = 0;
		    leftWeapon.SetActive(!leftWeapon.activeInHierarchy);
        }

        lastPositionMagicLaunchPoint = magicLaunchPoint.transform.position;
    }

	void LoadMagic()
	{
		if(currentMagic != null)
		{
			Destroy(currentMagic);
		}
		currentMagic = Instantiate(fireMagic, magicLaunchPoint.transform);
	}
}
