using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class AddViewSystem : ReactiveSystem<GameEntity> {
  readonly Transform viewContainer;

  public AddViewSystem(Contexts contexts) : base(contexts.game) {
    viewContainer = new GameObject("Views").transform;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.Asset);
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasAsset;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (GameEntity entity in entities) {
      var asset = Resources.Load<GameObject>(entity.asset.name);

      try {
        var gameObject = Object.Instantiate(asset);
        gameObject.transform.parent = viewContainer;
        entity.AddView(gameObject);
      } catch (System.Exception) {
        Debug.Log("Cannot instantiate " + asset);
      }
    }
  }
}