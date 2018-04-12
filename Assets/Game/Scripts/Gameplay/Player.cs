using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControl m_PlayerControl;
    public PlayerCamera m_PlayerCamera;

    void Start()
    {
        m_PlayerControl.Initialize(transform);
        m_PlayerCamera.Initialize(transform);
    }

    void Update()
    {
        m_PlayerControl.Update(transform);
        m_PlayerCamera.Update();
    }
}
