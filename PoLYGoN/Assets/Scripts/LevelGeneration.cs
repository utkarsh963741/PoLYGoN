using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] objects;
    public int mapwidth=40;
    public int maplength =32;
    public float footprint=25;
    public float randomness=15;
    public bool identity = true;

    // Start is called before the first frame update
    void Start()
    {   
        for(int l=0; l<maplength;l++)
        {
            for(int w=0 ;w<mapwidth;w++)
            {
                int choice = Random.Range(0,20);
                int result = Random.Range(0,8);
               
                Vector3 pos = new Vector3(w*footprint,0,l*footprint);
                
                if(choice>randomness && identity)
                    Instantiate(objects[result], transform.position+pos, Quaternion.identity);
                else if(choice>randomness && !identity)
                    Instantiate(objects[result], transform.position+pos,new  Quaternion(0,0,0,1));
                
                
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
