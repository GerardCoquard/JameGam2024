using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSpeed : MonoBehaviour
{
    [SerializeField] float fastSpeed;
    [SerializeField] float fastFastSpeed;
    public void ButtonNormalSpeed()
    {
        Time.timeScale = 1f;
    }
    public void ButtonFastSpeed()
    {
        Time.timeScale = fastSpeed;
    }
    public void ButtonFastFastSpeed()
    {
        Time.timeScale = fastFastSpeed;
    }
}
