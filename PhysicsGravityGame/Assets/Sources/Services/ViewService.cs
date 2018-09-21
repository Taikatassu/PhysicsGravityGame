using Entitas;
using UnityEngine;
using Entitas.Unity;

public class ViewService {
    private static Transform viewHolder;
    private static string viewHolderName = "Views";

    public static void LoadAsset(Contexts contexts, IEntity entity, string assetName) {
        LoadAsset(contexts, entity, assetName, Vector2.zero);
    }

    public static void LoadAsset(Contexts contexts, IEntity entity, string assetName, Vector2 position) {
        var viewGo = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + assetName));
        if(viewGo == null) return;

        if(viewHolder == null) viewHolder = new GameObject(viewHolderName).transform;
        viewGo.transform.SetParent(viewHolder);

        viewGo.transform.position = new Vector3(position.x, position.y, 0f);

        viewGo.Link(entity, contexts.game);

        var eventListeners = viewGo.GetComponentsInChildren<IEventListener>();
        foreach(var listener in eventListeners) listener.RegisterListeners(entity);
    }
}