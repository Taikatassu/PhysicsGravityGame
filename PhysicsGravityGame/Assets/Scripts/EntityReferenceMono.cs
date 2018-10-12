using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EntityReferenceMono : MonoBehaviour, IEventListener {

    public GameEntity entity { get; private set; }

    public void RegisterListeners(IEntity entity) {
        this.entity = (GameEntity)entity;
    }
}
