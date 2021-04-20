using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalmanFilter : MonoBehaviour
{

    public CarRealWorld car;
    
    //state vector mean
    public Matrix x_cap_Current;
    //state vector covariance
    public Matrix P_cap_Current;

    //Jacobian matrix
    public Matrix JacobianMatrix_H;

    // Every step we calculate these two
    //state vector mean
    public Matrix x_cap_New;
    //state vector covariance
    public Matrix P_cap_New;


    //Prediction (Intermidiate results)= checks
    //state vector mean
    public Matrix x_K_check_New;
    //state vector covariance
    public Matrix P_K_check_New;

    public float Q;
    public float R;


    //Function F = previous state estimate processing matrix
    Matrix FunctionMatrix_F(float deltaTime) {

        Matrix FunctionMatrix_F = new Matrix(2,2);
        FunctionMatrix_F[0, 0] = 1;
        FunctionMatrix_F[0, 1] = deltaTime;
        FunctionMatrix_F[1, 0] = 0;
        FunctionMatrix_F[1, 1] = 1;

        return FunctionMatrix_F;
    }

    //Function G = input processing matrix
    Matrix FunctionMatrix_G(float deltaTime) {
        Matrix FunctionMatrix_G = new Matrix(2, 1);
        FunctionMatrix_G[0, 0] = deltaTime * deltaTime;
        FunctionMatrix_G[1, 0] = deltaTime;
        return FunctionMatrix_G;
    }

    //covariance in motion model noise
    Matrix CovarianceMatrix_Q() {
        return Matrix.Identity(2) * Q;
    }

    //covariance in measurement model
    Matrix CovarianceMatrix_R()
    {
        return Matrix.Identity(1) * R;
    }

    //input accelaration from user
    Matrix input_acceleration_current() {
        //input accelaration
        Matrix input_acceleration_current = new Matrix(1, 1);
        input_acceleration_current[0, 0] = car.acceleration;
        return input_acceleration_current;
    }

    //GPS Measurement 
    Matrix y1_GPS() {
        // y1
        Matrix y1 = new Matrix(1, 1);
        y1[0, 0] = car.position;
        return y1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Input data! => Assumptions
        x_cap_Current = new Matrix(2,1);
        
        //start position
        x_cap_Current[0, 0] = 0;

        //start velocity m/s
        x_cap_Current[1, 0] = 0;


        // Input state covariance => P
        P_cap_Current = new Matrix(2, 2);
        P_cap_Current[0, 0] = 0.01f;
        P_cap_Current[0, 1] = 0f;
        P_cap_Current[1, 0] = 0f;
        P_cap_Current[1, 1] = 1f;

        //Jacobian matrix H
        JacobianMatrix_H = new Matrix(1, 2);
        JacobianMatrix_H[0, 0] = 1;
        JacobianMatrix_H[0, 1] = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        OneStep(Time.deltaTime);
        transform.position = new Vector3(x_cap_Current[0,0], -1 , 0);
    }

    void Prediction(float deltaTime) {
        x_K_check_New = (FunctionMatrix_F(deltaTime) * x_cap_Current) + (FunctionMatrix_G(deltaTime) * input_acceleration_current());
        P_K_check_New = (FunctionMatrix_F(deltaTime) * (P_cap_Current * FunctionMatrix_F(deltaTime).Transpose())) + CovarianceMatrix_Q();
    }

    void Correction() {

        // calculate gain
        Matrix K = (P_K_check_New * JacobianMatrix_H.Transpose()) * ((JacobianMatrix_H * (P_K_check_New * JacobianMatrix_H.Transpose())) + CovarianceMatrix_R()).Inverse();
        // fuse prediction and correction
        // (y1_GPS - (JacobianMatrix_H * x_K_check_New)) is the innovation
        x_cap_New = x_K_check_New + K * (y1_GPS() - (JacobianMatrix_H * x_K_check_New));
        P_cap_New = (Matrix.Identity(2) - (K * JacobianMatrix_H)) * P_K_check_New;
    }

    void OneStep(float deltaTime)
    {
        Prediction(deltaTime);
        Correction();
        //update state
        x_cap_Current = x_cap_New;
        P_cap_Current = P_cap_New;

    }

}
