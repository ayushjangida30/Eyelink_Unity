using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Trial : MonoBehaviour
{
    public Experiment exp;

    public void OnButtonClicked()    {
        exp.End_Trial("A_");
    }
}
