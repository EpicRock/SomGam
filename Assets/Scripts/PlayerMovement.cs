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

    private Vector2 CurrentVelocityRef;

    void Update()
    {
        //Creating player velocity from max speed and rotation
        Vector2 TargetVelocity = new Vector2(math.cos(transform.rotation.eulerAngles.z * Mathf.PI / 180) * speed, math.sin(transform.rotation.eulerAngles.z * Mathf.PI / 180) * speed);
        Debug.DrawLine(transform.position, transform.position + new Vector3(TargetVelocity.x, TargetVelocity.y, 1));

        //Slowly increses velocity to its target (feature?)
        rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, TargetVelocity, ref CurrentVelocityRef, speedSmooth);

        //Rotating player
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
