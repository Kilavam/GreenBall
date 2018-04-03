using System;
using UnityEngine;

[Serializable]
public class PlayerControl
{
    [SerializeField]
    private float m_speed = 10;
    [SerializeField]
    private float m_rotationSpeed = 10;

    internal void Initialize()
    {
        
    }

    internal void Update(Transform transform)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = vertical * transform.forward; //new Vector3(horizontal, 0, vertical);
        transform.position += direction * m_speed * Time.deltaTime;
        transform.Rotate(Vector3.up, horizontal * m_rotationSpeed * Time.deltaTime);
    }
}

