using UnityEditor;
using UnityEngine;

namespace Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Editor
{
    public class RecorderBaseEditor : UnityEditor.Editor
    {
        public virtual void CommonConfig()
        {
            var so = this.serializedObject;
            EditorGUILayout.PropertyField(so.FindProperty("m_outputDir"), true);
            EditorGUILayout.PropertyField(so.FindProperty("m_encoderConfigs"), true);
        }

        public virtual void ResolutionControl()
        {
            var recorder = this.target as Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase;
            var so = this.serializedObject;

            EditorGUILayout.PropertyField(so.FindProperty("m_resolution"));
            EditorGUI.indentLevel++;
            if (recorder.resolutionUnit == Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase.ResolutionUnit.Percent)
            {
                EditorGUILayout.PropertyField(so.FindProperty("m_resolutionPercent"));
            }
            else if (recorder.resolutionUnit == Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase.ResolutionUnit.Pixels)
            {
                EditorGUILayout.PropertyField(so.FindProperty("m_resolutionWidth"));
            }
            EditorGUI.indentLevel--;
        }

        public virtual void FramerateControl()
        {
            var recorder = this.target as Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase;
            var so = this.serializedObject;

            EditorGUILayout.PropertyField(so.FindProperty("m_framerateMode"));
            if (recorder.framerateMode == Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase.FrameRateMode.Constant)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(so.FindProperty("m_targetFramerate"));
                EditorGUILayout.PropertyField(so.FindProperty("m_fixDeltaTime"));
                if (recorder.fixDeltaTime)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(so.FindProperty("m_waitDeltaTime"));
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }
        }

        public virtual void RecordingControl()
        {
            var recorder = this.target as Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase;
            var so = this.serializedObject;

            // capture control
            EditorGUILayout.PropertyField(so.FindProperty("m_captureControl"));
            EditorGUI.indentLevel++;
            if (recorder.captureControl == Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase.CaptureControl.FrameRange)
            {
                EditorGUILayout.PropertyField(so.FindProperty("m_startFrame"));
                EditorGUILayout.PropertyField(so.FindProperty("m_endFrame"));
            }
            else if (recorder.captureControl == Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase.CaptureControl.TimeRange)
            {
                EditorGUILayout.PropertyField(so.FindProperty("m_startTime"));
                EditorGUILayout.PropertyField(so.FindProperty("m_endTime"));
            }

            if (recorder.captureControl == Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase.CaptureControl.FrameRange ||
                recorder.captureControl == Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase.CaptureControl.TimeRange)
            {
                if (!EditorApplication.isPlaying)
                {
                    EditorGUILayout.Space();
                    if (GUILayout.Button("Play"))
                    {
                        EditorApplication.isPlaying = true;
                    }
                }
                else if (recorder.isRecording)
                {
                    if (GUILayout.Button("Abort"))
                    {
                        recorder.EndRecording();
                    }
                }
            }
            else if (recorder.captureControl == Unity_Technologies.Recorder.Extensions.UTJ.FrameCapturer.Scripts.RecorderBase.CaptureControl.Manual)
            {
                EditorGUILayout.Space();
                if (!recorder.isRecording)
                {
                    if (GUILayout.Button("Start Recording"))
                    {
                        if (!EditorApplication.isPlaying)
                        {
                            so.FindProperty("m_recordOnStart").boolValue = true;
                            EditorApplication.isPlaying = true;
                        }
                        else
                        {
                            recorder.BeginRecording();
                        }
                    }
                }
                else
                {
                    if (GUILayout.Button("End Recording"))
                    {
                        recorder.EndRecording();
                    }
                }
            }
            EditorGUI.indentLevel--;
        }
    }
}
