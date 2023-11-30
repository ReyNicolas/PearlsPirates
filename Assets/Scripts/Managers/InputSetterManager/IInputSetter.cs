using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public abstract class IInputSetter
{
    protected string inputType;

    public bool IsType(string input)
    {
        return input.Contains(inputType);
    }
    public abstract void SetInput(string inputDevice, PlayerInput playerInput);
}

public class KeyboardInputSetter : IInputSetter
{
    public KeyboardInputSetter()
    {
        inputType =InputsTypesNames.KEYBOARD;
    }

    public override void SetInput(string inputDevice, PlayerInput playerInput)
    {
        if (playerInput.user.valid)
        {
            playerInput.user.UnpairDevices();
            InputUser.PerformPairingWithDevice(Keyboard.current, user: playerInput.user);
        }        
        playerInput.defaultActionMap = inputDevice;
        playerInput.SwitchCurrentActionMap(inputDevice);
    }
}

public class GamepadInputSetter : IInputSetter
{
    public GamepadInputSetter()
    {
        inputType = InputsTypesNames.GAMEPAD;
    }

    public override void SetInput(string inputDevice, PlayerInput playerInput)
    {
        playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(Gamepad.all[int.Parse(inputDevice.Replace(inputType, "")) - 1], user: playerInput.user);
        playerInput.defaultActionMap = inputType;
        playerInput.SwitchCurrentActionMap(inputType);
    }
}
