using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2;
    Transform targetPos;
    Vector3 playerPos;
    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        playerPos = new Vector3(targetPos.position.x, targetPos.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, playerPos, followSpeed*Time.deltaTime);
    }
}