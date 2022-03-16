using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _controller;
    private PlayerStats _stats;
    private PlayerVisuals _visuals;

    //Reference GameObjects
    [Header("Player's body parts")]
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _head;
    [Tooltip("Insert the player's Arms: [0]-Right || [1]-Left")]
    [SerializeField] private List<GameObject> _arms;

    //Prefabs
    [Header("Arm Prefabs")]
    [Tooltip("Contains all arm prefabs for the right arm")]
    [SerializeField] private List<GameObject> _rightArms;
    [Tooltip("The Index of ability chosen for rightArm")]
    [SerializeField] private int chosenAbilityIdxR = 0;
    [Tooltip("Contains all arm prefabs for the left arm")]
    [SerializeField] private List<GameObject> _leftArms;
    [Tooltip("The Index of ability chosen for leftArm")]
    [SerializeField] private int chosenAbilityIdxL = 0;

    //List of bools used for Actions
    [Header("Action's variables")]
    [SerializeField] private float _headbuttCoolDownTime = 1.0f;
    private float _headbuttCoolDown = 1.0f;
    private bool _canHeadbutt = true;
    private List<bool> _canThrow = new List<bool> { true, true };
    private bool _canHit = true;

    //Properties
    public bool CanHit { get => _canHit; set => _canHit = value; }
    public List<bool> CanThrow { get => _canThrow; set => _canThrow = value; }
    public bool CanHeadbutt { get => _canHeadbutt; set => _canHeadbutt = value; }
    public GameObject Aim { get => _aim; private set => _aim = value; }

    void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _stats = GetComponent<PlayerStats>();
        _visuals = GetComponentInChildren<PlayerVisuals>();
    }
    private void Start()
    {
        _aim.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
    }

    void Update()
    {
        UpdatePlayerLookDirection();

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
    private void UpdatePlayerLookDirection()
    {
        //Look direction
        Vector2 look = _controller.Look;
        Vector2 movement = _controller.Movement;
        Vector3 currentAimPos = _aim.transform.localPosition;
        
        if (look != Vector2.zero)
        {
            _cameraTarget.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
            _aim.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
            _cameraTarget.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (_controller.Movement != Vector2.zero)
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
        Vector3 InstantiationPos = _aim.transform.position;
        
        if (armSide == ARMSIDE.RIGHT)
        {
            //will have to adapt to ability chosen
            GameObject currentArm = Instantiate(_rightArms[chosenAbilityIdxR].gameObject, InstantiationPos, Quaternion.identity);
            Arm arm = currentArm.GetComponent<Arm>();
            arm.ArmDirection = _aim.transform.localPosition;
            arm.Damage *= _stats.DamageRight;
            //ThrowPosition used for BommerangArm
            arm.ThrowPosition = transform.position;
        }
        if (armSide == ARMSIDE.LEFT)
        {
            //will have to adapt to ability chosen
            GameObject currentArm = Instantiate(_leftArms[chosenAbilityIdxL].gameObject, InstantiationPos, Quaternion.identity);
            Arm arm = currentArm.GetComponent<Arm>();
            arm.ArmDirection = _aim.transform.localPosition;
            arm.Damage *= _stats.DamageLeft;
            //ThrowPosition used for BommerangArm
            arm.ThrowPosition = transform.position;
        }
    }

    /// <summary>
    /// Launches the Headbutt
    /// </summary>
    private void Headbutt()
    {
        _visuals.StartHeadbutt();
        _head.GetComponent<Headbutt>().PushPower = _stats.PushPower;
    }
}
