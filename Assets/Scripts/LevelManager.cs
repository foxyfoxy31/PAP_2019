using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject currentCheckpoint;

    private PlayerController2D player;

    public GameObject DeathParticle;

    public GameObject RespawnParticle;

    public float respawnDelay;

    public Renderer rend;

    private float gravityStore;

    private CameraController camera;

    public HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController2D>();
        camera = FindObjectOfType<CameraController>();
        rend = player.GetComponent<Renderer>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer() {
        StartCoroutine("RespawnPlayerCo");

        }

    public IEnumerator RespawnPlayerCo() {
        player.knockbackCount = 0;
        player.rb2d.velocity = new Vector2 (0, 0);
        player.enabled = false;
        camera.isFollowing = false;
        gravityStore = player.rb2d.gravityScale;
        player.rb2d.gravityScale = 0f;
        player.animator.Play("player_death");
        player.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(1);
        rend.enabled = false;
        Instantiate (DeathParticle, player.transform.position, player.transform.rotation);
        Debug.Log("Player Respawn");
        yield return new WaitForSeconds(respawnDelay);
        player.transform.position = currentCheckpoint.transform.position;
        camera.isFollowing = true;
        player.GetComponent<Rigidbody2D>().simulated = true;
        Instantiate (RespawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
        player.enabled = true;
        healthManager.FullHealth();
        healthManager.isDead = false;
        player.rb2d.gravityScale = gravityStore;
        rend.enabled = true;
        player.TurnInvincible();

    }

}
