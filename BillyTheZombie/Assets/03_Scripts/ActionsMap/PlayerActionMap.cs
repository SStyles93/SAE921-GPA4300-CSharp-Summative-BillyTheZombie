//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/03_Scripts/ActionsMap/PlayerActionMap.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActionMap : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActionMap"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""ab0232e3-9854-40ec-813e-e697be16afac"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""43d51ba4-4aaa-48ca-bf56-6124f52bad9b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""1aca8aae-0989-47f4-81d6-9ce134ca4064"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Head"",
                    ""type"": ""Value"",
                    ""id"": ""6fdb4ef4-f5ec-4218-9446-4e7674efafd2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ArmR"",
                    ""type"": ""Value"",
                    ""id"": ""e1a4de54-599b-4d17-a14d-c4ca796d74de"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ArmL"",
                    ""type"": ""Value"",
                    ""id"": ""44a91d46-d65d-4a9e-8625-54fae429ddfc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5821e3fc-ada7-4e9b-9c8e-1a6a0ae18d47"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee9cf710-bdcc-40d6-b63c-4eb152a28550"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14ea341a-9bf2-4b7b-b90d-5f0297d9e6a7"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArmL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f21149a3-30eb-4b73-9a23-5d24a3d4dc75"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArmR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a65f6ed-aa03-474b-92b2-db100a09630d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Head"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Move = m_PlayerMovement.FindAction("Move", throwIfNotFound: true);
        m_PlayerMovement_Look = m_PlayerMovement.FindAction("Look", throwIfNotFound: true);
        m_PlayerMovement_Head = m_PlayerMovement.FindAction("Head", throwIfNotFound: true);
        m_PlayerMovement_ArmR = m_PlayerMovement.FindAction("ArmR", throwIfNotFound: true);
        m_PlayerMovement_ArmL = m_PlayerMovement.FindAction("ArmL", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Move;
    private readonly InputAction m_PlayerMovement_Look;
    private readonly InputAction m_PlayerMovement_Head;
    private readonly InputAction m_PlayerMovement_ArmR;
    private readonly InputAction m_PlayerMovement_ArmL;
    public struct PlayerMovementActions
    {
        private @PlayerActionMap m_Wrapper;
        public PlayerMovementActions(@PlayerActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMovement_Move;
        public InputAction @Look => m_Wrapper.m_PlayerMovement_Look;
        public InputAction @Head => m_Wrapper.m_PlayerMovement_Head;
        public InputAction @ArmR => m_Wrapper.m_PlayerMovement_ArmR;
        public InputAction @ArmL => m_Wrapper.m_PlayerMovement_ArmL;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLook;
                @Head.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnHead;
                @Head.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnHead;
                @Head.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnHead;
                @ArmR.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArmR;
                @ArmR.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArmR;
                @ArmR.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArmR;
                @ArmL.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArmL;
                @ArmL.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArmL;
                @ArmL.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnArmL;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Head.started += instance.OnHead;
                @Head.performed += instance.OnHead;
                @Head.canceled += instance.OnHead;
                @ArmR.started += instance.OnArmR;
                @ArmR.performed += instance.OnArmR;
                @ArmR.canceled += instance.OnArmR;
                @ArmL.started += instance.OnArmL;
                @ArmL.performed += instance.OnArmL;
                @ArmL.canceled += instance.OnArmL;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
    public interface IPlayerMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnHead(InputAction.CallbackContext context);
        void OnArmR(InputAction.CallbackContext context);
        void OnArmL(InputAction.CallbackContext context);
    }
}
