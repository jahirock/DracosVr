using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    public GameObject rightHand, leftHand;
    Vector3 lastPositionRight, lastPositionLeft;

    Vector3 lastPositionMagicLaunchPoint;

    public GameObject rightWeapon, leftWeapon, rightWeaponAlt, magicLaunchPoint;
    public GameObject fireMagic;
    public GameObject currentMagic;

    public AudioClip fireShootClip;

    public float weaponCooldown, magicCooldown = 0.0F;
    public const float WEAPON_COOLDOWN_TIME = 0.5F;
    public const float MAGIC_COOLDOWN_TIME = 2F;

    public bool shieldActive = false;

    public InputActionReference rightTrigger = null;
    public InputActionReference leftTrigger = null;
    public InputActionReference leftGrip = null;
    public InputActionReference rightGrip = null;

    [SerializeField]
    private float forceMultiplyLaunch = 20F;
    void Start()
    {
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

        if(rTrigger > 0.1F && magicCooldown > MAGIC_COOLDOWN_TIME)
        {
            //TODO: gatillo derecho, disparar fuego si tenemos la vara activada
        	Debug.Log("RT: " + rTrigger);
            if(currentMagic != null)
            {
                Debug.Log("Last position: ");
                Debug.Log(lastPositionMagicLaunchPoint);

                Debug.Log("current position");
                Debug.Log(magicLaunchPoint.transform.position);
                magicCooldown = 0;
                //Se obtiene la fuerza con la que se va a lanzar el proyectil
                //Vector3 force = 100f * (rightHand.transform.position - lastPositionRight) / Time.deltaTime;
                Vector3 force = forceMultiplyLaunch * (magicLaunchPoint.transform.position - lastPositionMagicLaunchPoint) / Time.deltaTime;
                Debug.Log(force);
                //Se desacopla la magia del parent para que se mueva libremente.
                currentMagic.transform.parent = null;
                //Le quita el bloqueo de x y z
                currentMagic.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                currentMagic.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                currentMagic.GetComponent<AudioSource>().PlayOneShot(fireShootClip);

                Invoke("LoadMagic", MAGIC_COOLDOWN_TIME);
            }
	    }

        if(lTrigger > 0.1F && leftWeapon.activeInHierarchy)
        {
            //TODO: gatillo izquierdo
		    Debug.Log("LT: " + lTrigger);
		    shieldActive = true;
        }
	    else
	    {
		    shieldActive = false;
        }

        if(rGrip > 0.1F && weaponCooldown > WEAPON_COOLDOWN_TIME)
        {
            //TODO: agarre derecho
		    Debug.Log("RG: " + rGrip);
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

        if(lGrip > 0.1F && weaponCooldown > WEAPON_COOLDOWN_TIME)
        {
            //TODO: agarre izquierdo
		    Debug.Log("LG: " + lGrip);
		    weaponCooldown = 0;
		    leftWeapon.SetActive(!leftWeapon.activeInHierarchy);
        }


        lastPositionRight = rightHand.transform.position;
        lastPositionLeft = leftHand.transform.position;
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
