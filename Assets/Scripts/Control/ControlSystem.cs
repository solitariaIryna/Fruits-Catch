using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSystem : MonoBehaviour
{
    [SerializeField] private UISystem _uiSystem;
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private FruitPool _fruitsPool;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private PlayerIK _playerIK;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private FruitSpawn _fruitsSpawn;
    [SerializeField] private TextAnimation _textAnimation;

    [SerializeField] private GameObject _conveyer;
    
    private void Awake()
    {
        //events
        _playerIK.OnHarvestedFruit += _levelConfig.CalculateHarvestedFruits;
        _levelConfig.OnLevelCompleted += LevelComplete;
        _levelConfig.OnHittingTheBasket += ChangeCurrentPointText;
        _inputSystem.OnSelectFruit += _playerIK.TakeTargetFruit;


        //initialization 
        _playerIK.Initialization();
        _fruitsPool.Initialization();
        _fruitsSpawn.Initialization();
        _textAnimation.Initialization();
        _uiSystem.Initialization();
        _uiSystem.SetPointForLevelCompleted(_levelConfig.GenerateTask());
        _uiSystem.SetTaskText();
        _cameraMovement.Initialization();
        
    }

    private void OnDisable()
    {
        _playerIK.OnHarvestedFruit -= _levelConfig.CalculateHarvestedFruits;
        _levelConfig.OnLevelCompleted -= LevelComplete;
        _levelConfig.OnHittingTheBasket -= ChangeCurrentPointText;
        _inputSystem.OnSelectFruit -= _playerIK.TakeTargetFruit;
    }

    private void ChangeCurrentPointText(int point)
    {
        _uiSystem.SetCurrentPoint(point);
    }

    private void LevelComplete()
    {
        _conveyer.SetActive(false);
        _fruitsSpawn.StopSpawnFruits();
        _fruitsPool.DisableFruits();
        _cameraMovement.MovingCameraToPlayer();
        _playerIK.SetDancingAnimation();
        _uiSystem.CompleteLevel();
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
