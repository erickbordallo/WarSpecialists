using UnityEngine;

public class Maria : PlayerBase
{
    private AudioSource deadSound;
    protected override void Start()
    {
        Damage = 3;
        Deffense = 4;
        AttackSpeed = 2f;
        SpecialtyPoints = 0;
        Gold = 0;

        Transform deadSoundChild = gameObject.transform.Find("DeadSound");
        
        if (deadSoundChild != null)
            deadSound = deadSoundChild.GetComponent<AudioSource>();
    }
    protected override void Update()
    {
        if (!IsAlive)
        {
            Died();
        }
    }

    public void PlayDeadSound()
    {
        if (deadSound != null)
            deadSound.Play();

    }
}