using System.Collections;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{

    public float secondsCount { get; private set; }
    public float rawSeconds { get; private set; }
    public int minutesCount { get; private set; }
    public string minutesText { get; private set; }
    public string secondsText { get; private set; }

    [SerializeField] private TextMeshProUGUI stopWatchText = null;
    public static TimerManager Instance
    {
        get => Instance;
        private set
        {
            if (Instance == null)
                Instance = value;
            else if (Instance != value)
            {
                Debug.Log($"{nameof(TimerManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StopWatch();
    }

    public void StopWatch()
    {
        secondsCount += Time.deltaTime;
        rawSeconds += Time.deltaTime;

        if (minutesCount < 10)
            minutesText = "0" + minutesCount;
        else
            minutesText = minutesCount + "";
        if ((int)secondsCount < 10)
            secondsText = "0" + (int)secondsCount;
        else
            secondsText = (int)secondsCount + "";

        stopWatchText.text = minutesText + ":" + secondsText;

        if (secondsCount >= 60)
        {
            minutesCount++;
            secondsCount = 0;
        }
    }
}
