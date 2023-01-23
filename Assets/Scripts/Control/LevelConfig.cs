using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelConfig : MonoBehaviour
{
    private Fruits _fruitsType;
    private int _countOfFruits;
    private int _numberOfFruitsType;
    private int _harvestedFruit;
    private string[] _levelTask;

    public Action<int> OnHittingTheBasket;
    public Action OnLevelCompleted;


    public string[] GenerateTask()
    {
        _numberOfFruitsType = Random.Range(1, 4);
        _fruitsType = _numberOfFruitsType switch
        {
            1 => Fruits.Apple,
            2 => Fruits.Banana,
            3 => Fruits.Orange,
            _ => Fruits.Nothing
        } ; 
        _countOfFruits = Random.Range(1, 5);

        _levelTask = new string[2] {_countOfFruits.ToString(), _fruitsType.ToString()};
        
        return _levelTask;

    }


    public void CalculateHarvestedFruits(Fruits fruitsType)
    {
        if (_fruitsType == fruitsType)
        {
            _harvestedFruit++;
            OnHittingTheBasket?.Invoke(_harvestedFruit);
            if (_harvestedFruit == _countOfFruits)
            {
                _harvestedFruit = 0;
                OnLevelCompleted?.Invoke();
            }

        }
        
    }

}
