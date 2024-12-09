using UnityEngine;

namespace LDTool
{
    public class EnnemyRepresentation : MonoBehaviour
    {
        private float size;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            
            Gizmos.DrawCube(transform.position + new Vector3(0,size/2,0), new Vector3(size, size, size));
        }
        
        public void SetSize(float newSize)
        {
            size = newSize;
        }
    }
}
