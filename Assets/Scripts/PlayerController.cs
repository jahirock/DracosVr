using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5F;


    public InputActionReference move = null;
    public Transform cameraTransform;
    Vector3 moveDirection;
    CharacterController controller;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 dir = move.action.ReadValue<Vector2>();
        if(dir != Vector2.zero)
        {
            moveDirection = (cameraTransform.right * dir.x) + (cameraTransform.forward * dir.y);
        }

        controller.Move(moveDirection * speed * Time.deltaTime);
        controller.Move(Physics.gravity/5);
    }

    //Para mover al player con el teclado
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        moveDirection = (cameraTransform.right * dir.x) + (cameraTransform.forward * dir.y);
    }


    //Para disparar fuego cuando se de click con el mouse
    public float cooldown = 2;
    public int fureza = 100;
    public GameObject posicionAnterior;
    public GameObject posicionSiguiente;

    public void Fire(InputAction.CallbackContext context)
    {
        cooldown += Time.deltaTime;
        
        Debug.Log("Fire!");

        //if(cooldown > 0.5F)
        if(cooldown > 0.5F && GetComponent<Mana>().ManaPoints >= GetComponent<Weapons>().manaCost)
        {
            Vector3 force = fureza * (posicionSiguiente.transform.position - posicionAnterior.transform.position);// / Time.deltaTime;
            Debug.DrawRay(posicionSiguiente.transform.position, force, Color.white, 1F);

            GameObject fire = ObjectPool.SharedInstance.GetPooledObject();
            fire.transform.position = posicionSiguiente.transform.position;
            fire.SetActive(true);
            fire.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            fire.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

            GetComponent<Mana>().ManaPoints -= GetComponent<Weapons>().manaCost;

            cooldown = 0;
        }

    }

    public Transform cameraOffset;
    float xRotation = 0F;
    float yRotation = 0F;
    public void Look(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        direction.x *= -1;

        Debug.Log(direction);

        //Obtiene el movimiento del mouse en las dos coordenadas (negativo o positivo dependiendo la direccion).
        float mouseX = direction.x * 0.5F;
        float mouseY = direction.y * 0.5F;

        //Le suma o le resta el movimiento dle mouse a la rotacion de la camara
        xRotation -= mouseY;
        yRotation -= mouseX;
        //Se asegura de que xRotation solo pueda tener valores entre -80 y 70
        xRotation = Mathf.Clamp(xRotation, -80F, 70F);
        //Rota la camara en el eje X. (Osea que va a apuntar hacia arriba o hacia abajo)
        
        //cameraOffset.localRotation = Quaternion.Euler(xRotation, yRotation, 0F);
        //Rota al jugador(y la camara) en el eje Y (Osea mirar a los lados)
        //cameraOffset.Rotate(Vector3.up * mouseX);
    }
}
