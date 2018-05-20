using UnityEngine;
using System.Collections;
using System;

public class CameraFacingBillboard : MonoBehaviour
{
    public Camera m_Camera;

    void Start()
    {
        //m_Camera = Camera.main;
       // m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        try
        {
            if (m_Camera == null)
                m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                m_Camera.transform.rotation * Vector3.up);
        }
        catch(Exception ex)
        {

        }
    }
}