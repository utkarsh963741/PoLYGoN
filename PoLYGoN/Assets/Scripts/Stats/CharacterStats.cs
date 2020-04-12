using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth=100;
    public float currentHealth;
    public float armor = 0;
    public float hunger = 75;
    public float stamina = 100;

    private float timer=0f;

    public UIStatBars healthBar;
    public UIStatBars armorBar;
    public UIStatBars hungerBar;
    public UIStatBars staminaBar;
    

    void Awake()
    {
        currentHealth = maxHealth;
        
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth,currentHealth);
        armorBar.SetMaxValue(100,armor);
        hungerBar.SetMaxValue(100,hunger);
        staminaBar.SetMaxValue(100,stamina);
    }

    void Update() 
    {
        timer-=Time.deltaTime;

        if(hunger < 20 && hunger > 10 && timer <= 0)
        {
            currentHealth -= 0.5f;
            healthBar.SetValue(currentHealth);
            timer =1;
            if(currentHealth <=0){ Die(); }
        }
        if(hunger < 10 && hunger >0)
        {
            currentHealth -= 2.5f;
            healthBar.SetValue(currentHealth);
            timer =1;
            if(currentHealth <=0){ Die(); }
        }
        if(hunger ==0 )
        {
            currentHealth -= 7.5f;
            healthBar.SetValue(currentHealth);
            timer =1;
            if(currentHealth <=0){ Die(); }
        }
    }

    public void TakeDamage(float damage)
    {
        float armorBefore = armor;
        armor -= damage;
        armor = Mathf.Clamp(armor,0,int.MaxValue);

        float leftDamage = damage- armorBefore;
        leftDamage = Mathf.Clamp(leftDamage,0,int.MaxValue);
        
        currentHealth -= leftDamage;
        Debug.Log(transform.name + " takes" + damage + " damage.");

        if(currentHealth <=0)
        { 
            Die();
        }

        healthBar.SetValue(currentHealth);
        armorBar.SetValue(armor);
    }

    public void Die()
    {
        Debug.Log(transform.name + " died.");
        PlayerManager.instance.KillPlayer();
    }

    public void AddModifier(float health,float armoradd ,float hungeradd,float staminaadd)
    {
        currentHealth += health;
        currentHealth = Mathf.Clamp(currentHealth,0,100);

        armor += armoradd;
        armor = Mathf.Clamp(armor,0,100);

        hunger += hungeradd;
        hunger =Mathf.Clamp(hunger,0,100);

        stamina += staminaadd;
        stamina = Mathf.Clamp(stamina,0,100);

        healthBar.SetValue(currentHealth);
        armorBar.SetValue(armor);
        hungerBar.SetValue(hunger);
        staminaBar.SetValue(stamina);

    }
}


