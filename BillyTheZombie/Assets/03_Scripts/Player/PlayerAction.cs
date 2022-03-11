using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _controller;

    //Reference GameObjects
    [Header("Player's body parts")]
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _body;
    [Tooltip("Insert the player's Arms: [0]-Right || [1]-Left")]
    [SerializeField] private List<GameObject> _arms;

    //Prefabs
    [Header("Arm Prefabs")]
    [SerializeField] private List<GameObject> _rightArm;
    [SerializeField] private List<GameObject> _leftArm;

    private List<bool> _canThrow = new List<bool> { true, true };

    void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        //Look direction
        Vector2 look = _controller.Look;
        Vector3 currentAimPos = _aim.transform.localPosition; 
        if (look != Vector2.zero) 
        {
            _cameraTarget.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
            _aim.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
        }
        else if(_controller.Movement != Vector2.zero)
        {
            _cameraTarget.transform.localPosition = new Vector3(_controller.Movement.x, _controller.Movement.y, 0.0f);
            _aim.transform.localPosition = new Vector3(_controller.Movement.x, _controller.Movement.y, 0.0f);
        }
        else
        {
            _cameraTarget.transform.localPosition = Vector3.zero;
            _aim.transform.localPosition = currentAimPos;
        }

        if (_controller.ArmR && _canThrow[(int)ARMSIDE.RIGHT])
        {
            EnablePlayersArm(ARMSIDE.RIGHT, false);
            InstantiateArm(ARMSIDE.RIGHT);
            _canThrow[(int)ARMSIDE.RIGHT] = false;

        }
        if (_controller.ArmL && _canThrow[(int)ARMSIDE.LEFT])
        {
            EnablePlayersArm(ARMSIDE.LEFT, false);
            InstantiateArm(ARMSIDE.LEFT);
            _canThrow[(int)ARMSIDE.LEFT] = false;
        }
        if (_controller.Head)
        {
            //Headtbutt
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
    private void InstantiateArm(ARMSIDE armSide)
    {
        int rightArmAbilityIndex = 0;
        int leftArmAbilityIndex = 0;

        if (armSide == ARMSIDE.RIGHT)
        {
            //will have to adapt to ability chosen
            Instantiate(_rightArm[rightArmAbilityIndex].gameObject, _aim.transform.position, Quaternion.identity);
        }
        if (armSide == ARMSIDE.LEFT)
        {
            //will have to adapt to ability chosen
            Instantiate(_leftArm[leftArmAbilityIndex].gameObject,_aim.transform.position, Quaternion.identity);
        }
    }
}
