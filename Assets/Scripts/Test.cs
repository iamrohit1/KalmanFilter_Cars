using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Matrix floatMat = new Matrix(2,2);
        floatMat[0, 0] = 1;
        floatMat[0, 1] = 2;
        floatMat[1, 0] = 3;
        floatMat[1, 1] = 4;


        Matrix I = new Matrix(2, 2);
        I[0, 0] = 1;
        I[0, 1] = 1;
        I[1, 0] = 1;
        I[1, 1] = 1;

        floatMat = floatMat * I;
        floatMat = floatMat * 2;
        floatMat = 2 * floatMat * Matrix.Identity(2);
        floatMat.Print();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
