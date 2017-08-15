using UnityEngine;

public class PawnCharacter : MonoBehaviour {

    public float health;
    public float defense;
    public float attack;
    public float attackRate;
    private bool isEnemy;
	

    public virtual void Damage(float _damage)
    {
        health -= _damage;
        Debug.Log(health);
        if(health <= 0)
        {
            
            Destroy(gameObject);
            return;
        }
    }
}
