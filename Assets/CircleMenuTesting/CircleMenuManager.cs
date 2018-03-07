using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleMenuManager : MonoBehaviour
{
    [Range(1,9)]
    public int NumberOfDivisions = 9;
    public float circleRadaius = 100f;
    public Button[] buttons;

    int lastDivisionIndex;

    private void Start()
    {
        lastDivisionIndex = 0;

        buttons[lastDivisionIndex].Select();
        buttons[lastDivisionIndex].OnSelect(null);

        ActivateButtonsInCircle();
    }

    // Update is called once per frame
    void Update () {

        //ActivateButtonsInCircle(); //Uncomment for Debug Radius

        Vector3 input1 = InputManagerTest.Singleton.JStick_JDpad_Keyboard(1);

        //Debug.Log("1: " + input1);

        if (input1 != Vector3.zero && input1.magnitude > 0.5)
            lastDivisionIndex =  DivisionIndexBasedOnInput(NumberOfDivisions, input1);

        Debug.Log("Index: " + lastDivisionIndex);

        buttons[lastDivisionIndex].Select();
        buttons[lastDivisionIndex].OnSelect(null);
    }

    int DivisionIndexBasedOnInput (int circleDivision,Vector3 input)
    {
        float angle;

        if (input == Vector3.zero)
            angle = 0;  // As Vector3.zero woul return 90 degrees
        else
            angle = Vector3.SignedAngle(Vector3.forward, input, Vector3.up); //Signed angle only returns (-)0->180

        if (angle < 0)
            angle += 360;

        //Debug.Log("Angle: " + angle);

        float divisionAngle = 360 / circleDivision;
        int divisionIndex = 0; 

        float firstDivisionAngle = 0;
        float lastDivisionAngle = 360;

        float firstDivisionAngleZeroIndex = 360f - (divisionAngle / 2);
        angle = angle % firstDivisionAngleZeroIndex;

        for (int i = 0; i < circleDivision; i++)
        {
            firstDivisionAngle = (divisionAngle * i) - (divisionAngle / 2);
            lastDivisionAngle = (divisionAngle * i) + (divisionAngle / 2); // Never passes 360

            if (angle >= firstDivisionAngle && angle < lastDivisionAngle)
            {
                divisionIndex = i;
                break;
            }
        }

        //Debug.Log("F: " + firstDivisionAngle + " L: " + lastDivisionAngle);
        //Debug.Log("Div Index: " + divisionIndex);

        return divisionIndex;
    }

    void ActivateButtonsInCircle()
    {
        Vector2 pivotPoint = this.gameObject.GetComponent<RectTransform>().pivot;

        for (int i = 0; i < NumberOfDivisions; i++)
        {
            buttons[i].gameObject.SetActive(true);

            float theta = (2 * Mathf.PI / NumberOfDivisions) * i;
            float xpos = Mathf.Sin(theta);
            float ypos = Mathf.Cos(theta);
            buttons[i].transform.localPosition = new Vector3(xpos, ypos, 0f) * circleRadaius;
        }
    }
}
