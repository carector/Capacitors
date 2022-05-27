using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacitorCalculations
{
    // Physics 122: Capacitance mini-project
    // Caleb Rector

    /// Formulas used:
    /// Q = CV
    /// C = (A*e-nought)/d
    /// U = Q^2 / 2C
    /// U = 1/2(e-nought*E^2)
    /// surface charge density = Q/A

    [System.Serializable]
    public class CapacitorVariables // All the necessary variables for our calculations
    {
        public float A;
        public float d;
        public float Q;
        public float C;
        public float deltaV;
        public float o;
        public float E;
        public float U;
    }

    public CapacitorVariables variables = new CapacitorVariables();

    const float eNought = 0.00000000000885f;

    // Inputs: Q, A, D
    // Outputs: C, U, E, V

    public void CalculateVariables()
    {
        // Order matters: Some functions depend on
        // other variables determined by inputs
        variables.C = CalculateC(variables.A, variables.d);
        variables.deltaV = CalculateDeltaV(variables.Q, variables.C);
        variables.U = CalculateU(variables.Q, variables.C);
        variables.E = CalculateE(variables.U);
    }

    public float CalculateC(float A, float d) // temp name
    {
        return (A * eNought) / d;
    }

    public float CalculateDeltaV(float q, float c) // temp name
    {
        return q / c;
    }

    public float CalculateU(float q, float c) // temp name
    {
        return q * q / (2 * c);
    }

    public float CalculateE(float U) // temp name
    {
        return Mathf.Sqrt(2 * U);
    }
}
