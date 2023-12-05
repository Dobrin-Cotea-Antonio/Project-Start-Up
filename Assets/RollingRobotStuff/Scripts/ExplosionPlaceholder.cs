using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlaceholder : MonoBehaviour
{

    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private SkinnedMeshRenderer enemyMesh;

    private Material enemyBodyMaterial;
    private bool coolDown = false;

    private void Start()
    {
        enemyBodyMaterial = enemyMesh.material;
    }

    public IEnumerator Explode()
    {
        if (!coolDown)
        {
            coolDown = true;
            enemyBodyMaterial.SetColor("_EmissionColor", new Color(150, 150, 150));

            yield return new WaitForSeconds(0.5f);
            ParticleSystem instantiatedPS = Instantiate(explosion);
            instantiatedPS.transform.position = gameObject.transform.position;

            //In real game, destroy instead of deactivating and use root of prefab instead
            enemyMesh.gameObject.SetActive(false);

            //Demo only stuff
            yield return new WaitForSeconds(1f);
            enemyMesh.gameObject.SetActive(true);
            enemyBodyMaterial.SetColor("_EmissionColor", new Color(46, 46, 46));
            coolDown = false;
        }
    }
}
