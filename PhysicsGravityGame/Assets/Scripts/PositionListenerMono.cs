using Entitas;
using UnityEngine;

public class PositionListenerMono : MonoBehaviour, IEventListener, IPositionListener {

    public bool useRigidbody;
    GameEntity entity;
    Rigidbody rb;

    public void RegisterListeners(IEntity entity) {
        this.entity = (GameEntity)entity;
        if(useRigidbody)
            rb = GetComponent<Rigidbody>();

        if(this.entity == null) {
            Debug.Log("Cannot add listeners: Entity was null or incorrect type.");
            return;
        }

        this.entity.AddPositionListener(this);
    }

    public void OnPosition(GameEntity entity, Vector2 value) {
        if(useRigidbody)
            rb.MovePosition(value);
        else
            transform.position = value;
    }
}
