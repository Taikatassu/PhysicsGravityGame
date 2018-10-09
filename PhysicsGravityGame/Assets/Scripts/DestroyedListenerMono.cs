using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class DestroyedListenerMono : MonoBehaviour, IEventListener, IDestroyedListener {

    public void RegisterListeners(IEntity entity) {
        var gameEntity = (GameEntity)entity;
        if (gameEntity == null) {
            Debug.Log("Cannot add listeners: Entity was null or incorrect type.");
            return;
        }

        gameEntity.AddDestroyedListener(this);
    }

    public void OnDestroyed(GameEntity entity) {
        if (gameObject.GetEntityLink() != null)
            gameObject.Unlink();

        Destroy(gameObject);
    }
}
