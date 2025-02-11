using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
public class HUDManager : MonoBehaviour
{
    
    private UIDocument _uiDocument;
    private static Label _scoreLabel;
    private static Label _remainingLabel;
    private static Label _timerLabel;
    
    [SerializeField] private float timer = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        _scoreLabel = _uiDocument.rootVisualElement.Q<Label>("Score");
        _remainingLabel = _uiDocument.rootVisualElement.Q<Label>("Remaining");
        _timerLabel = _uiDocument.rootVisualElement.Q<Label>("Timer");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SetTimer();
    }
    
    public static void SetScore(int score)
    {
        if (_scoreLabel == null)
            return;
        _scoreLabel.text = score.ToString() + " Gold";
    }


    public static void SetRemaining(int remainingCount)
    {
        if (_remainingLabel == null)
            return;
        _remainingLabel.text = remainingCount.ToString("00") + " Remaining";
    }
    
    private void SetTimer()
    {
        if (_timerLabel == null)
            return;
        _timerLabel.text = Mathf.FloorToInt(timer / 60).ToString("00") + ":" + Mathf.FloorToInt((timer % 60 * 100) / 100).ToString("00");
    }
}
