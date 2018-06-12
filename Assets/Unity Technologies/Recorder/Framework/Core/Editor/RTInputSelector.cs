﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Unity_Technologies.Recorder.Framework.Core.Editor
{
    public class RTInputSelector
    {
        Unity_Technologies.Recorder.Framework.Core.Engine.RecorderSettings recSettings;

        struct InputGroup
        {
            public string title;
            public string[] captions;
            public Type[] types;
        }

        SortedDictionary<int, InputGroup> m_Groups;

        public RTInputSelector( Unity_Technologies.Recorder.Framework.Core.Engine.RecorderSettings recSettings  )
        {
            this.m_Groups = new SortedDictionary<int, InputGroup>();
            this.recSettings = recSettings;

            this.AddGroups( recSettings.GetInputGroups() );
        }

        void AddGroups(List<Unity_Technologies.Recorder.Framework.Core.Engine.InputGroupFilter> groups)
        {
            for(int i = 0; i < groups.Count; i++)
            {
                this.m_Groups.Add(
                        this.m_Groups.Count,
                    new InputGroup()
                    {
                        title = groups[i].title,
                        captions = groups[i].typesFilter.Select(x => x.title).ToArray(),
                        types = groups[i].typesFilter.Select(x => x.type).ToArray(),
                    });
            }
        }

        public bool OnInputGui( int groupIndex, ref Unity_Technologies.Recorder.Framework.Core.Engine.RecorderInputSetting input)
        {
            if (!this.m_Groups.ContainsKey(groupIndex))
                return false;
            if (this.m_Groups[groupIndex].types.Length < 2)
                return false;

            int index = 0;
            for (int i = 0; i < this.m_Groups[groupIndex].types.Length; i++)
            {
                if (this.m_Groups[groupIndex].types[i] == input.GetType())
                {
                    index = i;
                    break;
                }
            }
            var newIndex = EditorGUILayout.Popup("Collection method", index, this.m_Groups[groupIndex].captions);

            if (index != newIndex)
            {
                input = this.recSettings.NewInputSettingsObj(this.m_Groups[groupIndex].types[newIndex], this.m_Groups[groupIndex].title );
                return true;
            }

            return false;
        }
    }

}