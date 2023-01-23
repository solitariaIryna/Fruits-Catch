using UnityEngine;

public class FruitMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Fruits _fruitsType;
    [SerializeField] private LocationFruits _fruitsLocation;

    private float _speed = -45f;
    private bool _isMove = false;
    public Fruits FruitsType { get => _fruitsType;}
    public LocationFruits FruitsLocation { get => _fruitsLocation; set => _fruitsLocation = value; }

  
    private void FixedUpdate()
    {
        if (_isMove)
            Move();
    }

    public void StartMoveFruit()
    {
        _isMove = true;
    }

    public void TakeHand()
    {
        _fruitsLocation = LocationFruits.inBasket;
        _isMove = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void PutBasket()
    {
        _rigidbody.useGravity = true;
        transform.parent = null;
    }

    public void StopMoveFruits()
    {
        _fruitsLocation = LocationFruits.inPool;
        _isMove = false;
        _rigidbody.velocity = Vector3.zero;
    }
    private void Move()
    {
        _rigidbody.velocity = new Vector3(_speed * Time.deltaTime, 0, 0);
    }

}
