using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public event UnityAction OnTimeout;

    private float _currentTime;
    private bool _isRunning;

    [SerializeField] 
    private TMP_Text _timerText;


    public void SetTimer(float seconds)
    {
        _currentTime = seconds;
        _isRunning = true;
        _timerText.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!_isRunning) return;

        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0f)
        {
            _currentTime = 0f;
            _isRunning = false;
            OnTimeout?.Invoke(); 
        }

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        var minutes = Mathf.FloorToInt(_currentTime / 60f).ToString().PadLeft(2, '0');
        var seconds = Mathf.FloorToInt(_currentTime % 60f).ToString().PadLeft(2, '0');

        _timerText.text = $"{minutes}:{seconds}";
    }

}
