using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    private string sceneToLoadName;
    private bool isfirstUpadate = true;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        sceneToLoadName = Loader.sceneToLoad;
        //Invoke("startIt",0.1f);
        //startIt();
    }
    private void Update()
    {
        if (isfirstUpadate)
        {
            startIt();
            isfirstUpadate = false;
        }
    }

    public void startIt()
    {
        StartCoroutine(LoadAsync(sceneToLoadName));

    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            slider.value = operation.progress / 0.9f;
            yield return null;
        }
        
    }
}
