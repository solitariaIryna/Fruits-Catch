using System;
using UnityEngine;

public enum StateAnimation
{
    None,
    TakingFruit,
    PutFruitInBasket,
    DefaultPosition
}

public class PlayerIK : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _spineTransform;
    [SerializeField] private Transform _positionHand;
    [SerializeField] private Transform _positionBasket;

    [SerializeField] private Transform _positionFruitInBasket; 
    [SerializeField] private Transform _positionFruitInHand;

    private FruitMove _targetFruit;
    private StateAnimation _stateAnimation;

    private bool _isSpineAnimation;
    private bool _isMoveToFruit;
    private bool _isTakeFruit;

    private float _xValueSpine;
    private float _yValueSpine;

    //for take fruit
    private Vector3 _positionStartLerp;
    private Vector3 _positionDefaultHand;
    private Vector3 _positionStartLerpFruit;

    //for move player
    private Vector3 _positionStartPlayer;
    private Vector3 _positionEndPlayer;

    private float _timerLerpHand;
    private float _maxDistanceToFruit = 0.75f;
    
    public Action<Fruits> OnHarvestedFruit;
    public void Initialization()
    {
        
        SetIdleAnimation();
        _isSpineAnimation = false;
        _stateAnimation = StateAnimation.None;
        _isTakeFruit = false;
        _isMoveToFruit = false;
        _positionDefaultHand = _positionHand.transform.localPosition;
    }


    private void SetIdleAnimation()
    {
        _animator.SetBool("Idle", true);
        _animator.SetBool("Dancing", false);
    }


    private void FixedUpdate()
    {
        if (_isSpineAnimation)
        {
            _timerLerpHand += Time.deltaTime * 2f;
            AnimationAction(_timerLerpHand);
        }   
    }

    private void OnAnimatorIK()
    {
        if(_isSpineAnimation)
        {
            Vector3 rotationSpine = new Vector3(_xValueSpine, _yValueSpine, 0);
            _animator.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(rotationSpine));

        }
        
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _positionBasket.position);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, _positionHand.position);
    }

    private void AnimationAction(float timer)
    {
        switch (_stateAnimation)
        {
            case StateAnimation.TakingFruit:
                {
                    TakeFruitFromConveyor(timer);
                    break;
                }
            case StateAnimation.PutFruitInBasket:
                {
                    PutFruitInBasket(timer);
                    break;
                }
            case StateAnimation.DefaultPosition:
                {
                    ReturnToDefaultPosition(timer);
                    break;
                }
        }


    }
    private void TakeFruitFromConveyor(float timer)
    {
        if (timer < 1f)
        {
            LerpPlayerToFruit(timer);
        }
        else
        {
            _timerLerpHand = 0f;
            _stateAnimation = StateAnimation.PutFruitInBasket;
            _positionStartLerp = _positionHand.position;
            _targetFruit.transform.parent = _positionFruitInHand.transform;
            _targetFruit.transform.position = _positionFruitInHand.transform.position;
            _targetFruit.TakeHand();
            _isTakeFruit = false;
        }
    }

    private void PutFruitInBasket(float timer)
    {
        if (timer < 1f)
        {
            LerpPlayerToBasket(timer);
        }
        else
        {
            _timerLerpHand = 0f;
            _stateAnimation = StateAnimation.DefaultPosition;
            _positionStartLerp = _positionHand.localPosition;
            _targetFruit.PutBasket();
            OnHarvestedFruit?.Invoke(_targetFruit.FruitsType);
            _targetFruit = null;

        }
    }

    private void ReturnToDefaultPosition(float timer)
    {
        if (timer < 1f)
        {
            LerpPlayerToDefaultPosition(timer);
        }
        else
        {
            _timerLerpHand = 0f;
            _stateAnimation = StateAnimation.None;
            _isSpineAnimation = false;



        }
    }
    

    public void SetDancingAnimation()
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("Dancing", true);
    }

    public void TakeTargetFruit(FruitMove fruitMove)
    {
        if(_isSpineAnimation != true)
        {
            _isSpineAnimation = true;
            _positionStartLerp = _positionHand.position;
            _stateAnimation = StateAnimation.TakingFruit;
        }
        _targetFruit = fruitMove;
        CheckDistance();
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, _targetFruit.transform.position);
        if (distance > _maxDistanceToFruit)
        {
            float direction = _targetFruit.transform.position.x - transform.position.x;
            _positionStartPlayer = transform.position;
            _isMoveToFruit = true;
            MovePlayerPositionX(direction);

        }
        else
            _isMoveToFruit = false;
    }

    private void MovePlayerPositionX(float direction)
    {
        _positionEndPlayer = new Vector3(transform.position.x + direction, transform.position.y, transform.position.z);
    }


    private void LerpPlayerToFruit(float timer)
    {
        _xValueSpine = Mathf.Lerp(0, 15, timer);
        if (_isMoveToFruit )
        {
            transform.position = Vector3.Lerp(_positionStartPlayer, _positionEndPlayer, timer);
        }
        if (timer < 0.95f)
        {
            _positionHand.position = Vector3.Lerp(_positionStartLerp, _targetFruit.transform.position, timer);
        }
        else
        {
            _isTakeFruit = true;
            _positionStartLerpFruit = _targetFruit.transform.position;
            
        }
        if (_isTakeFruit)
        {
            float timerLerpFruit = (timer - 0.95f) * 20f;
            _targetFruit.transform.position = Vector3.Lerp(_positionStartLerpFruit, _positionFruitInHand.position, timerLerpFruit);
        }
        

    }
    private void LerpPlayerToBasket(float timer)
    {
        _positionHand.position = Vector3.Lerp(_positionStartLerp, _positionFruitInBasket.position, timer);
        _yValueSpine = Mathf.Lerp(0, -90, timer);
        _xValueSpine = Mathf.Lerp(15, 15, timer);
    }

    private void LerpPlayerToDefaultPosition(float timer)
    {
        Vector3 positionHand = Vector3.Lerp(_positionStartLerp, _positionDefaultHand, timer);
        _positionHand.localPosition = positionHand;
        _yValueSpine = Mathf.Lerp(-90, 0, timer);
        _xValueSpine = Mathf.Lerp(15, 0, timer);
    }
}
