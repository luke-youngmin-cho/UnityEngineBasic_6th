using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public Node node;
    public TowerType type;
    public int upgradeLevel;
    protected LayerMask targetMask;
    protected float detectRange;

    protected virtual Collider[] DetectTargets()
    {
        return Physics.OverlapSphere(transform.position, detectRange, targetMask);
    }

    private void OnMouseUpAsButton()
    {
        TowerUI.instance.Show(this);
    }
}