using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LifeBarManager : MonoBehaviour
{
    [SerializeField] private Sprite[] hpSprites;

    private readonly List<Image> hpImgs = new();
	
    [SerializeField] private float ShakeMagnitude;
    [SerializeField] private float ShakeTimer;
	
    private int value = 0;
	
    private bool firstUpdate = true;

    private void Awake()
    {
        foreach(Transform child in transform) hpImgs.Add(child.GetComponent<Image>());
        UpdateLife(transform.childCount);
    }
/*
    private void Update()
    {
        if (value++ == 300)
        {
            StartCoroutine(ShakeCoroutine(ShakeMagnitude, ShakeTimer));
            value = 0;
        }
    }
*/
    public void UpdateLife(int health)
    {
        // TODO anims ?
        int i = 1;
        foreach(Image img in hpImgs) img.sprite = hpSprites[i++ > health ? 1 : 0];
        if (firstUpdate)
        {
            firstUpdate = false;
            return;
        }
        Shake();
    }

    private void Shake()
    {
        StartCoroutine(ShakeCoroutine(ShakeMagnitude, ShakeTimer));
    }
	
    private IEnumerator ShakeCoroutine(float magnitude, float timer)
    {
        List<Vector3> originalPos = new List<Vector3>();

        int i = 0;

        foreach (Image img in hpImgs)
        {
            originalPos.Add(img.transform.position);
        }
		
        float elapsedTime = 0.0f;
		
        while (elapsedTime < timer)
        {
            i = 0;
            foreach (Image img in hpImgs)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                img.transform.position = new Vector3(originalPos[i].x + x, originalPos[i].y + y, originalPos[i].z);

                elapsedTime += Time.deltaTime;
                i++;
            }
            yield return null;
        }

        i = 0;
        foreach (Image img in hpImgs)
        {
            img.transform.position = originalPos[i++];
        }
    }
}
