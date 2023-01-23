using System;
using UnityEngine;

public class FruitDetect : MonoBehaviour
{

    public Action<FruitMove> OnPutPoolFruit;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            FruitMove fruit = other.GetComponent<FruitMove>();
            if (fruit.FruitsLocation == LocationFruits.onConveyor)
            {
                fruit.StopMoveFruits();
                OnPutPoolFruit?.Invoke(fruit);

            }
        }
    }
}

