using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
#if UNITY_EDITOR
#endif

namespace Modules.UIManager.Panels
{
    [CreateAssetMenu(fileName = nameof(UIPanels), menuName = "Configs/UI/"+nameof(UIPanels), order = 0)]
    public class UIPanels : ScriptableObject
    {
        [SerializeField, OnValueChanged(nameof(OnUpdatePanelsInfo)), DrawWithUnity]
        private List<AssetReference> panelReferences;

        [SerializeField, ReadOnly]
        private List<PanelsInfo> panelsInfo;

        public string TryGetPanelGuid<TPanel>() where TPanel : UIPanel
        {
            var panelInfo = panelsInfo.SingleOrDefault(t => t.PanelName == typeof(TPanel).FullName);
            
            if (panelInfo == null)
            {
                Debug.LogError($"No panel reference in config with type \'{typeof(TPanel)}\'");
                return null;
            }
            
            return panelInfo.PanelGuid;;
        }

        [OnInspectorInit]
        private void OnUpdatePanelsInfo()
        {
#if UNITY_EDITOR
            panelsInfo.Clear();
            foreach (var panelReference in panelReferences)
            {
                if(panelReference == null) continue;
                
                var panelInfo = new PanelsInfo
                {
                    PanelGuid = panelReference.AssetGUID,
                    PanelName = panelReference.editorAsset.GameObject().GetComponent<UIPanel<UIContext>>().GetType().FullName
                };
                panelsInfo.Add(panelInfo);
            }
            
            EditorUtility.SetDirty(this);
#endif
        }

        [Serializable]
        public class PanelsInfo
        {
            [field: SerializeField]
            public string PanelGuid;
            
            [field: SerializeField]
            public string PanelName;
        }
    }
}