using System.Collections;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float activeTime = 2;

    [Header("Mesh related")]
    public float meshRefreshRate = 0.1f;

    private SkinnedMeshRenderer[] skinnedMeshRenderers;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // NANI ?!
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail (float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderers == null)
            {
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            for (int i = 0; i<skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();

                
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }
    }
}
