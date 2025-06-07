using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    public static Controller access;
     public GameObject bulletPrefab; // Prefab do projétil
    public float bulletSpeed = 10f; // Velocidade do projétil
    public Transform shootPoint; 
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
       // Controller.access.movement;
        // Instancia o projétil na posição de origem (shootPoint)
       // GameObject bullet = Instantiate(bulletPrefab, movement.x, movement.y);

        // Roda o projétil para a direção do sprite do jogador
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //if (rb != null)
        //{
            // Aplique a velocidade do projétil na direção da rotação do jogador
          //  rb.velocity = movement * bulletSpeed; // O "right" pega a direção em que o sprite está apontando
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
