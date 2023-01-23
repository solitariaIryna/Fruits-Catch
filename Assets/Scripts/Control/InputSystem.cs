using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _basketTransform;
    [SerializeField] private PlayerIK _playerIK;

    RaycastHit[] _hits;

    public Action<FruitMove> OnSelectFruit;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectObject();
            CheckTag();       
        }

    }

    private void CheckTag()
    {
        foreach (RaycastHit hit in _hits)
        {
            if (hit.collider.CompareTag("Fruit"))
            {
                FruitMove fruit = hit.collider.GetComponent<FruitMove>();
                if (fruit.FruitsLocation != LocationFruits.inBasket)
                    OnSelectFruit?.Invoke(fruit);

            }
        }
    }

    private void DetectObject()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        _hits = Physics.RaycastAll(ray);



    }
}
