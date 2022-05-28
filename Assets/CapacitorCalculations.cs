using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacitorCalculations
{
    // Physics 122: Capacitance mini-project
    // Caleb Rector

    /// Formulas used:
    ///     Capacitance formula:                    Q = CV
    ///     Parallel-plate capacitor C formula:     C = (A * ε) / d
    ///     Parallel-plate capacitor U formula:     U = 1/2 (ε0 * E^2)
    ///     Surface charge density formula:         σ = Q/A
    ///     Parallel-plate capacitor E formula:     E = σ / ε0


    ///////////////////////////////////////////
    /// 
    /// Inputs + outputs test cases
    ///     Note:   I didn't have time to make the slider values more reasonable, so the numbers here aren't that sensible (but should still be correct)
    /// 
    /// Test 1
    ///     Inputs:                     Q = 1 E-4 C, A = 5 m^2, d = 1 m
    ///     Calculations by hand:       C = 4.427E-11 F, ΔV = 2258820 V, E = 2258820 V/m, U = 22.588 J
    ///     Calculations by program:    C = 4.42E-11 F, ΔV = 2.26E6 V, E = 2.26E6 V/m, U = 22.68 J
    ///     Calculations match
    ///     
    /// Test 2
    ///     Inputs:                     Q = -4.83 E-5 C, A = 6.32 m^2, d = 5 m
    ///     Calculations by hand:       C = 1.12E-11 F, ΔV = -4315704 V, E = -863140 V/m, U = 3.298 J
    ///     Calculations by program:    C = 1.12E-11 F, ΔV = -4.32E6 V, E = -8.63E5 V/m, U = 3.3 J
    ///     Calculations match
    ///     
    /// Test 3
    ///     Inputs:                     Q = -3.05E-5 C, A = 2 m^2, d = 10 m
    ///     Calculations by hand:       C = 1.77E-12 F, ΔV = -17223503 V, E = -1722350.3 V/m, U = 13.13 J
    ///     Calculations by program:    C = 1.77E-12 F, ΔV = -1.72E7 V, E = -1.72E6 V/m, U = 13.15 J
    ///     Calculations match
    ///
    ///////////////////////////////////////////

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
        public float surfaceChargeDensity;
    }

    public CapacitorVariables variables = new CapacitorVariables();

    const float epsilon = 0.00000000000885f;

    // Inputs: Q, A, D
    // Outputs: C, U, E, ΔV

    // Calculate all outputs from our inputs
    public void CalculateVariables()
    {
        // Order matters: Some functions depend on other variables determined by inputs
        variables.C = CalculateC(variables.A, variables.d);
        variables.deltaV = CalculateDeltaV(variables.Q, variables.C);
        variables.surfaceChargeDensity = CalculateSurfaceChargeDensity(variables.Q, variables.A);
        variables.E = CalculateE(variables.surfaceChargeDensity);
        variables.U = CalculateU(variables.E);
    }

    // Calculate capacitance, derived from C = (ε0 * A) / d
    public float CalculateC(float A, float d)
    {
        return (A * epsilon) / d;
    }

    // Calculate potential difference, derived from Q = CΔV
    public float CalculateDeltaV(float q, float c)
    {
        return q / c;
    }

    // Calculate surface charge density
    public float CalculateSurfaceChargeDensity(float q, float A)
    {
        return q / A;
    }

    // Calculate electric field, derived from E = σ / ε0
    public float CalculateE(float surfaceChargeDensity)
    {
        return surfaceChargeDensity / epsilon;
    }

    // Calculate electric potential energy, derived from U = 1/2 * ε0 * E^2
    public float CalculateU(float E)
    {
        return 0.5f * epsilon * E * E;
    }
}
