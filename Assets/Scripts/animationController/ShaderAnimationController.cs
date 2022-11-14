using System.Collections;
using System.Collections.Generic;
using System;
using Random=UnityEngine.Random;
using UnityEngine;


public class ShaderAnimationController : MonoBehaviour, AnimationController
{

    // animation delay
    public float animationDelay = 1.0F;
    public string propertyAnimationValue;

    // shader animtion
    public Renderer meshRenderer;
    public Material specificMaterial;
    public float intervalDelay;
    public float dissolvePower; // amount value for the burn animation
    private float dissolveAmount = 0;

    // particle animation
    public GameObject fireParticle;
    public int fireNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<Renderer>();
        if (specificMaterial == null) {
            if (meshRenderer.materials.Length < 2) {
                specificMaterial = meshRenderer.material;
            }
        }
    }

    public void startAnimation() {
        Debug.Log("calling function");
        StartCoroutine("startShaderAnimation");
    }

    private IEnumerator startShaderAnimation() {
        specificMaterial.SetFloat(propertyAnimationValue,0);
        dissolveAmount = specificMaterial.GetFloat(propertyAnimationValue);
        Debug.Log("test");
        Debug.Log(dissolveAmount);
        Debug.Log(animationDelay);
        if(fireParticle != null) {
            instantiateFireParticle();
        }
        while(dissolveAmount < animationDelay) {
            yield return new WaitForSeconds(intervalDelay);
            stepBurnAnimation();
        }
        foreach (Transform child in gameObject.transform) {
            Destroy(child.gameObject);
        }
    }

    void instantiateFireParticle() {
        for (int i = 0; i < fireNumber; i++) {
            Vector3 fireSpawnCoord = new Vector3(0,0,0);
            float newX = Random.Range(-1.0f, 1f);
            float newY = Random.Range(-0.3f, 0.3f);
            float newZ = Random.Range(-1.0f, 1f);
            fireSpawnCoord.Set(transform.position.x + newX, transform.position.y + newY, transform.position.z + newZ);
            Instantiate(fireParticle, fireSpawnCoord, fireParticle.transform.rotation, transform);
        }
    }

    void stepBurnAnimation() {
        dissolveAmount = specificMaterial.GetFloat(propertyAnimationValue);
        specificMaterial.SetFloat(propertyAnimationValue,dissolveAmount+dissolvePower);
    }

    
}
