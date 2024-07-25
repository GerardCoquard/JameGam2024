using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] float speed;
    [SerializeField] GameObject sphere;
    [SerializeField] AnimationCurve fadeCurve;
    bool canrotate = true;
    void Start()
    {
        
    }
    public void StartGame()
    {
        StartCoroutine(AnimateCamera());
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (canrotate)
        {
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y + speed * Time.deltaTime, 0);
        }
        
    }
    IEnumerator AnimateCamera()
    {
        GameObject button1 = transform.GetChild(0).gameObject;
        GameObject button2 = transform.GetChild(1).gameObject;

        button1.GetComponent<Button>().enabled = false;
        button2.GetComponent<Button>().enabled = false;
        Color startColor1 = button1.GetComponent<Image>().color;
        Color finalColor1 = new Color(0, 0, 0, 0);
        Color startVertex = Color.white;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            float percentageDuration = time / 1;
            button1.GetComponent<Image>().color = Color.Lerp(startColor1, finalColor1, fadeCurve.Evaluate(percentageDuration));
            button2.GetComponent<Image>().color = Color.Lerp(startColor1, finalColor1, fadeCurve.Evaluate(percentageDuration));
            button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.Lerp(startVertex, finalColor1, fadeCurve.Evaluate(percentageDuration));
            button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.Lerp(startVertex, finalColor1, fadeCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
        canrotate = false;
        Vector3 startRot = cam.transform.eulerAngles;
        Vector3 endRot = new Vector3(45, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
        Vector3 startPos = cam.transform.GetChild(0).transform.localPosition;
        Vector3 endPos = new Vector3(cam.transform.GetChild(0).transform.localPosition.x, cam.transform.GetChild(0).transform.localPosition.y, -50f);
        while (time <4)
        {
            time += Time.deltaTime;
            float percentageDuration = time / 4;

            cam.transform.eulerAngles = Vector3.Lerp(startRot,endRot, fadeCurve.Evaluate(percentageDuration));
            cam.transform.GetChild(0).transform.localPosition = Vector3.Lerp(startPos, endPos, fadeCurve.Evaluate(percentageDuration));
            yield return new WaitForEndOfFrame();
        }
        cam.transform.GetChild(0).GetComponent<CameraController>().enabled = true;

        FindObjectOfType<TutorialIntro>(true).ActivateTutorial();
    }
}
