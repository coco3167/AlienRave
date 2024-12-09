using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LDTool
{
    [Serializable]
    public class Chapter : MonoBehaviour
    {
        [SerializeField] private EnnemyRepresentation ennemyPrefab;
        
        public List<EnnemyRepresentation> ennemies { get; private set; }
        private float maxDistance;
        public float SelfSize { get; private set; }

        private float ennemyRepresentationSize;

        private void OnDrawGizmosSelected()
        {
            CalculateMaxDistance();
            
            Gizmos.color = Color.yellow;

            float halfDistance = maxDistance / 2;
            float halfEnnemySize = ennemyRepresentationSize / 2;
            SelfSize = maxDistance + halfEnnemySize;
            
            Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, halfDistance + halfEnnemySize/2), new Vector3(5, 0, SelfSize));
        }

        private void OnTransformChildrenChanged()
        {
            // doesnt work
            InitializeEnnemies();
        }

        public void Initialize(float ennemySize)
        {
            ennemyRepresentationSize = ennemySize;
            ennemies = new List<EnnemyRepresentation>();
        }

        public void AddEnnemy()
        {
            EnnemyRepresentation newEnnemy = Instantiate(ennemyPrefab, transform);
            if (ennemies.IsUnityNull())
                ennemies = new List<EnnemyRepresentation>();
            ennemies.Add(newEnnemy);
            newEnnemy.SetSize(ennemyRepresentationSize);
        }

        private void InitializeEnnemies()
        {
            ennemies = new List<EnnemyRepresentation>();
            for (int loop = 0; loop < transform.childCount; loop++)
            {
                ennemies.Add(transform.GetChild(loop).GetComponent<EnnemyRepresentation>());
                ennemies[loop].SetSize(ennemyRepresentationSize);
            }
        }

        private void CalculateMaxDistance()
        {
            maxDistance = 0;
            foreach (EnnemyRepresentation ennemy in ennemies)
            {
                float distance = Mathf.Abs(transform.position.z - ennemy.transform.position.z);
                if (distance > maxDistance)
                    maxDistance = distance;
            }
        }
    }
}
