using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Task_Button : MonoBehaviour
{
    public Experiment exp;

    public void OnButtonClicked()    {
        exp.End_Recording("sample_1.edf");
    }
}
