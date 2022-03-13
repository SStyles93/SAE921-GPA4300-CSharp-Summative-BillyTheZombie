using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _controller;
    private PlayerStats _stats;

    //Reference GameObjects
    [Header("Player's body parts")]
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _body;
    [Tooltip("Insert the player's Arms: [0]-Right || [1]-Left")]
    [SerializeField] private List<GameObject> _arms;

    //Prefabs
    [Header("Arm Prefabs")]
    [Tooltip("Contains all arm prefabs for the right arm")]
    [SerializeField] private List<GameObject> _rightArms;
    [Tooltip("Contains all arm prefabs for the left arm")]
    [SerializeField] private List<GameObject> _leftArms;

    //List of bools used for Actions
    [Header("Action's variables")]
    [SerializeField] private float _headbuttCoolDownTime = 1.0f;
    private float _headbuttCoolDown = 1.0f;
    private bool _canHeadbutt = true;
    private List<bool> _canThrow = new List<bool> { true, true };

    //Properties
    public List<bool> CanThrow { get => _canThrow; set => _canThrow = value; }
    public bool CanHit { get => _canHeadbutt; set => _canHeadbutt = value; }

    void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        UpdatePlayerLookDirection();
        ActionCheck();        
    }

    /// <summary>
    /// Updates the player look direction
    /// </summary>
    private void UpdatePlayerLookDirection()
    {
        //Look direction
        Vector2 look = _controller.Look;
        Vector3 currentAimPos = _aim.transform.localPosition;
        if (look != Vector2.zero)
        {
            _cameraTarget.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
            _aim.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
        }
        else if (_controller.Movement != Vector2.zero)
        {
            _cameraTarget.transform.localPosition = new Vector3(_controller.Movement.x, _controller.Movement.y, 0.0f);
            _aim.transform.localPosition = new Vector3(_controller.Movement.x, _controller.Movement.y, 0.0f);
        }
        else
        {
            _cameraTarget.transform.localPosition = Vector3.zero;
            _aim.transform.localPosition = currentAimPos;
        }
    }

    /// <summary>
    /// Activates the player's actions
    /// </summary>
    private void ActionCheck()
    {
        //RightArm Throw
        if (_controller.ArmR && _canThrow[(int)ARMSIDE.RIGHT])
        {
            EnablePlayersArm(ARMSIDE.RIGHT, false);
            InstantiateArm(ARMSIDE.RIGHT);
            _canThrow[(int)ARMSIDE.RIGHT] = false;

        }
        
        //LeftArm Throw
        if (_controller.ArmL && _canThrow[(int)ARMSIDE.LEFT])
        {
            EnablePlayersArm(ARMSIDE.LEFT, false);
            InstantiateArm(ARMSIDE.LEFT);
            _canThrow[(int)ARMSIDE.LEFT] = false;
        }

        //Headbutt
        if (_controller.Head && _canHeadbutt)
        {
            //TODO : Headtbutt
            _canHeadbutt = false;
        }
        else if(!_canHeadbutt)
        {
            _headbuttCoolDown -= Time.deltaTime;
            if (_headbuttCoolDown <= 0.0f)
            {
                _headbuttCoolDown = _headbuttCoolDownTime;
                _canHeadbutt = true;
            }
        }
    }

    /// <summary>
    /// Enables or disables Arms
    /// </summary>
    /// <param name="armSide">Enum use to indicate which arms to interact with</param>
    /// <param name="enable">Bool to enable or disable</param>
    public void EnablePlayersArm(ARMSIDE armSide, bool enable)
    {
        _arms[(int)armSide].SetActive(enable);
        _canThrow[(int)armSide] = true;

    }
    
    /// <summary>
    /// Instantiates an arm
    /// </summary>
    /// <param name="armSide">Define which arm to instantiate</param>
    private void InstantiateArm(ARMSIDE armSide)
    {
        int rightArmAbilityIndex = 0;
        int leftArmAbilityIndex = 0;

        if (armSide == ARMSIDE.RIGHT)
        {
            //will have to adapt to ability chosen
            GameObject currentArm = Instantiate(_rightArms[rightArmAbilityIndex].gameObject, _aim.transform.position, Quaternion.identity);
            Arm arm = currentArm.GetComponent<Arm>();
            arm.ArmDirection = _aim.transform.localPosition;
            arm.Damage *= _stats.DamageRight;
        }
        if (armSide == ARMSIDE.LEFT)
        {
            //will have to adapt to ability chosen
            GameObject currentArm = Instantiate(_leftArms[leftArmAbilityIndex].gameObject,_aim.transform.position, Quaternion.identity);
            Arm arm = currentArm.GetComponent<Arm>();
            arm.ArmDirection = _aim.transform.localPosition;
            arm.Damage *= _stats.DamageLeft;
        }
    }
}
