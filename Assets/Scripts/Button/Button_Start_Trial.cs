using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Start_Trial : MonoBehaviour
{
    public Experiment exp;

    public void OnButtonClicked()    {
        exp.Start_Trial("A");
    }
}
