using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlateVisuals : MonoBehaviour
{
    CapacitorCalculations capacitor;

    Slider areaSlider;
    Slider chargeSlider;
    Slider distanceSlider;
    TextMeshProUGUI[] inputs;
    TextMeshProUGUI[] outputs;
    Transform[] plates;
    Transform[] EFLinePositions;
    LineRenderer[] lines;

    // Start is called before the first frame update
    void Start()
    {
        capacitor = new CapacitorCalculations();

        inputs = new TextMeshProUGUI[3];
        outputs = new TextMeshProUGUI[4];
        plates = new Transform[2];

        plates[0] = GameObject.Find("LowerPlate").transform;
        plates[1] = GameObject.Find("UpperPlate").transform;

        // Get UI element references
        areaSlider = GameObject.Find("AreaSlider").GetComponent<Slider>();
        inputs[0] = GameObject.Find("AreaSliderText").GetComponent<TextMeshProUGUI>();
        distanceSlider = GameObject.Find("DistanceSlider").GetComponent<Slider>();
        inputs[1] = GameObject.Find("DistanceSliderText").GetComponent<TextMeshProUGUI>();
        chargeSlider = GameObject.Find("ChargeSlider").GetComponent<Slider>();
        inputs[2] = GameObject.Find("ChargeSliderText").GetComponent<TextMeshProUGUI>();
        

        for (int i = 0; i < 4; i++)
        {
            outputs[i] = GameObject.Find("OutputText" + i).transform.GetComponentInChildren<TextMeshProUGUI>();
        }

        EFLinePositions = new Transform[8];
        for (int i = 1; i <= 4; i++)
        {
            EFLinePositions[i-1] = GameObject.Find("LinePosTop" + i).transform;
            EFLinePositions[i+3] = GameObject.Find("LinePosBottom" + i).transform;
        }

        lines = FindObjectsOfType<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate parallel plate capacitor model
        transform.Rotate(Vector3.up, 0.25f);

        // Update capacitor inputs based on slider values
        capacitor.variables.A = areaSlider.value;
        capacitor.variables.d = distanceSlider.value;
        capacitor.variables.Q = chargeSlider.value;

        capacitor.CalculateVariables();

        // Update plate scale + distance
        for (int i = -1; i < 1; i++)
        {
            plates[i+1].localScale = new Vector3(capacitor.variables.A, 0.25f, capacitor.variables.A);
            plates[i+1].localPosition = new Vector3(0, Mathf.Sign(i) * (0.125f + capacitor.variables.d / 2));
        }


        // Add units and round values
        inputs[0].text = FormatNumber(capacitor.variables.A) + " m^2";
        inputs[1].text = FormatNumber(capacitor.variables.d) + " m";
        inputs[2].text = FormatNumber(capacitor.variables.Q) + " C";

        outputs[0].text = FormatNumber(capacitor.variables.C) + " F";
        outputs[1].text = FormatNumber(capacitor.variables.deltaV) + " V";
        outputs[2].text = FormatNumber(capacitor.variables.U) + " J";
        outputs[3].text = FormatNumber(capacitor.variables.E) + " V/m";

        // Update electric field lines
        UpdateLineRenderers();
    }
    
    // Round number to 2 decimal places and include scientific notation
    public string FormatNumber(float input)
    {
        string result;

        // For very large or very small numbers we need to
        // separate the exponent to round off our input appropriately
        if (Mathf.Abs(input) >= 1000 || (Mathf.Abs(input) <= 0.01f && input != 0))
        {
            float exponent = (float)(System.Math.Floor(System.Math.Log10(System.Math.Abs(input))));
            float mantissa = (float)(input / System.Math.Pow(10, exponent));

            result = $"{Mathf.Round(mantissa*100f)/100} E{exponent}";
        }
        else
            result = (Mathf.Round(input*100) / 100f).ToString();

        return result;
    }

    // Updates electric field lines
    public void UpdateLineRenderers()
    {
        for (int i = 0; i < 4; i++)
        {
            lines[i].SetPositions(new Vector3[2]{ EFLinePositions[i].position, EFLinePositions[i + 4].position});
        }
    }
}
