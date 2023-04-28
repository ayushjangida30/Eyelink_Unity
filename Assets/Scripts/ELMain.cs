using System;
using UnityEngine;
using System.IO;

namespace elconnect
{
    class ELMain : MonoBehaviour
    {

        private static string tempString;
        [STAThread]
        public static void Main()
        {
            // EyelinkWindow elW = new EyelinkWindow();
            // elW.Show();

            SREYELINKLib.EL_EYE eye = SREYELINKLib.EL_EYE.EL_EYE_NONE;
            SREYELINKLib.EyeLinkUtil elutil = new SREYELINKLib.EyeLinkUtil();
            SREYELINKLib.EyeLink el = new SREYELINKLib.EyeLink();

            System.IO.StreamWriter writer;

            string path = Application.dataPath + "/EyeLink_Data.txt";

            writer = new System.IO.StreamWriter(path, true);
            // string tempString;


            try
            {
                double st;
                double lastSampleTime = 0.0;


                el.open("100.1.1.1", 0);
                el.openDataFile("abc.edf");
                el.sendCommand("link_sample_data  = LEFT,RIGHT,GAZE");
                el.sendCommand("screen_pixel_coords=0,0," + 1680 + "," + 1050);
                el.sendMessage("abc");



                // SREYELINKLib.ELGDICal cal = elutil.getGDICal();
                // // cal.setCalibrationWindow(elW.Handle.ToInt32());
                // // cal.enableKeyCollection(true);
                // el.doTrackerSetup();
                // // cal.enableKeyCollection(false);

                // // cal.enableKeyCollection(true);
                // elutil.pumpDelay(1500);
                // el.doDriftCorrect((short)(1092 / 2), (short)(1080 / 2), true, true);
                // cal.enableKeyCollection(false);

                // elW.setGazeCursor(true);

                // el.setOfflineMode();
                // elutil.pumpDelay(50);

                el.startRecording(false, false, true, false);

                st = elutil.currentTime();
                while ((st + 20000) > elutil.currentTime())
                {
                    SREYELINKLib.Sample s;
                    s = el.getNewestSample();
                    if (s != null && s.time != lastSampleTime)
                    {
                        if (eye != SREYELINKLib.EL_EYE.EL_EYE_NONE)
                        {
                            if (eye == SREYELINKLib.EL_EYE.EL_BINOCULAR)
                                eye = SREYELINKLib.EL_EYE.EL_LEFT;

                            float x = s.get_gx(eye);
                            float y = s.get_gy(eye);
                            Console.Write(s.time);
                            Console.Write("\t");
                            Console.Write(x);
                            Console.Write("\t");
                            Console.Write(y);
                            Console.WriteLine("");// New line

                            lastSampleTime = s.time;
                            // elW.setGaze((int)x, (int)y);


                            tempString +=  x + "," + y + "\n";
                        }
                        else
                        {
                            eye = (SREYELINKLib.EL_EYE)el.eyeAvailable();
                        }
                        st = elutil.currentTime();
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // finally
            // {
            //     el.stopRecording();
            //     el.close();
            //     el = null;
            //     elutil = null;
            // }

            // writer.WriteLine(tempString);
            // writer.Close();
        }
    }
}
