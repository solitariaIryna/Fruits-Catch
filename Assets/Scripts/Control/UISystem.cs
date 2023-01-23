using TMPro;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _task;
    [SerializeField] private GameObject _taskPanel;
    [SerializeField] private GameObject _finalPanel;
    [SerializeField] private RectTransform _rectTransformButton;

    private string _pointForCompletedLevel;
    private string _typeFruitsForCompletedLevel;

    private int _totalPoint = 0;
    private float _timer;
    private bool _isShowButton;

    private Vector3 _maxButtonScale = Vector3.one * 3.3f;
    private Vector3 _minButtonScale = Vector3.one * 0.4f;


    public void Initialization()
    {
        _isShowButton = false;
        _timer = 0;
        _rectTransformButton.localScale = _minButtonScale;
        _taskPanel.SetActive(true);
        _finalPanel.SetActive(false);
    }

    private void Update()
    {
        if (_isShowButton)
        {
            _timer += Time.deltaTime;
            if (_timer < 1f)
            {
                IncreaseButton(_timer);
            }
            else
            {
                _timer = 0;
                _isShowButton = false;
            }
        }
    }


    private void IncreaseButton(float timer)
    {
        _rectTransformButton.localScale = Vector3.Lerp(_minButtonScale, _maxButtonScale, timer);
    }


    public void SetTaskText()
    {
        _task.text = "Collect" + " " + _pointForCompletedLevel + " " + _typeFruitsForCompletedLevel + "  " + "(" + _totalPoint + "/" + _pointForCompletedLevel + ")";
    }

    public void SetPointForLevelCompleted(string[] levelTask)
    {
        _pointForCompletedLevel = levelTask[0];
        _typeFruitsForCompletedLevel= levelTask[1];
    }

    public void SetCurrentPoint(int point)
    {
        _totalPoint = point;
        SetTaskText();
    }


    public void CompleteLevel()
    {
        _taskPanel.SetActive(false);
        _finalPanel.SetActive(true);
        _isShowButton = true;
    }



}
