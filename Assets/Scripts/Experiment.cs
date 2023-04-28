using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Experiment : MonoBehaviour
{
    private static SREYELINKLib.EL_EYE eye;
    private SREYELINKLib.EyeLink el;
    private SREYELINKLib.IELData elData;
    SREYELINKLib.EyeLinkUtil elutil;
    private double lastSampleTime = 0.0;
    // Start is called before the first frame update
    void Start()
    {
        eye = SREYELINKLib.EL_EYE.EL_EYE_NONE;
        double st;
        elutil = new SREYELINKLib.EyeLinkUtil();
        el = new SREYELINKLib.EyeLink();

                                              
        el.open("100.1.1.1", 0);
        print("Connect opened successfully");     
        el.openDataFile("trial_4.edf"); 
        el.sendCommand("file_event_filter = LEFT,RIGHT,FIXATION,SACCADE,BLINK,MESSAGE,BUTTON,INPUT");
        el.sendCommand("file_sample_data = LEFT,RIGHT,GAZE,AREA,GAZERES,STATUS,INPUT");
        el.sendCommand("link_event_filter = LEFT,RIGHT,FIXATION,SACCADE,BLINK,BUTTON,INPUT");
        el.sendCommand("link_sample_data = LEFT,RIGHT,GAZE,GAZERES,AREA,STATUS,INPUT");
        el.sendCommand("screen_pixel_coords=0,0," + 1680 + "," + 1050);
        el.sendMessage("TRIALID 1");
        
        el.sendMessage("!V IAREA RECTANGLE 1 0 0 639 539 topleft");


        el.setOfflineMode();
        elutil.pumpDelay(50);

        el.startRecording(true, true, true, true);
        el.waitForBlockStart(50, true, true);

        eye = (SREYELINKLib.EL_EYE)el.eyeAvailable();
        if (eye == SREYELINKLib.EL_EYE.EL_BINOCULAR)    eye = SREYELINKLib.EL_EYE.EL_LEFT;

    }

    public void End_Recording(string str)  {
        // Stop Recording
        el.stopRecording();
        el.setOfflineMode();
        elutil.pumpDelay(500);
        el.closeDataFile();

        // If el.isConnected <> -1 Then 'Skip file transfer if in dummy mode.
	    el.receiveDataFile("trial_3.edf", "./Results/" + str);
    }

    public void Start_Trial(string str)   {
        el.sendMessage(str);
    }

    public void End_Trial(string str)   {
        el.sendMessage(str);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
