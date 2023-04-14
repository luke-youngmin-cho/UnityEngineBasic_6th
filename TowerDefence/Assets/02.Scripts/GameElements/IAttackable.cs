public interface IAttackable
{
    public float damage { get; }
    public float damageModified { get; set; }

    public float Attack();
}