using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QInventory
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EquipmentInventory))]
    public class EquipmentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {

        }
    }
}
