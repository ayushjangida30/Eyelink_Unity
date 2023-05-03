using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder.Input;

namespace UnityEditor.Recorder.FrameCapturer
{
    /// <exclude/>
    public abstract class BaseFCRecorderSettings : RecorderSettings
    {
        [SerializeField] internal UTJImageInputSelector m_ImageInputSelector = new UTJImageInputSelector();

        protected internal override bool ValidityCheck(List<string> errors)
        {
            var ok = base.ValidityCheck(errors);
            return ok;
        }

        public ImageInputSettings imageInputSettings
        {
            get { return m_ImageInputSelector.imageInputSettings; }
            set { m_ImageInputSelector.imageInputSettings = value; }
        }

        public override bool IsPlatformSupported
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsEditor ||
                    Application.platform == RuntimePlatform.WindowsPlayer ||
                    Application.platform == RuntimePlatform.OSXEditor ||
                    Application.platform == RuntimePlatform.OSXPlayer ||
                    Application.platform == RuntimePlatform.LinuxEditor ||
                    Application.platform == RuntimePlatform.LinuxPlayer;
            }
        }

        public override IEnumerable<RecorderInputSettings> InputsSettings
        {
            get { yield return m_ImageInputSelector.Selected; }
        }
    }
}
