using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using System.IO;
     

public class Experiment : MonoBehaviour
{
    private static SREYELINKLib.EL_EYE eye;
    private SREYELINKLib.EyeLink el;
    private SREYELINKLib.IELData elData;
    SREYELINKLib.EyeLinkUtil elutil;
    private double lastSampleTime = 0.0;
    private RecorderWindow recorderWindow;
    // private MovieRecorderSettings movieRecorderSettings;
    private bool IsRecording = false;
    private int i = 1;
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
        // el.sendMessage("!V MSG 1156056 VFRAME " + i++ + " 152 144 C:/Eye_Tracker/Assets/Recordings/Rec_1.mp4");
        // el.sendMessage("FRAMERATE 30"); // set the frame rate of the video
        // el.sendMessage("STARTVIDEO " + videoFilename); // start recording video
        // el.sendMessage("VIDEOPARAMS " + videoFilePath); // set the video file path
        el.sendMessage("TRIALID 1");
        
        el.sendMessage("!V IAREA RECTANGLE 1 0 0 639 539 topleft");


        el.setOfflineMode();
        elutil.pumpDelay(50);

        el.startRecording(true, true, true, true);
        el.waitForBlockStart(50, true, true);

        eye = (SREYELINKLib.EL_EYE)el.eyeAvailable();
        if (eye == SREYELINKLib.EL_EYE.EL_BINOCULAR)    eye = SREYELINKLib.EL_EYE.EL_LEFT;

        recorderWindow = GetRecorderWindow();

        if(!recorderWindow.IsRecording())    {
            recorderWindow.StartRecording();
            IsRecording = true;
        }


    }

    public void End_Recording(string str)  {
        // Stop Recording
        el.stopRecording();
        el.setOfflineMode();
        elutil.pumpDelay(500);
        el.closeDataFile();

        // If el.isConnected <> -1 Then 'Skip file transfer if in dummy mode.
	    el.receiveDataFile("trial_4.edf", "./Results/" + "Video_Trial_2.edf");
        if(recorderWindow.IsRecording())    {
            recorderWindow.StopRecording();
            IsRecording = false;
        }
        string filePath = Application.dataPath + "/Recordings/Trial_5.mp4";   
        print(Application.dataPath + "/Recordings/Rec_1.mp4");
        string newFilePath = Application.dataPath + "/Recordings/Rec_28.mp4"; 
        File.Move(filePath, newFilePath);
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
        el.sendMessage("!V VFRAME " + i++ + " 0 89 C:/Eye_Tracker/Assets/Recordings/Rec_28.mp4 resize 1680 1050");
    }


    private RecorderWindow GetRecorderWindow()
        {
            return (RecorderWindow)EditorWindow.GetWindow(typeof(RecorderWindow));
        }
}
