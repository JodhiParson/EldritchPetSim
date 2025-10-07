using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using URandom = UnityEngine.Random;
public class GachaItemScroll : MonoBehaviour
{
    public List<ItemData> slots = new List<ItemData>();
    public Image displayImage;
    public float scrollSpeed = 0.05f; // time between images
    public int spinCount = 30;

    // Callback version
    public void StartScroll(ItemData finalItem, Action onComplete = null)
    {
        StartCoroutine(ScrollRoutine(finalItem, onComplete));
    }

    private IEnumerator ScrollRoutine(ItemData finalItem, Action onComplete)
    {
        float currentDelay = scrollSpeed;

        for (int i = 0; i < spinCount; i++)
        {
            int randomIndex = URandom.Range(0, slots.Count);
            displayImage.sprite = slots[randomIndex].icon;
            yield return new WaitForSeconds(currentDelay);
            currentDelay += 0.01f; // optional slow down
        }

        // Show the final item
        displayImage.sprite = finalItem.icon;
        Debug.Log("You got: " + finalItem.itemName);

        // Invoke the callback if provided
        onComplete?.Invoke();
    }
}
