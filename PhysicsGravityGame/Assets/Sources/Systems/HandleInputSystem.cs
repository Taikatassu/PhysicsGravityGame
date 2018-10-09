using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class HandleInputSystem : IExecuteSystem {
    private Contexts contexts;
    private IGroup<GameEntity> playerControlledShooters;

    public HandleInputSystem(Contexts contexts) {
        this.contexts = contexts;
        playerControlledShooters = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.PlayerControlledShooter,
                GameMatcher.Position
            ));
    }

    public void Execute() {
        var inputContext = contexts.input;
        if (inputContext.inputPrimaryActionButtonPressed) {
            foreach (var e in playerControlledShooters) {
                var shootDirection = (inputContext.mousePosition.value - e.position.value).normalized;
                e.ReplaceShootDirection(shootDirection);
            }
        }

        //TODO: Move black hole creation to separate system, just add a flag for it here
        if (inputContext.inputSecondaryActionButtonPressed) {
            if (contexts.game.blackHoleEntity == null) {
                CreateBlackHole(contexts, inputContext.mousePosition.value);
                Debug.Log("Created new black hole on button down");
            }
        }

        if (inputContext.inputSecondaryActionButtonReleased) {
            if (contexts.game.blackHoleEntity != null) {
                contexts.game.blackHoleEntity.isDestroyed = true;
                Debug.Log("Destroyed black hole on button up");
            }
        }

        if (inputContext.inputSecondaryActionButtonHeld) {
            if (contexts.game.blackHoleEntity != null) {
                contexts.game.blackHoleEntity.ReplacePosition(inputContext.mousePosition.value);
            } else {
                CreateBlackHole(contexts, inputContext.mousePosition.value);
                Debug.Log("Created new black hole on button held, since none existed");
            }
        }
    }

    private static void CreateBlackHole(Contexts contexts, Vector2 position) {
        var blackHoleRadius = 5f;
        var blackHoleEntity = contexts.game.CreateEntity();
        blackHoleEntity.isBlackHole = true;
        blackHoleEntity.ReplacePosition(position);
        blackHoleEntity.ReplaceVelocity(Vector2.zero);
        blackHoleEntity.ReplaceMass(99999999f);
        blackHoleEntity.ReplaceRadius(blackHoleRadius);

        ViewService.LoadAsset(contexts, blackHoleEntity, GameControllerMono.blackHoleAssetName, position);
    }
}
