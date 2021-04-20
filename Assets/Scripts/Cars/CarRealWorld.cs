using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarRealWorld : MonoBehaviour
{
    public float accelerationNoise;
    public float positionNoise;
    
    public TMP_Text acceleration_text;
    public Slider timeScale;

    private float velocity;

    private float _acceleration;
    public float acceleration {
        get {
            return _acceleration;
        }
        set {
            //Input sensor inaccuracy
            _acceleration = Mathf.Clamp(value + Gaussian.RandomGaussian() * accelerationNoise, 0, Mathf.Infinity);
        }
    }

    private float _position;
    public float position {
        get {
            //GPS inaccuracy
            return transform.position.x + Gaussian.RandomGaussian() * positionNoise;
        }
        set
        {
            _position = value;
            transform.position = new Vector3(_position, 1 ,0);
        }
    }

    private void Start()
    {
        velocity = 0;
        acceleration = 0;
        position = 0;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration_text.text = "Acceleration Reading : "+acceleration;
        acceleration_text.color = acceleration > 1 ? Color.green : Color.red;

        OneStep(Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            acceleration = 50;
        }
        else {
            acceleration = 0;
        }

        Time.timeScale = timeScale.value;
    }

    public void OneStep(float deltaTime) {
        position = _position + (velocity * deltaTime) + (_acceleration * deltaTime * deltaTime);
        velocity = velocity + (_acceleration * deltaTime * deltaTime);
    }

    public float GetGPSPosition() {
        return position;
    }

}


