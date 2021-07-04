using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Player player;
    public float speed;
    public float angularSpeed;
    public float speedSmooth;
    public Quaternion direction;

    private Vector2 CurrentVelocityRef;

    void Update()
    {
        //Vector2 TargetSpeed = new Vector2(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y).normalized * speed;
        Vector2 TargetSpeed = new Vector2(math.cos(transform.rotation.eulerAngles.z) * speed, math.sin(transform.rotation.eulerAngles.z) * speed);
        Debug.DrawLine(transform.position, transform.position + new Vector3(TargetSpeed.x, TargetSpeed.y, 1));

        rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, TargetSpeed, ref CurrentVelocityRef, speedSmooth);
        if (Input.GetKey(player.LeftKey))
        {
            rigidbody.angularVelocity = angularSpeed;
        }
        else if (Input.GetKey(player.RightKey))
        {
            rigidbody.angularVelocity = -angularSpeed;
        }
        else rigidbody.angularVelocity = 0;
    }
}
