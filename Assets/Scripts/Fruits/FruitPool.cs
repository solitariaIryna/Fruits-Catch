using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitPool : MonoBehaviour
{
    [SerializeField] private FruitDetect _fruitDetect;
    [SerializeField] private FruitMove[] _fruitsArray;
    [SerializeField] private List<FruitMove> _fruitsUse;
    [SerializeField] private int _countOfFruits;

    private List<FruitMove> _fruitsPool;
    


    public void Initialization()
    {
        _fruitDetect.OnPutPoolFruit += PutFruitToPool;

        _fruitsPool = new List<FruitMove>();
        _fruitsUse = new List<FruitMove>();

        InizializePool();
    }
    private void OnDisable()
    {
        _fruitDetect.OnPutPoolFruit -= PutFruitToPool;
    }

    private void InizializePool()
    {
        for (int i = 0; i < _countOfFruits; i++)
        {
            int fruit = Random.Range(0, _fruitsArray.Length);
            _fruitsPool.Add(Instantiate(_fruitsArray[fruit], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity));
        }
    }
    public FruitMove GetFruit()
    {
        int index = Random.Range(0, _fruitsPool.Count);
        FruitMove fruit = _fruitsPool[index];
        _fruitsPool.RemoveAt(index);
        _fruitsUse.Add(fruit);
        return fruit;
    }

    public void PutFruitToPool(FruitMove fruit)
    {

        _fruitsUse.Remove(fruit);
        _fruitsPool.Add(fruit);
    }

   
    public void DisableFruits()
    {
        for (int i = 0; i < _fruitsUse.Count; i++)
        {
            
            FruitMove fruit = _fruitsUse[i];
            if (fruit.FruitsLocation != LocationFruits.inBasket)
                fruit.gameObject.SetActive(false);
            else
            {
                if (fruit.FruitsLocation == LocationFruits.inBasket && fruit.transform.position.y < -0.364f)
                {
                    fruit.gameObject.SetActive(false);
                }
            }

        }
        for (int i = 0; i < _fruitsPool.Count; i++)
        {
            FruitMove fruit = _fruitsPool[i];
            fruit.gameObject.SetActive(false);
        }
    }
}
