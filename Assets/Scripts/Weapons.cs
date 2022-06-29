using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    public const float WEAPON_COOLDOWN_TIME = 0.5F;
    public const float MAGIC_COOLDOWN_TIME = 2F;

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

    Vector3 lastPositionRight, lastPositionLeft;
    Vector3 lastPositionMagicLaunchPoint = Vector3.zero;

    Mana mana;

    public float manaCost = 10;

    [SerializeField]
    float forceMultiplyLaunch = 20F;
    void Start()
    {
        mana = GetComponent<Mana>();

        rightWeaponAlt.SetActive(false);
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

        //Si se puede usar la magia
        //if(magicCooldown > MAGIC_COOLDOWN_TIME)
        if(mana.ManaPoints >= manaCost)
        {
            //Si el trigger derecho está presionado
            if(rTrigger > 0.1F)
            {
                //Si no esta iniciado el lanzamiento de magia, se inicia
                if(lastPositionMagicLaunchPoint == Vector3.zero)
                {
                    lastPositionMagicLaunchPoint = magicLaunchPoint.transform.position;
                }
                else
                {
                    Vector3 force = forceMultiplyLaunch * (magicLaunchPoint.transform.position - lastPositionMagicLaunchPoint);// / Time.deltaTime;
                    Debug.DrawRay(lastPositionMagicLaunchPoint, force);
                }
            }
            //Si se suelta el trigger y está iniciado el lanzamiento de magia
            else if(lastPositionMagicLaunchPoint != Vector3.zero)
            {
                if (currentMagic != null)
                {
                    magicCooldown = 0;
                    //Se obtiene la fuerza con la que se va a lanzar el proyectil
                    Vector3 force = forceMultiplyLaunch * (magicLaunchPoint.transform.position - lastPositionMagicLaunchPoint);// / Time.deltaTime;

                    GameObject fire = ObjectPool.SharedInstance.GetPooledObject();
                    fire.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    fire.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                    fire.GetComponent<AudioSource>().PlayOneShot(fireShootClip);
                    fire.SetActive(true);

                    mana.ManaPoints -= manaCost;

                    //Se desacopla la magia del parent para que se mueva libremente.
                    //currentMagic.transform.parent = null;
                    //Le quita el bloqueo de x y z
                    //currentMagic.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    //currentMagic.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                    //currentMagic.GetComponent<AudioSource>().PlayOneShot(fireShootClip);

                    //Invoke("LoadMagic", MAGIC_COOLDOWN_TIME);

                    //Se reinicia el lanzamiento de magia
                    lastPositionMagicLaunchPoint = Vector3.zero;
                }
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
