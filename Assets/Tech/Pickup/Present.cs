using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public List<ProjectileWeapon> projectileWeaponsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            int weaponNumber = Random.Range(0, projectileWeaponsToSpawn.Count - 1); //-1 because it starts counting at 0

            ProjectileWeapon projectileWeaponToSpawn = projectileWeaponsToSpawn[weaponNumber];

            Instantiate(projectileWeaponToSpawn.gameObject, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
