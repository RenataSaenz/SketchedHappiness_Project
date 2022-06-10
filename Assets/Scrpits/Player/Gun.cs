
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float damage = 1f;
    [SerializeField]
    private float range = 100f;
    [SerializeField]
    private Camera fpsCam;
    [SerializeField]
    private ParticleSystem flash;
    [SerializeField]
    private ParticleSystem shootParticle;
    [SerializeField]
    private GameObject impactEffect;
    private bool _noGun;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AudioManager.instance.Play(AudioManager.Types.GunShoot);
            Shoot();
        }
    }
    void Shoot()
    {
        flash.Play();
        shootParticle.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("You shot: " + hit.transform.name);
           
             Enemy enemy = hit.transform.GetComponent<Enemy>();
             Bee bee = hit.transform.GetComponent<Bee>();
            
            if (enemy != null) enemy.TakeDamage(damage);
            if (bee != null) bee.TakeDamage(damage);
            
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f);
        }
    }
}
