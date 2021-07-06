using UnityEngine; 
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))] 
internal class AutoScaleCamera : MonoBehaviour
{
    [SerializeField] private bool uniform = true;
    [SerializeField] private bool autoSetUniform = false;
    private Camera _camera;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;

        if (uniform)
            SetUniform();
    } 
    private void LateUpdate()
    {
        if (autoSetUniform && uniform)
            SetUniform();
    }
    private void SetUniform()
    {
        float orthographicSize = _camera.pixelHeight/2;
        if (orthographicSize != _camera.orthographicSize)
            _camera.orthographicSize = orthographicSize;
    }
}