using System;
using UnityEngine;

[Serializable]
public class PlayerControl
{
    [SerializeField]
    private float m_speed = 10;

    //private Vector3 m_lookAt;
    //private Vector3 m_foward;
    //private Vector3 m_right;
    //private Vector3 m_up;

    public void Initialize(Transform transform)
    {
        if (transform.position == Vector3.zero)
            transform.position = Vector3.up;

        transform.up = transform.position.normalized;
        transform.position = transform.up * Game.PlanetRadius;
        
        //m_foward = transform.forward;
        //m_right = transform.right;
        //m_up = transform.up;

        //m_lookAt = m_foward;
    }

    public void Update(Transform transform)
    {
        Vector2 moveInput = GetMovementDirection();
        if(moveInput.sqrMagnitude > 0)
        {
            Vector3 direction = (moveInput.x * transform.right + moveInput.y * transform.forward).normalized;
            transform.position += direction * m_speed * Time.deltaTime;
        }
        
        //Vector2 lookAtInput = GetLookAtDirection();
        //if(lookAtInput.sqrMagnitude > 0)
        //{
        //    Vector3 lookAt = (lookAtInput.x * Vector3.right + lookAtInput.y * Vector3.forward).normalized;
        //    transform.Rotate(Vector3.up, Vector3.SignedAngle(transform.forward, lookAt, Vector3.up));
        //}
    }

    private Vector2 GetMovementDirection()
    {
        float forward = Input.GetAxis("MoveForward");
        float right = Input.GetAxis("MoveRight");
        return new Vector2(right, forward);
    }

    private Vector2 GetLookAtDirection()
    {
        float forward = Input.GetAxis("LookForward");
        float right = Input.GetAxis("LookRight");
        return new Vector2(right, forward);
    }
}

