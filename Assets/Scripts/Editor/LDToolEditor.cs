using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace LDTool
{
    [CustomEditor(typeof(LDToolManager))]
    public class LDToolEditor : Editor
    {
        private LDToolManager ldToolManager;
        private Chapter currentChapter;
        private int chapterEditingIndex = -1;
        private int enemyEditingIndex = -1;
        private GUIStyle titleStyle = new();

        private void OnEnable()
        {
            ldToolManager = (LDToolManager)target;

            titleStyle.normal.textColor = Color.white;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.fontSize = 20;
            titleStyle.alignment = TextAnchor.UpperCenter;
            
            EditorUtility.SetDirty(target);
            PrefabUtility.RecordPrefabInstancePropertyModifications(target);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            ldToolManager.RemoveUnusedChapters();

            int newEditingIndex = DisplayChapters();

            
            if(!IsElementBeingEdited(ref chapterEditingIndex, newEditingIndex))
                return;
            newEditingIndex = DisplayEnemies();

            if (!IsElementBeingEdited(ref enemyEditingIndex, newEditingIndex))
                return;
            DisplayEnemy();
        }

        private int DisplayChapters()
        {
            GUILayout.Space(25);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(25);
            
            ldToolManager.RemoveUnusedChapters();

            GUILayout.BeginVertical("Box");
            int newEditingChapter = DisplayObjectList(ldToolManager.chapters, chapterEditingIndex, "Chapters");
            GUILayout.EndVertical();
            
            return newEditingChapter;
        }

        private int DisplayEnemies()
        {
            currentChapter = ldToolManager.chapters[chapterEditingIndex];
            
            GUILayout.Space(50);
            EditorGUILayout.Separator();
            
            GUILayout.BeginVertical("Box");
            
            // Ennemies Display
            int newEditingEnnemy = DisplayObjectList(currentChapter.ennemies, enemyEditingIndex, "Enemies");
            
            // A bit of space
            GUILayout.Space(5);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(5);
            
            // Chapter Name
            UpdateName(ref currentChapter, "Chapter Name");
            GUILayout.EndVertical();

            return newEditingEnnemy;
        }

        private void DisplayEnemy()
        {
            EnnemyRepresentation currentEnemy = currentChapter.ennemies[enemyEditingIndex];
            
            GUILayout.Space(50);
            EditorGUILayout.Separator();
            
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Enemy", titleStyle);
            UpdateName(ref currentEnemy, "Ennemy Name");
            GUILayout.EndVertical();
        }

        
        private int DisplayObjectList<T>(List<T> list, int currentEditingIndex, string title) where T : MonoBehaviour
        {
            int newEditingIndex = -1;
            
            GUILayout.Label(title, titleStyle);
            for (int loop = 0; loop < list.Count; loop++)
            {
                T listElem = list[loop];
                
                if(listElem.IsUnityNull())
                    continue;
                
                GUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(listElem, typeof(T), true);

                if (GUILayout.Button("Edit"))
                {
                    newEditingIndex = loop;
                }

                if (GUILayout.Button("-"))
                {
                    list.Remove(listElem);
                    DestroyImmediate(listElem.gameObject);
                    
                    if (currentEditingIndex == loop)
                        newEditingIndex = -1;
                    else if (currentEditingIndex > loop)
                        newEditingIndex = currentEditingIndex-1;
                    loop--;
                    PrefabUtility.ApplyPrefabInstance(ldToolManager.ldToolManagerPrefab, InteractionMode.AutomatedAction);
                }
                GUILayout.EndHorizontal();
                
                GUILayout.Space(2);
            }
            
            GUILayout.Space(5);
            
            if (GUILayout.Button("+"))
            {
                if(list.GetType() == typeof(List<EnnemyRepresentation>))
                    currentChapter.AddEnnemy();
                else
                    ldToolManager.AddChapter();
                PrefabUtility.ApplyPrefabInstance(ldToolManager.ldToolManagerPrefab, InteractionMode.AutomatedAction);
            }

            return newEditingIndex;
        }

        private bool IsElementBeingEdited(ref int currentEditingIndex, int newEditingIndex)
        {
            if (newEditingIndex == currentEditingIndex)
            {
                currentEditingIndex = -1;
                return false; // Clicked on the one being edited
            }

            if (newEditingIndex != -1)
                currentEditingIndex = newEditingIndex;
            
            return true;
        }

        private void UpdateName<T>(ref T obj, string title) where T : MonoBehaviour
        {
            string oldName = obj.name;
            obj.name = EditorGUILayout.TextField(title, oldName);
            if(oldName != obj.name)
                PrefabUtility.ApplyPrefabInstance(ldToolManager.ldToolManagerPrefab, InteractionMode.AutomatedAction);
        }
    }
}