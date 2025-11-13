public interface IDamageAble : IHitable
{
    void TakeDamage(int damageAmount, float knockBackThrust);
}
