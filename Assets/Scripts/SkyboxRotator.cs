using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 2f;
    private Material _skybox;

    void Start()
    {
        _skybox = Instantiate(RenderSettings.skybox);
        RenderSettings.skybox = _skybox;
    }

    void Update()
    {
        RotateSkybox(Time.timeSinceLevelLoad * _rotationSpeed);
    }

    private void RotateSkybox(float angle)
    {
        _skybox.SetFloat("_Rotation", angle);
    }
}