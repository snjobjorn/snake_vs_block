using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Camera cam;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0.00f, 12.00f, -9.80f), Time.deltaTime * speed);
        transform.position = new Vector3(0.00f, 12.00f, transform.position.z);
        //Debug.Log(transform.position);
    }
}
