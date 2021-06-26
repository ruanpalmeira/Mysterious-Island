using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab, coinPrefab, runePrefab, cakePrefab;
    public bool isSpawn = false;
    public bool spawn = false;
    public bool isCoin = false;
    public bool isEnemy = false;
    public bool isRune = false;
    public bool isRunestone = false;
    public bool isCake = false;
    public Runestatue runestatue;
    private AudioSource spawnSound;
    public AudioClip enemySpawn, coinSpawn, runeSpawn, cakeSpawn;
    public GameObject player;
    public PlayerController playerController;
    public SpriteRenderer StatueGem;
    public GameController gameController;

    private void Start(){
        spawnSound = GetComponent<AudioSource>();
        //StatueGem = GameObject.Find("StatueGem");
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        runestatue = GameObject.FindGameObjectWithTag("Statue").GetComponent<Runestatue>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StatueGem = GameObject.FindGameObjectWithTag("Statuegem").GetComponent<SpriteRenderer>();
        StatueGem.enabled = false;
    }

    // Update is called once per frame
    void Update(){
        if (isSpawn){
            if (spawn){
                if (isEnemy){
                    spawnSound.clip = enemySpawn;
                    Instantiate(enemyPrefab, transform.position, transform.rotation);
                    //Debug.Log("EnemySpawned");
                }

                if (isCoin)
                {
                    spawnSound.clip = coinSpawn;
                    Instantiate(coinPrefab, transform.position, transform.rotation);
                    //Debug.Log("CoinSpawned");
                }

                if (isRune)
                {
                    spawnSound.clip = runeSpawn;
                    Instantiate(runePrefab, transform.position, transform.rotation);
                }
                if (isCake)
                {
                    //spawnSound.clip = cakeSpawn;
                    Instantiate(cakePrefab, transform.position, transform.rotation);
                }
                if (isRunestone)
                {
                    if (playerController.hasRune && !StatueGem.enabled){
                            StatueGem.enabled = true;
                            
                        }
                        
                        runestatue.setGlow(playerController.hasRune);
                        if(gameController.getLevel() != 4){
                            gameController.LoadNextLevel();
                        }else{
                            gameController.setGameOver(0);
                        }
                        //Instantiate(runePrefab, transform.position, transform.rotation);
                    
                }
                spawnSound.Play();
                spawn = false;
            }
        }
    }

    public void setSpawn()
    {
        spawn = true;
    }
}
