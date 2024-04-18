using UnityEngine;
using UnityEngine.SceneManagement;
//Same as last time
public class SceneReloader : MonoBehaviour
{
    public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
