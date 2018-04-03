using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControl m_PlayerControl;

    void Start()
    {
        m_PlayerControl.Initialize();
    }

    void Update()
    {
        m_PlayerControl.Update(transform);
    }
}
