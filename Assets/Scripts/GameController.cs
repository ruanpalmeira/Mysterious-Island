using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private AudioSource wind;
    

    public Slider slider;
    public Text gameOverText;
    public Text scoreText, menuTitle;
    public Button playButton, ExitButton;
    bool gameOver = false;
    public Animator transition;
    public Image coinBag;
    public float transitionTime;

    void Start(){
        wind = GetComponent<AudioSource>();
        StartCoroutine("PlaySound");
        //gameOverText.enabled = false;
        if(SceneManager.GetActiveScene().buildIndex == 0){
            //AudioSource.PlayClipAtPoint(theme, transform.position);
            setGameStart(1);
        }else{
            setGameStart(2);
        }
    }         
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            quitGame();
        }
        /*
        if(gameOver){
            restartGame();
        }*/
    }

    IEnumerator PlaySound(){
        while(true){
            yield return new WaitForSeconds(Random.Range(5,10));
            wind.Play();
        }
    }

    public void setGameOver(int goldCollected){
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(Fade());
        //gameOver = true;
        slider.enabled = false;
        scoreText.enabled = false;
        StartCoroutine(LoadMenu(5.0f));
        //gameOverText.text = "x " + slimesKilled;
    }

    public void setGameStart(int value){
        if(value ==1){
            coinBag.gameObject.SetActive(false);
            slider.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
            menuTitle.enabled = true;
            playButton.gameObject.SetActive(true);
            ExitButton.gameObject.SetActive(true);
        }
        if(value == 2){
            coinBag.gameObject.SetActive(true);
            slider.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
            menuTitle.enabled = false;
            playButton.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(false);
        }
        

        //gameOverText.text = "x " + slimesKilled;
    }

    public void updateScore(int gold){
        scoreText.text = "x " + gold;
    }

    
    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void openMenu(){
        SceneManager.LoadScene(0);
    }

    public void quitGame(){
        Application.Quit();
    }

    public void SetMaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(float health){
        slider.value = health;
    }

    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex){
        transition.SetTrigger("start");
        menuTitle.enabled = false;
        playButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadMenu(float tTime){
        yield return new WaitForSeconds(tTime);
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }

    IEnumerator Fade(){
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        //SceneManager.LoadScene(levelIndex);
    }

    public int getLevel(){
        return SceneManager.GetActiveScene().buildIndex;
    }
    
}
