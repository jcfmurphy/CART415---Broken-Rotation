using System;
using UnityEngine;

[Serializable]
public class SoundFollowTankManager : SoundTankManager
{

	public override void Setup()
    {
        m_Movement = m_Instance.GetComponent<SoundTankMovement>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        m_Movement.m_PlayerNumber = m_PlayerNumber;

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }
		
}
