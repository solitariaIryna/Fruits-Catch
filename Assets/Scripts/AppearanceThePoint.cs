using UnityEngine;
using UnityEngine.UI;

public class AppearanceThePoint : MonoBehaviour
{
    [SerializeField] private TextAnimation _point;
    [SerializeField] private LevelConfig _levelConfig;
    
    private void Awake()
    {
        _levelConfig.OnHittingTheBasket += AddPoint;
    }

    private void OnDisable()
    {
        _levelConfig.OnHittingTheBasket -= AddPoint;
    }
    private void AddPoint(int harvestedFruit)
    {
        TextAnimation point = Instantiate(_point);
        point.transform.position = transform.position;
        point.Initialization();
    }

}
