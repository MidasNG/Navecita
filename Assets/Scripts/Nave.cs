using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Nave : MonoBehaviour
{
    public float maxSpeed;
    private float speed, acceleration;
    public float pitchSpeed, rollSpeed, yawSpeed;
    private Vector3 rotations;

    public TextMeshProUGUI speedText;

    //Datos iniciales para el botón de volver a jugar
    public void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(-32.2f, 48, -67.7f);
        speed = 0;
        acceleration = 0;
        speedText.text = "Speed: 0/50";
    }

    private void Update()
    {
        //Aplicando la velocidad
        if (acceleration != 0)
        {
            speed = Mathf.Clamp(speed + acceleration * Time.deltaTime * maxSpeed / 2, 0, maxSpeed);
            speedText.text = "Speed: " + (int)speed + "/50";
        }
        transform.position += transform.forward * speed * Time.deltaTime;

        //Aplicando las rotaciones
        if (rotations.magnitude > 0)
        {
            transform.Rotate(-pitchSpeed * rotations.y * Time.deltaTime, yawSpeed * rotations.z * Time.deltaTime, -rollSpeed * rotations.x * Time.deltaTime);
        }
    }

    public void OnAccelerate(InputValue value)
    {
        acceleration = value.Get<float>();
    }

    public void OnRotate(InputValue value)
    {
        rotations = value.Get<Vector3>();
    }
}
