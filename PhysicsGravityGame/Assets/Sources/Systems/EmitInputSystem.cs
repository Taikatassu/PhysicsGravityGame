using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class EmitInputSystem : IExecuteSystem {
    private InputContext inputContext;

    public EmitInputSystem(Contexts contexts) {
        inputContext = contexts.input;
    }

    public void Execute() {
        inputContext.inputPrimaryActionButtonPressed = Input.GetMouseButtonDown(0);
        inputContext.inputPrimaryActionButtonHeld = Input.GetMouseButton(0);
        inputContext.inputPrimaryActionButtonReleased = Input.GetMouseButtonUp(0);

        inputContext.inputSecondaryActionButtonPressed = Input.GetMouseButtonDown(1);
        inputContext.inputSecondaryActionButtonHeld = Input.GetMouseButton(1);
        inputContext.inputSecondaryActionButtonReleased = Input.GetMouseButtonUp(1);

        inputContext.ReplaceMousePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
