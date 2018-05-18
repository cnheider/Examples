using System;
using UnityEditor;
using UnityEngine;

namespace UTJ.FrameCapturer
{
    [CustomEditor(typeof(Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.AudioRecorder))]
    public class AudioRecorderEditor : RecorderBaseEditor
    {
        public override void OnInspectorGUI()
        {
            var so = serializedObject;

            CommonConfig();
            EditorGUILayout.Space();
            FramerateControl();
            EditorGUILayout.Space();
            RecordingControl();

            so.ApplyModifiedProperties();
        }
    }
}
