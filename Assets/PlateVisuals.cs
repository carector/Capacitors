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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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


        // TODO: add units
        inputs[0].text = capacitor.variables.A.ToString() + " m^2";
        inputs[1].text = capacitor.variables.d.ToString() + " m";
        inputs[2].text = capacitor.variables.Q.ToString() + " C";

        outputs[0].text = capacitor.variables.C.ToString() + " F";
        outputs[1].text = capacitor.variables.deltaV.ToString() + " V";
        outputs[2].text = capacitor.variables.U.ToString() + " J";
        outputs[3].text = capacitor.variables.E.ToString() + " N/C";
    }
}
