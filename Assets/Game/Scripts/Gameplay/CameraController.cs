using UnityEngine;

public class CameraController : MonoBehaviour {
    
    [Header("Settings")]
    [SerializeField]
    private float m_angle = 45f;
    [SerializeField]
    private float m_distance = 3f;
    [SerializeField]
    [Range(0,1)]
    private float m_positionSpeed = 0.1f;
    [SerializeField]
    [Range(0, 1)]
    private float m_roationSpeed = 0.1f;

    [Header("Target")]
    [SerializeField]
    private Transform m_target = null;
    
    private Vector3 m_offset;
    private Vector3 m_position;
    private Quaternion m_rotation;

    public Vector3 Position
    {
        get { return m_position; }
    }

    void Start () {
        m_offset = Vector3.zero;
        m_position = Vector3.zero;
        m_rotation = Quaternion.identity;

        Reset();
    }
	
	void LateUpdate () {
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
        
        transform.position = Vector3.Lerp(transform.position, m_position, m_positionSpeed);
        
        Vector3 lookAt = m_target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), m_roationSpeed);
    }
    
    public void Reset()
    {
        CalculateOffset();
        CalculateTargetPosition();

        transform.position = m_position;
        transform.rotation = m_rotation;
    }
}
