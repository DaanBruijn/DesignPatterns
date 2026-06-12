using UnityEngine;

// - Basic Target Script 
// - Can be damaged
// - Daniel Bruijn

public class Target : IDamageable
{
    // - Varibales
    private int _health;

    public bool IsDestroyed => _health <= 0;
    
    public Target(int health)
    {
        _health = health;
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
    }
}
