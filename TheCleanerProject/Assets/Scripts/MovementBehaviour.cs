using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    private float Velocity;
    private Vector3 Direction;
    private int RotationSpeed;
    private Rigidbody RB3D;
    private Rigidbody2D RB2D;

    private void Awake()
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody rigidbody3d))
        {
            RB3D = rigidbody3d;
        }
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2d))
        {
            RB2D = rigidbody2d;
        }
    }

    public void Init(float Vel, Vector3 Dir, int RotateSpeed)
    {
        Velocity = Vel;
        Direction = Dir;
        RotationSpeed = RotateSpeed;
    }

    public void Init(float Vel)
    {
        Velocity = Vel;
    }

    public void Init(Vector3 Dir)
    {
        Direction = Dir;
    }

    public void Init(int RotatSpeed)
    {
        RotationSpeed = RotatSpeed;
    }
    public void Move()
    {
        transform.position = transform.position + Velocity * Direction * Time.deltaTime;
    }

    public void Move(Vector3 dir)
    {
        transform.position = transform.position + Velocity * dir * Time.deltaTime;
    }

    public void Move(float Vel)
    {
        transform.position = transform.position + Vel * Direction * Time.deltaTime;
    }

    public void Move(float Vel, Vector3 Dir)
    {
        transform.position = transform.position + Vel * Dir * Time.deltaTime;
    }

    public void MoveRB()
    {
        RB2D.MovePosition(transform.position + Velocity * Direction * Time.fixedDeltaTime);
    }
    public void MoveRB(float Vel)
    {
        RB2D.MovePosition(transform.position + Vel * Direction * Time.fixedDeltaTime);
    }
    public void MoveRB(Vector3 dir)
    {
        RB2D.MovePosition(transform.position + Velocity * dir * Time.fixedDeltaTime);
    }

    public void MoveRB(float Vel, Vector3 dir)
    {
        RB2D.MovePosition(transform.position + Vel * dir * Time.fixedDeltaTime);
    }

    public void MoveRB3D()
    {
        RB3D.MovePosition(transform.position + Velocity * Direction * Time.fixedDeltaTime);
    }

    public void MoveRB3D(float vel)
    {
        RB3D.MovePosition(transform.position + vel * Direction * Time.fixedDeltaTime);
    }

    public void MoveRB3D(Vector3 Dir)
    {
        RB3D.MovePosition(transform.position + Velocity * Dir * Time.fixedDeltaTime);
    }

    public void MoveRB3D(float vel, Vector3 Dir)
    {
        RB3D.MovePosition(transform.position + vel * Dir * Time.fixedDeltaTime);
    }

    public void MoveVelocity(Vector3 Dir)
    {
        RB3D.velocity = Dir;
    }

    public void RotateRight()
    {
        transform.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
    }

    public void RotateLeft()
    {
        transform.Rotate(new Vector3(0, -RotationSpeed * Time.deltaTime, 0));
    }

    public void ChangeVelocity(float amount)
    {
        Velocity = amount;
    }

    public void ChangeDirection(Vector3 newDir)
    {
        Direction = newDir;
    }

    public float getVelocity()
    {
        return Velocity;
    }

    public Vector3 getDirection()
    {
        return Direction;
    }
}