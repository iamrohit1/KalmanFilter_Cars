using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAccelerationInput : MonoBehaviour
{
    public CarRealWorld car;

    float velocity;
    float acceleration;
    float position
    {
        get
        {
            return transform.position.x;
        }
        set
        {
            transform.position = new Vector3(value, 5, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        velocity = 0;
        position = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        OneStep(Time.deltaTime);
    }

    public void OneStep(float deltaTime) {

        acceleration = car.acceleration;

        position += (velocity * deltaTime) + (acceleration * deltaTime * deltaTime);
        velocity += acceleration * deltaTime * deltaTime;

    }


}
