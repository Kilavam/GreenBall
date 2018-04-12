using System;
using UnityEngine;

[Serializable]
public class PlayerControl
{
    [SerializeField]
    private float m_speed = 10;
    [SerializeField]
    private float m_rotationSpeed = 100;

    private Vector3 m_lookAt;
    private Vector3 m_foward;
    private Vector3 m_right;
    private Vector3 m_up;

    public void Initialize(Transform transform)
    {
        if (transform.position == Vector3.zero)
            transform.position = Vector3.up;

        transform.up = transform.position.normalized;
        transform.position = transform.up * Game.PlanetRadius;
        
        m_foward = transform.forward;
        m_right = transform.right;
        m_up = transform.up;

        m_lookAt = m_foward;
    }

    public void Update(Transform transform)
    {
        Vector2 moveInput = GetMovementDirection();
        Vector3 direction = (moveInput.x * m_right + moveInput.y * m_foward).normalized;
        
        transform.position += direction * m_speed * Time.deltaTime;
        // transform.Rotate(Vector3.up, horizontal * m_rotationSpeed * Time.deltaTime);
    }

    private Vector2 GetMovementDirection()
    {
        float forward = Input.GetAxis("MoveFoward");
        float right = Input.GetAxis("MoveRight");
        return new Vector2(right, forward);
    }

    private Vector2 GetLookAtDirection()
    {
        float forward = Input.GetAxis("LookForward");
        float right = Input.GetAxis("LookRgiht");
        return new Vector2(right, forward);
    }
}

