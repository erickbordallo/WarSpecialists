using UnityEngine;

public class Maria : PlayerBase
{
    private AudioSource deadSound;
    private Vector3 initialTransform;

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

        initialTransform = gameObject.transform.position;
    }

    public void PlayDeadSound()
    {
        if (deadSound != null)
            deadSound.Play();
    }

    public Vector3 GetInitialTransform()
    {
        return initialTransform;
    }

}