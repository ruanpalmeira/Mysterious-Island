using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetector : MonoBehaviour{
    public bool found = false;
    private GameObject my_object;
    private AudioSource itemFound;

    // Start is called before the first frame update
    void Start(){
        itemFound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(found){
                found = false;
                itemFound.Play();
                //Debug.Log("You got a coin!" + my_object.name);
                my_object.GetComponent<Spawner>().setSpawn();
                
                Destroy(my_object.gameObject, 0.5f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other){
            if(other.transform.tag == "CoinTile"){
                if(other.GetComponent<Spawner>().isRunestone){
                    if (gameObject.GetComponentInParent<PlayerController>().hasRune){
                        my_object = other.gameObject;
                        found = true;
                    }
                    
                }else{
                    my_object = other.gameObject;
                    found = true;
                }
            }
    }

    private void OnTriggerExit2D(Collider2D other) {
        found = false;
        my_object = null;
    }
    
    public void setVisible(string direction){
        switch(direction){
            case "left":
                transform.localPosition = Vector2.left;
                break;
            case "right":
                transform.localPosition = Vector2.right;   
                break; 
            case "up":
                transform.localPosition = Vector2.up;
                break;
            case "down":
                transform.localPosition = Vector2.down;
                break;
            default:
            break;
        }
    }    
}
