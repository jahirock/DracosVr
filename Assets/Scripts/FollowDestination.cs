using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDestination : MonoBehaviour
{
    GameObject[] destinations;

    Transform destination;

    public float speed = 5F;

    public float destPlayerProb = 0.5F;

    GameObject player;

    Animator animator;

    private void Awake()
    {
        destinations = GameObject.FindGameObjectsWithTag("WayPoint");
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        destination = destinations[Random.Range(0, destinations.Length)].transform;
    }

    void Update()
    {
        //Translacion
        float space = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination.position, space);
        //Rotacion
        Vector3 targetDirection = destination.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, space, 0); //0 para que no aumente la velocidad de rotacion
        
        Debug.DrawRay(transform.position, newDirection, Color.red);

        transform.rotation = Quaternion.LookRotation(newDirection);

        animator.SetFloat("distance", targetDirection.magnitude);

        //Cambio de ruta
        if(targetDirection.magnitude < 0.1)
        {
            if(Random.Range(0F, 1F) < destPlayerProb)
            {
                destination = destinations[Random.Range(0, destinations.Length)].transform;
            }
            else
            {
                destination = player.transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collider>() != null && other.CompareTag("Terrain"))
        {
            destination = destinations[Random.Range(0, destinations.Length)].transform;
        }
    }
}
