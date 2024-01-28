using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogressBarScript : MonoBehaviour
{
    [Header("Progress Bar Settinngs")]
    public GameObject progressBar;
    [Range(0, 100)]
    public int progress;
    float calculatedWidth = 0 ;
    float calculatedPosX = 0;
    SpriteRenderer spriteRenderer;
    int calculatedProcess;
    void Start(){
        spriteRenderer = progressBar.GetComponent<SpriteRenderer>();
        progressBar.transform.localPosition = new Vector3(-0.015f, 0.0f, 0.0f);
        spriteRenderer.size = new Vector2(0.17f, 0.2f);
    }
    void Update()
    {
        gameObject.GetComponent<Renderer>().enabled = progress != 0;
        progressBar.GetComponent<Renderer>().enabled = progress != 0;
        if(calculatedProcess != progress){
            calculatedProcess = progress;
            calculatedWidth = ((0.14f)*(progress/100.0f))+0.03f;
            spriteRenderer.size = new Vector2(calculatedWidth, spriteRenderer.size.y);
            calculatedPosX = -0.015f-((0.17f-spriteRenderer.size.x)*0.5f);
            progressBar.transform.localPosition = new Vector3(calculatedPosX, 0.0f, 0.0f);
        }
    }
}
