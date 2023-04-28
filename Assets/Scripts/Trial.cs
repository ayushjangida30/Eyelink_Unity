using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static linkevent.GetCurrentEyeMovementType;
using static elconnect.ELMain;

public class Trial : MonoBehaviour
{

    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("#####Start");
        Debug.Log("#####End");

        // Debug.Log("#####Start - ELMain");
        // elconnect.ELMain.Main();
        // Debug.Log("#####End - ELMain");
    }

    // Update is called once per frame
    void Update()
    {
        // cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y + 0.01f, cube.transform.position.z);
        // Debug.Log("#####Start - ELMain");
        // elconnect.ELMain.Main();
        // Debug.Log("#####End - ELMain");
    }
}
