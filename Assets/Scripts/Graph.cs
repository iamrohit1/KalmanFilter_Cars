using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    public GameObject car;
    public GameObject mark;
    public float range = 30;
    float d;
    // Start is called before the first frame update
    void Start()
    {
        d = Mathf.Floor(car.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        
        while (d < car.transform.position.x + range)
        {
            GameObject cube = Instantiate(mark);
            cube.transform.position = new Vector3(d, 0, 0);
            cube.GetComponentInChildren<TextMesh>().text = d.ToString();
            cube.transform.parent = transform;
            d += 1;
        }
    }
}
