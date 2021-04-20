using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Matrix
{
    ///Number of rows in matrix
    private int _rows;
    public int rows {get { return _rows; }}

    ///Number of columns in matrix
    private int _columns;
    public int columns { get { return _columns; } }

    ///Data
    private List<List<float>> elements;

    //Constructor
    public Matrix(int rows, int columns)
    {
        //set dimentions at construction
        this._rows = rows;
        this._columns = columns;

        elements = new List<List<float>>(rows);
        for (int i = 0; i < rows; i++)
        {
            //Initialize matrix with 0s
            elements.Add(new List<float>(new float[columns]));
        }
    }

    //Indexing the Matrix
    public float this[int r, int c]
    {
        get
        {
            return elements[r][c];
        }
        set
        {
            elements[r][c] = value;
        }
    }

    //Log matrix 
    public string Print()
    {
        string out_s = "\n";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                out_s += this[i, j] + "\t";
            }
            out_s += "\n";
        }

        return out_s;
    }

    /// <summary>
    /// Returns square identity matrix of specified size
    /// </summary>
    /// <param name="size"> size of identity matrix </param>
    /// <returns></returns>
    public static Matrix Identity(int size)
    {
        Matrix I = new Matrix(size, size);
        for (int i = 0; i < size; i++)
            I[i, i] = 1;
        return I;
    }

    //Matrix Additions
    public static Matrix operator +(float v, Matrix a) { return a + v;}
    public static Matrix operator +(Matrix a, float v) {
        Matrix r = new Matrix(a.rows, a.columns);
        for (int i = 0; i < r.rows; i++)
            for (int j = 0; j < r.columns; j++)
                r[i, j] = a[i, j] + v;
        return r;
    }
    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.rows != b.rows || a.columns != b.columns)
            throw new Exception("Cannot add matrices of different dimentions");

        Matrix r = new Matrix(a.rows, a.columns);
        for (int i = 0; i < r.rows; i++)
        {
            for (int j = 0; j < r.columns; j++)
            {
                r[i, j] = a[i, j] + b[i, j];
            }
        }

        return r;
    }

    //Matrix Substractions
    public static Matrix operator -(Matrix a, float v) { return a + (-v);}
    public static Matrix operator -(float v, Matrix a) { return v + (-1f * a);}
    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.rows != b.rows || a.columns != b.columns)
            throw new Exception("Cannot add matrices of different dimentions");

        Matrix r = new Matrix(a.rows, a.columns);
        for (int i = 0; i < r.rows; i++)
        {
            for (int j = 0; j < r.columns; j++)
            {
                r[i, j] = a[i, j] - b[i, j];
            }
        }

        return r;
    }

    //Matrix multipications
    public static Matrix operator *(float v, Matrix a) { return a * v; }
    public static Matrix operator *(Matrix a, float v)
    {
        Matrix r = new Matrix(a.rows, a.columns);
        for (int i = 0; i < r.rows; i++)
        {
            for (int j = 0; j < r.columns; j++)
            {
                r[i, j] = a[i, j] * v;
            }
        }

        return r;
    }
    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.columns != b.rows)
            throw new Exception("Matrix cannot me multiplied! Cols(A) != Rows(B)");

        Matrix result = new Matrix(a.rows, b.columns);
        for (int i = 0; i < a.rows; i++)
        {

            for (int j = 0; j < b.columns; j++)
            {
                float r = 0;
                for (int k = 0; k < a.columns; k++)
                {
                    r += a[i, k] * b[k, j];
                }
                result[i, j] = r;
            }
        }

        return result;
    }

    //Matrix transpose
    public Matrix Transpose()
    {
        Matrix ret = new Matrix(columns, rows);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                ret[j, i] = this[i, j];
            }
        }

        return ret;
    }

    //if Matrix of size 1x1 returns the element
    public float Value() {
        if (rows == 1 && columns == 1)
            return this[0, 0];
        throw new Exception("Cannot get value of a matrix whose dimentions are not 1x1!");
    }

    //calculate inverse of matrix
    public Matrix Inverse() {
        Matrix ret = new Matrix(rows, columns);
        if (rows == 1 && columns == 1) {
            ret[0, 0] = 1 / this[0, 0];
            return ret;
        }
        // implementation needed for more than 1x1 matrix!
        throw new NotImplementedException();
    }
}
