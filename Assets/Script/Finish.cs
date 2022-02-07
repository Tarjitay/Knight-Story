using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject finishLevelCanvas;
    [SerializeField] private GameObject messageUI;

    private bool _isActivated = false;

    public void Activate()
    {
        _isActivated = true;
        messageUI.SetActive(false);
    }

    public void FinishLevel()
    {
        if (_isActivated)
        {
            finishLevelCanvas.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            messageUI.SetActive(true);
        }
    }
}
