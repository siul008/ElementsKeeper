using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : TowerBehaviour
{
    [SerializeField]
    float range;
    [SerializeField]
    GameObject flamethrow;

    // Audio sources for the flame sounds

    private bool isFiring = false;

    private void Start()
    {
        flamethrow.GetComponent<TowerProjectile>().SetupProjectile(tower.damage, tower.attackRate);
    }

    public override bool IsEnemyInFront()
    {
        return Physics2D.Raycast(transform.position, Vector2.right, range, mask);
    }

    public override void Update()
    {
        if (IsEnemyInFront())
        {
            if (!isFiring)
            {
                Fire();
            }
        }
        else
        {
            if (isFiring)
            {
                StopFire();
            }
        }
    }

    void StopFire()
    {
        isFiring = false;
        flamethrow.SetActive(false);

        SoundManager.Instance.InstantPlaySfx("FlamethrowEnd", false);
        //flameLoopAudioSource.Stop();
        //flameEndAudioSource.Play();
    }

    public override void Fire()
    {
        isFiring = true;
        flamethrow.SetActive(true);

        // Play start sound and schedule loop sound
        SoundManager.Instance.InstantPlaySfx("FlamethrowStart", false);
        StartCoroutine(PlayFlameLoopAfterStart());
    }

    private IEnumerator PlayFlameLoopAfterStart()
    {
        // Wait for the start sound to finish
        yield return new WaitForSeconds(0.3f);

        // Only play loop sound if still firing
        if (isFiring)
        {
            SoundManager.Instance.InstantPlaySfx("FlamethrowLoop", false);
           // flameLoopAudioSource.Play();
        }
    }
}
