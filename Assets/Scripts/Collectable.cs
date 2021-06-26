using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int reward = 1;

    public int getAmount(){
        return reward;
    }
   }
