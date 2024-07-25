using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIntro : MonoBehaviour
{
    public float timeUnskipable;
    public GameObject escToSkip;

    public void ActivateTutorial()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        StartCoroutine(WaitForSkip());
    }

    IEnumerator WaitForSkip()
    {
        yield return new WaitForSeconds(timeUnskipable);
        escToSkip.SetActive(true);
    }

    public void DeactivateTutorial()
    {
        if(!escToSkip.activeInHierarchy) return;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
