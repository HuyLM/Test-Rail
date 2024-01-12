using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnReset;
    [SerializeField] private Slider sdSpeed;
    [SerializeField] private TMP_Dropdown ddMap;

    [SerializeField] private Map[] maps;

    private Map curMap;

    private void Start()
    {
        Application.targetFrameRate = 60;
        btnStart.onClick.AddListener(OnStartButtonClicked);
        btnReset.onClick.AddListener(OnResetButtonClicked);
        ddMap.onValueChanged.AddListener(OnMapDropDownChanged);

        curMap = maps[0];
    }


    private void OnStartButtonClicked()
    {
        curMap.StartRun(sdSpeed.value);
    }

    private void OnResetButtonClicked()
    {
        curMap.ResetRun();
    }

    private void OnMapDropDownChanged(int index)
    {
        curMap.gameObject.SetActive(false);
        curMap = maps[index];
        curMap.gameObject.SetActive(true);
        curMap.ResetRun();
    }

}
