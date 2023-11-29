using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputSetterManager
{
    List<IInputSetter> inputsSetters = new List<IInputSetter>()
    {
        new GamepadInputSetter(),
        new KeyboardInputSetter()
    };

    public void SetPlayerInput(string inputDevice, PlayerInput playerInput)
        => inputsSetters.Find(inputSetter => inputSetter.IsType(inputDevice)).SetInput(inputDevice, playerInput);
}