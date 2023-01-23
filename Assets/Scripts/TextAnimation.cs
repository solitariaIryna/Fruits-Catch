using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TextMesh _textMesh;
    private float _timer;
    private float _textFadeFactor;

    private Color _color;

    public void Initialization()
    {
        _timer = 0;
        _color = _textMesh.color;
        _textFadeFactor = 1;
    }


    void Update()
    {
        FadingText();
    }

    private void FadingText()
    {
        if (_timer >= 1)
        {
            Destroy(gameObject);
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
            Vector3 pos = transform.position;
            pos.y += 0.01f;
            transform.position = pos;

            if (_textFadeFactor > 0.4)
            {
                _textFadeFactor -= Time.deltaTime;
                _color.a = _textFadeFactor;
                _textMesh.color = _color;
            }

        }
    }
}
