using UnityEngine;

public interface IMachine
{
    public Vector3Int gridPos { get; set; }
    public void TriggerItem(Item item);
    public Vector3Int GetWorldTargetDirection();
    public void OnActivate();
}