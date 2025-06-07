using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;
    
    void Update()
    {
        this.transform.position += this.direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.destroyed != null)
        {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
