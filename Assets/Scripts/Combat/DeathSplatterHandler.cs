using UnityEngine;

public class DeathSplatterHandler : MonoBehaviour
{
    private void OnEnable()
    {
        Health.OnDeath += SpawnDeathVFX;
        Health.OnDeath += SpawnDeathSplatterPrefab;
    }
    private void OnDisable()
    {
       Health.OnDeath -= SpawnDeathVFX;
       Health.OnDeath -= SpawnDeathSplatterPrefab;
    }
    private void SpawnDeathSplatterPrefab(Health sender)
    {
        GameObject newSplatterPrefab = Instantiate(sender.SplatterPrefab, sender.transform.position, transform.rotation);
        SpriteRenderer deathSplatterSpriteRender = newSplatterPrefab.GetComponent<SpriteRenderer>();
        ColorChanger colorChanger = sender.GetComponent<ColorChanger>();
        Color currentColor = colorChanger.DefaultColor;
        deathSplatterSpriteRender.color = currentColor;
        newSplatterPrefab.transform.SetParent(this.transform);
    }
    private void SpawnDeathVFX(Health sender)
    {
        GameObject deathVFX = Instantiate(sender.DeathVFX, sender.transform.position, transform.rotation);
        ParticleSystem.MainModule ps = deathVFX.GetComponent<ParticleSystem>().main;
        ColorChanger colorChanger = sender.GetComponent<ColorChanger>();
        Color currentColor = colorChanger.DefaultColor;
        ps.startColor = currentColor;
        deathVFX.transform.SetParent(this.transform);
    }
}
