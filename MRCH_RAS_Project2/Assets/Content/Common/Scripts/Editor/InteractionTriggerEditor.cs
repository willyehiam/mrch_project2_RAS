using UnityEditor;

namespace Content.Common.Scripts.Editor
{
    //[CustomEditor(typeof(InteractionTriggerWrapper))]
    public class InteractionTriggerEditor : UnityEditor.Editor
    {
        private SerializedProperty _useColliderTrigger;
        private SerializedProperty _useDistanceTrigger;
        private SerializedProperty _useLookAtTrigger;
        private SerializedProperty _useEventsTriggers;

        private SerializedProperty _useStartTrigger;
        private SerializedProperty _useOnEnableTrigger;
        private SerializedProperty _useUpdateTrigger;
        private SerializedProperty _useOnDisableTrigger;


        private SerializedProperty _distance;
        private SerializedProperty _lookAtAngle;
        private SerializedProperty _lookAtDistance;

        private SerializedProperty _onTriggerFirstEnter;
        private SerializedProperty _onTriggerEnter;
        private SerializedProperty _onTriggerExit;

        private SerializedProperty _onDistanceFirstEnter;
        private SerializedProperty _onDistanceEnter;
        private SerializedProperty _onDistanceExit;

        private SerializedProperty _onLookAtFirstEnter;
        private SerializedProperty _onLookAtEnter;
        private SerializedProperty _onLookAtDistanceExit;

        private SerializedProperty _onStart;
        private SerializedProperty _onEnable;
        private SerializedProperty _onUpdate;
        private SerializedProperty _onDisable;

        private void OnEnable()
        {

            _useColliderTrigger = serializedObject.FindProperty("useColliderTrigger");
            _useDistanceTrigger = serializedObject.FindProperty("useDistanceTrigger");
            _useLookAtTrigger = serializedObject.FindProperty("useLookAtTrigger");
            _useEventsTriggers = serializedObject.FindProperty("useEventsTriggers");


            _useStartTrigger = serializedObject.FindProperty("useStartTrigger");
            _useOnEnableTrigger = serializedObject.FindProperty("useOnEnableTrigger");
            _useUpdateTrigger = serializedObject.FindProperty("useUpdateTrigger");
            _useOnDisableTrigger = serializedObject.FindProperty("useOnDisableTrigger");

            _distance = serializedObject.FindProperty("distance");
            _lookAtAngle = serializedObject.FindProperty("lookAtAngle");
            _lookAtDistance = serializedObject.FindProperty("lookAtDistance");

            _onTriggerFirstEnter = serializedObject.FindProperty("onTriggerFirstEnter");
            _onTriggerEnter = serializedObject.FindProperty("onTriggerEnter");
            _onTriggerExit = serializedObject.FindProperty("onTriggerExit");

            _onDistanceFirstEnter = serializedObject.FindProperty("onDistanceFirstEnter");
            _onDistanceEnter = serializedObject.FindProperty("onDistanceEnter");
            _onDistanceExit = serializedObject.FindProperty("onDistanceExit");

            _onLookAtFirstEnter = serializedObject.FindProperty("onLookAtFirstEnter");
            _onLookAtEnter = serializedObject.FindProperty("onLookAtEnter");
            _onLookAtDistanceExit = serializedObject.FindProperty("onLookAtDistanceExit");

            _onStart = serializedObject.FindProperty("onStart");
            _onEnable = serializedObject.FindProperty("onEnable");
            _onUpdate = serializedObject.FindProperty("onUpdate");
            _onDisable = serializedObject.FindProperty("onDisable");
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector(); //Change to Odin
            /*serializedObject.Update();

            EditorGUILayout.PropertyField(_useColliderTrigger);
            if (_useColliderTrigger.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_onTriggerFirstEnter);
                EditorGUILayout.PropertyField(_onTriggerEnter);
                EditorGUILayout.PropertyField(_onTriggerExit);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_useDistanceTrigger);
            if (_useDistanceTrigger.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_distance);
                EditorGUILayout.PropertyField(_onDistanceFirstEnter);
                EditorGUILayout.PropertyField(_onDistanceEnter);
                EditorGUILayout.PropertyField(_onDistanceExit);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_useLookAtTrigger);
            if (_useLookAtTrigger.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_lookAtAngle);
                EditorGUILayout.PropertyField(_lookAtDistance);
                EditorGUILayout.PropertyField(_onLookAtFirstEnter);
                EditorGUILayout.PropertyField(_onLookAtEnter);
                EditorGUILayout.PropertyField(_onLookAtDistanceExit);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_useEventsTriggers);
            if (_useEventsTriggers.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_useStartTrigger);
                if(_useStartTrigger.boolValue)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(_onStart);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.PropertyField(_useOnEnableTrigger);
                if (_useOnEnableTrigger.boolValue)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(_onEnable);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.PropertyField(_useUpdateTrigger);
                if (_useUpdateTrigger.boolValue)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.LabelField("ARE YOU SURE YOU NEED THIS??");
                    EditorGUILayout.PropertyField(_onUpdate);
                    EditorGUILayout.LabelField("WAIT, ARE YOU REALLY SURE??");
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                }

                EditorGUILayout.PropertyField(_useOnDisableTrigger);
                if(_useOnDisableTrigger.boolValue)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(_onDisable);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }*/
        }
    }
}
