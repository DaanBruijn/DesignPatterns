// - Basic Target Script 
// - Can be damaged
// - Daniel Bruijn

public class Target : IDamageable
{
    // - Varibales
    private int _health;
    private int _maxHealth;

    public bool IsDestroyed => _health <= 0;
    
    public Target(int health)
    {
        _health = health;
        _maxHealth = health;
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public void Reset()
    {
        _health = _maxHealth;
    }
}
