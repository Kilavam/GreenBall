using System;
using UnityEngine;

[Serializable]
public class PlayerCamera
{
    [SerializeField]
    private Camera m_camera = null;

    [Header("Settings")]
    [SerializeField]
    private float m_angle = 45f;
    [SerializeField]
    private float m_distance = 3f;
    [SerializeField]
    [Range(0, 1)]
    private float m_positionSpeed = 0.1f;
    [SerializeField]
    [Range(0, 1)]
    private float m_roationSpeed = 0.1f;
    
    private Transform m_target = null;

    private Vector3 m_offset;
    private Vector3 m_position;
    private Quaternion m_rotation;

    public Vector3 Position
    {
        get { return m_position; }
    }

    public Transform Target
    {
        get { return m_target; }
        set { m_target = value; Reset(); }
    }

    public void Initialize(Transform target = null)
    {
        m_target = target;
        m_offset = Vector3.zero;
        m_position = Vector3.zero;
        m_rotation = Quaternion.identity;

        Reset();
    }

    public void Update()
    {
        CalculateOffset();
        CalculateTargetPosition();
        LerpToTarget();
    }

    private void CalculateOffset()
    {
        m_offset = -Vector3.forward * m_distance;
        m_offset = Quaternion.AngleAxis(-m_angle, Vector3.left) * m_offset;
    }

    private void CalculateTargetPosition()
    {
        if (m_target == null) return;

        Vector3 worldOffset = m_target.TransformVector(m_offset);
        m_position = m_target.position + worldOffset;
        m_rotation = Quaternion.LookRotation(-worldOffset);
    }

    private void LerpToTarget()
    {
        if (m_target == null) return;

        m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_position, m_positionSpeed);

        Vector3 lookAt = m_target.position - m_camera.transform.position;
        m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, Quaternion.LookRotation(lookAt, m_target.transform.up), m_roationSpeed);
    }

    public void Reset()
    {
        if (m_target == null) return;

        CalculateOffset();
        CalculateTargetPosition();

        m_camera.transform.position = m_position;
        m_camera.transform.rotation = m_rotation;
    }
}
