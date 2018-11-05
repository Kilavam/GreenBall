using System;
using UnityEngine;

[Serializable]
public class PlayerControl
{
    [SerializeField]
    private float m_speed = 10;

    private Quaternion m_position = Quaternion.identity;
    private Quaternion m_lookRot = Quaternion.identity;

    public Quaternion Position { get { return m_position; } }
    
    public void Update(Transform transform)
    {
        GetCameraRotation();

        Vector2 moveInput = GetMovementDirection();
        if (moveInput.sqrMagnitude > 0)
        {
            Vector3 forward = m_position * Vector3.forward;
            Vector3 right = m_position * Vector3.right;
            Vector3 rotAxis = -moveInput.x * forward + moveInput.y * right;

            m_position = Quaternion.AngleAxis(m_speed * Time.deltaTime, rotAxis) * m_position;
            
            float angle = Vector2.SignedAngle(Vector2.up, moveInput);
            m_lookRot = Quaternion.AngleAxis(-angle, Vector3.up);
        }

        Vector2 lookAtInput = GetLookAtDirection();
        if (lookAtInput.sqrMagnitude > 0)
        {
            float angle = Vector2.SignedAngle(Vector2.up, lookAtInput);
            m_lookRot = Quaternion.AngleAxis(-angle, Vector3.up);
        }
        
        transform.position = m_position * Vector3.up * Game.PlanetRadius;
        transform.rotation = m_position * m_lookRot;
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

    private void GetCameraRotation()
    {
        float rotation = Input.GetAxis("CameraRotate");

        //m_position = m_position * Quaternion.AngleAxis(rotation * 100 * Time.deltaTime, Vector3.up);
        //m_lookRot = m_lookRot * Quaternion.AngleAxis(-rotation * 100 * Time.deltaTime, Vector3.up);

        Quaternion q = Quaternion.AngleAxis(rotation * 100 * Time.deltaTime, Vector3.up);
        m_position = m_position * q;
        m_lookRot = m_lookRot * Quaternion.Inverse(q);
    }
}

