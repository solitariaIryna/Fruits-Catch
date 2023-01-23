using UnityEngine;

public class FruitSpawn : MonoBehaviour
{
    [SerializeField] private FruitPool _fruitPool;
    [SerializeField] private Transform _endFruit;

    private float _timerSpawn;
    private bool _isCanSpawnFruit;
    private Vector3 _position;

    public void Initialization()
    {
        _timerSpawn = 0;
        _isCanSpawnFruit = true;
        _position = transform.position;
    }

    private void Update()
    {
        if (_isCanSpawnFruit)
        {
            if (_timerSpawn >= 1.5)
            {
                SpawnFruit();
                _timerSpawn = 0;
            }
            else
                _timerSpawn += Time.deltaTime;
        }
        
    }
    private void SpawnFruit()
    {
        FruitMove fruit = _fruitPool.GetFruit();
        fruit.transform.LookAt(_endFruit);
        fruit.transform.position = _position;
        fruit.StartMoveFruit();
        fruit.FruitsLocation = LocationFruits.onConveyor;
    }

    public void StopSpawnFruits()
    {
        _isCanSpawnFruit = false;
    }



}
