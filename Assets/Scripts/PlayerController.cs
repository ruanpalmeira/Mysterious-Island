using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    private Animator animator;
    [SerializeField]
    private float changeX, changeY;
    [SerializeField]
    private string direction;
    public ItemDetector dl;
    [SerializeField]
    private int goldCollected;
    public AudioClip coin, dmg, hit;
    public AudioSource audios;
    public bool attacking;
    public bool hasRune;
    
    
    public GameController gameController;
    public float maxHealth = 100;
    public float currentHealth;
    public bool isAlive = true;
    
    void Start(){
        animator = GetComponentInChildren<Animator>();
        audios = GetComponent<AudioSource>();
        movePoint.parent = null;
        attacking = false;
        currentHealth = maxHealth;
        gameController.SetMaxHealth(maxHealth);
    }

    void Update(){
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f){
            audios.Stop();
            changeX = Input.GetAxisRaw("Horizontal");
            changeY = Input.GetAxisRaw("Vertical");

            if(Input.GetMouseButtonDown(0) && attacking != true){
                StartCoroutine(Attack());
            }else if(Mathf.Abs(changeX) == 1f){
                setDirection(changeX, "h");
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(changeX, 0f, 0f), .2f, whatStopsMovement)){
                    movePoint.position += new Vector3(changeX, 0f, 0f);
                    walkSound();
                    animator.SetFloat("moveY", 0f);
                    animator.SetFloat("moveX", changeX);
                    animator.SetBool("moving", true);
                }else{
                    //animator.SetBool("moving", false);
                }
            } else {
                
                animator.SetBool("moving", false);
                if(Mathf.Abs(changeY) == 1f){
                    setDirection(changeY, "v");
                    if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, changeY, 0f), .2f, whatStopsMovement)){
                        movePoint.position += new Vector3(0f, changeY, 0f);
                        walkSound();
                        animator.SetFloat("moveX", 0f);
                        animator.SetFloat("moveY", changeY);
                        animator.SetBool("moving", true);
                    }else{
                    //animator.SetBool("moving", false);
                    }
                }else{
                    
                    animator.SetBool("moving", false);
                }
            }
        }
    }

    public void setDirection(float value, string d){
        switch (value){
            case -1f:
                if(d == "h"){
                    direction = "left";
                }else{
                    direction = "down";
                }
                break;
            case 1f:
                if(d == "v"){
                    direction = "up";
                }else{
                    direction = "right";
                }
                break;

            default:
                direction = "down";
                break;
        }
        dl.setVisible(direction);
    }

    public void addGold(int goldWon){
        goldCollected += goldWon;
        gameController.updateScore(goldCollected);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Coin"){
            AudioSource.PlayClipAtPoint(coin, transform.position);
            addGold(other.gameObject.GetComponent<Collectable>().getAmount());
            Destroy(other.gameObject);
        }
        if(other.tag == "Rune"){
            //AudioSource.PlayClipAtPoint(coin, transform.position);
            hasRune = true;
            Destroy(other.gameObject);
        }
        if(other.tag == "Cake"){
            //AudioSource.PlayClipAtPoint(coin, transform.position);
            AddLife(other.gameObject.GetComponent<Collectable>().getAmount());
            Destroy(other.gameObject);
        }
    }

    IEnumerator Attack(){
        AudioSource.PlayClipAtPoint(hit, transform.position);
        animator.SetBool("attacking", true);
        attacking = true;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds (.7f);
        attacking = false;
    }

    public void TakeDamage(float damage){
        AudioSource.PlayClipAtPoint(dmg, transform.position);
        currentHealth -= damage;
        gameController.SetHealth(currentHealth);
        if(currentHealth <= 0){
            isAlive = false;
            gameController.restartGame();
            //gameObject.SetActive(false);
            //gameObject.GetComponentInChildren<Light>().enabled = false;
        }
    }

    public void AddLife(float extraLife){
        if(currentHealth < maxHealth){
            currentHealth += extraLife;
            if(currentHealth > maxHealth){
                currentHealth = maxHealth;
            }
        }
        gameController.SetHealth(currentHealth);
    }

    void walkSound(){
        if(!audios.isPlaying){
            audios.PlayDelayed(0.1f);
            //audios.Play();
        }
    }

}