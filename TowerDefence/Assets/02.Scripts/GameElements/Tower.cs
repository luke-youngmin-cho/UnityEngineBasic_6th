using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public Node node;
    public TowerType type;
    public int upgradeLevel;
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] protected float detectRange;

    protected virtual Collider[] DetectTargets()
    {
        return Physics.OverlapSphere(transform.position, detectRange, targetMask);
    }

    private void OnMouseUpAsButton()
    {
        TowerUI.instance.Show(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}