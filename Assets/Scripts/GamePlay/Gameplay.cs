using Model.Inventory.Saves;
using Controller.Inventory;
using System.Collections;
using Model.Inventory;
using View.Inventory;
using UnityEngine;


public class Gameplay : MonoBehaviour
{
    private InventoryGrid _inventory;
    [SerializeField] private InventoryGridView _inventoryView;
    [SerializeField] private DragableSlot[] _dragableSlots;
    [SerializeField] private PopupWindow _popupWindow;

    [SerializeField] private HealthBar _playerBar;
    [SerializeField] private HealthBar _enemyBar;
    [SerializeField] private GameObject _gameOverWindow;

    //gameplay model
    private Player _player;
    private Enemy _enemy;

    [SerializeField] private Gun[] _guns;

    private bool _fighting;

    private void Start()
    {
        _inventory = new InventoryGrid(InventorySaver.Load());

        new InventoryGridController(_inventory, _inventoryView);
        _popupWindow.Init(_inventory);

        int i = 0;
        foreach (var slot in _dragableSlots)
        {
            slot.Init(_inventory, i);
            i++;
        }

        InitGameplay();        
    }

    public void SwitchGun(int gunIndex)
    {
        if (_fighting) return;

        _player.Gun.DeSelect();
        _player.Gun = _guns[gunIndex];
        _player.Gun.Select();  
    }

    public void StartFightStep()
    {
        if (_fighting) return;
        _fighting = true;

        StartCoroutine(FightStepRoutine());
    }

    private void InitGameplay()
    {
        _player = new Player(_guns[0], _inventory.HeadSlot, _inventory.OutwearSlot);
        _enemy = new Enemy(_guns[1]);

        _playerBar.Init(_player);
        _enemyBar.Init(_enemy);
        foreach (Gun gun in _guns)
            gun.Init(_inventory);

        _guns[1].DeSelect();
        _guns[0].Select();

        _player.OnDie += GameOver;
        _enemy.OnDie += Win;
    }

    private IEnumerator FightStepRoutine()
    {
        yield return StartCoroutine(_player.Gun.ShootRoutine(_enemy, true));
        yield return StartCoroutine(_enemy.Gun.ShootRoutine(_player, false));

        _fighting = false;
    }

    private void GameOver()
    {
        StopAllCoroutines();

        _player.OnDie -= GameOver;
        _enemy.OnDie -= Win;

        _gameOverWindow.SetActive(true);
        _fighting = false;
        InitGameplay();
    }

    private void Win()
    {
        StopAllCoroutines();

        _player.OnDie -= GameOver;
        _enemy.OnDie -= Win;

        _inventory.SearchSlotAndAddItem(ConfigManager.Instance.CreateRandomItem().ID);
        _fighting = false;
        InitGameplay(); 
    }
}
