using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerActions : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _playerController;
    private PlayerStats _playerStats;
    private PlayerVisuals _playerVisuals;
    [SerializeField] private PlayerStatsSO _playerStatSO;

    //Reference GameObjects
    [Header("Player's body parts")]
    [SerializeField] private GameObject mouseTarget;
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _head;
    [Tooltip("Insert the player's Arms:\n[0]-Right\n[1]-Left")]
    [SerializeField] private List<GameObject> _arms;

    //Prefabs
    [Header("Arm Prefabs")]
    [Tooltip("Contains all arm prefabs for the right arm")]
    [SerializeField] private List<GameObject> _rightArms;
    [Tooltip("The Index of ability chosen for rightArm")]
    [SerializeField] private int _chosenAbilityIdxR = 0;
    [Tooltip("Contains all arm prefabs for the left arm")]
    [SerializeField] private List<GameObject> _leftArms;
    [Tooltip("The Index of ability chosen for leftArm")]
    [SerializeField] private int _chosenAbilityIdxL = 0;

    //List of bools used for Actions
    [Header("Action's variables")]
    [SerializeField] private float _headbuttCoolDownTime = 1.0f;
    private float _headbuttCoolDown = 1.0f;
    private bool _canHeadbutt = true;
    private bool[] _canThrow = new bool[2] { true, true };
    private bool _canHit = true;
    private bool _isInCombat = true;

    //Properties
    public bool CanHit { get => _canHit; set => _canHit = value; }
    public bool[] CanThrow { get => _canThrow; set => _canThrow = value; }
    public bool CanHeadbutt { get => _canHeadbutt; set => _canHeadbutt = value; }
    public GameObject Aim { get => _aim; private set => _aim = value; }
    public bool IsInCombat { get => _isInCombat; set => _isInCombat = value; }

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerStats = GetComponent<PlayerStats>();
        _playerVisuals = GetComponentInChildren<PlayerVisuals>();
    }
    private void Start()
    {
        _aim.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
        _chosenAbilityIdxR = _playerStatSO._rightArmType;
        _chosenAbilityIdxL = _playerStatSO._leftArmType;
    }

    void Update()
    {
        if (_isInCombat)
        {
            PlayerCombatLook();
        }
        else
        {
            PlayerPasiveLook();
        }
        

        if (_canHit)
        {
            ActionCheck();
        }
        
        //Sets the position of the head at half the aim's position;
        _head.transform.position = _aim.transform.position;
        _head.transform.localPosition /= 2.0f;
    }

    /// <summary>
    /// Updates the player look direction
    /// </summary>
    private void PlayerCombatLook()
    {
        switch (_playerController.ControlScheme)
        {
            case "Gamepad":

                //Switches from MouseTarget to GamepadTarget
                mouseTarget.gameObject.SetActive(false);
                _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;

                //Updates the Aim position according to the Gamepad input
                Vector2 look = _playerController.Look;
                Vector3 currentAimPos = _aim.transform.localPosition;
                if (look != Vector2.zero)
                {
                    _cameraTarget.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
                    _aim.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
                    _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;
                }
                #region MoveToAim

                //Vector2 movement = _controller.Movement;
                //else if (_controller.Movement != Vector2.zero)
                //{
                //    _cameraTarget.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
                //    _aim.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
                //    _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;
                //}

                #endregion
                else
                {
                    _cameraTarget.transform.localPosition = Vector3.zero;
                    _cameraTarget.GetComponent<SpriteRenderer>().enabled = false;
                    _aim.transform.localPosition = currentAimPos;
                }
                break;
            case "Keyboard":
                
                //Switches from GamepadTarget to MouseTarget
                mouseTarget.gameObject.SetActive(true);
                _cameraTarget.GetComponent<SpriteRenderer>().enabled = false;

                //Updates the Aim position according to the Mouse position 
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 0.0f;
                mouseTarget.transform.position = mousePos;
                _aim.transform.localPosition = 
                _cameraTarget.transform.localPosition = 
                    mouseTarget.transform.localPosition.normalized;
                break;

            default:
                mouseTarget.gameObject.SetActive(false);
                break;
        }
        
    }

    /// <summary>
    /// Updates the player look when out of combat
    /// </summary>
    private void PlayerPasiveLook()
    {
        Vector3 currentAimPos = _aim.transform.localPosition;
        Vector2 movement = _playerController.Movement;
        if (_playerController.Movement != Vector2.zero)
        {
            _cameraTarget.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
            _aim.transform.localPosition = new Vector3(movement.x, movement.y, 0.0f);
            _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            _cameraTarget.transform.localPosition = Vector3.zero;
            _cameraTarget.GetComponent<SpriteRenderer>().enabled = false;
            _aim.transform.localPosition = currentAimPos;
        }
    }

    /// <summary>
    /// Activates the player's actions
    /// </summary>
    private void ActionCheck()
    {
        //RightArm Throw
        if (_playerController.ArmR && _canThrow[(int)BODYPART.RIGHTARM])
        {
            EnablePlayersArm(BODYPART.RIGHTARM, false);
            InstantiateArm(BODYPART.RIGHTARM);
            _canThrow[(int)BODYPART.RIGHTARM] = false;

        }
        
        //LeftArm Throw
        if (_playerController.ArmL && _canThrow[(int)BODYPART.LEFTARM])
        {
            EnablePlayersArm(BODYPART.LEFTARM, false);
            InstantiateArm(BODYPART.LEFTARM);
            _canThrow[(int)BODYPART.LEFTARM] = false;
        }

        //Headbutt
        if (_playerController.Head && _canHeadbutt)
        {
            _canHeadbutt = false;
            Headbutt();
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
    public void EnablePlayersArm(BODYPART armSide, bool enable)
    {
        _arms[(int)armSide].SetActive(enable);
        _canThrow[(int)armSide] = true;

    }

    /// <summary>
    /// Instantiates an arm
    /// </summary>
    /// <param name="armSide">Define which arm to instantiate</param>
    private void InstantiateArm(BODYPART armSide)
    {
        Vector3 instantiationPos = _aim.transform.position;
        
        if (armSide == BODYPART.RIGHTARM)
        {
            //will have to adapt to ability chosen
            GameObject currentArm = Instantiate(_rightArms[_chosenAbilityIdxR].gameObject, instantiationPos, Quaternion.identity);
            Arm arm = currentArm.GetComponent<Arm>();
            arm.ArmDirection = _aim.transform.localPosition;
            arm.Damage *= _playerStats.ArmDamage;
            //ThrowPosition used for BommerangArm
            arm.ThrowPosition = transform.position;
        }
        if (armSide == BODYPART.LEFTARM)
        {
            //will have to adapt to ability chosen
            GameObject currentArm = Instantiate(_leftArms[_chosenAbilityIdxL].gameObject, instantiationPos, Quaternion.identity);
            Arm arm = currentArm.GetComponent<Arm>();
            arm.ArmDirection = _aim.transform.localPosition;
            arm.Damage *= _playerStats.ArmDamage;
            //ThrowPosition used for BommerangArm
            arm.ThrowPosition = transform.position;
        }
    }

    /// <summary>
    /// Launches the Headbutt
    /// </summary>
    private void Headbutt()
    {
        _playerVisuals.StartHeadbutt();
        _head.GetComponent<Headbutt>().PushPower = _playerStats.PushPower;
    }
}
