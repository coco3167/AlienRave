using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LDTool
{
    public class LDToolManager : MonoBehaviour
    {
        public GameObject ldToolManagerPrefab;
        
        [SerializeField] private float ennemyRepresentationSize = 1;
        [SerializeField] private Chapter chapterPrefab;

        public List<Chapter> chapters { get; private set; } = new();

        public void AddChapter()
        {
            Chapter newChapter = Instantiate(chapterPrefab, transform);
            if (chapters.IsUnityNull())
                chapters = new List<Chapter>();
            chapters.Add(newChapter);
            newChapter.Initialize(ennemyRepresentationSize);
        }

        public void RemoveUnusedChapters()
        {
            for (int loop = 0; loop < chapters.Count; loop++)
            {
                Chapter chapter = chapters[loop];
                if (chapter.IsUnityNull())
                {
                    chapters.RemoveAt(loop);
                    loop--;
                }
            }
        }

        public void InitializeChapters()
        {
            chapters = new List<Chapter>();
            for (int loop = 0; loop < transform.childCount; loop++)
            {
                chapters.Add(transform.GetChild(loop).GetComponent<Chapter>());
                chapters[loop].Initialize(ennemyRepresentationSize);
            }
        }
    }
}
