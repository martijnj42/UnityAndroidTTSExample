using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleUsage : MonoBehaviour
{
    private AndroidTTS androidTTS;
    public string engineName = "com.google.android.tts";
    public InputField inputField;

    class InitCallback : AndroidTTS.IOnInitialisationCallback {
        public ExampleUsage main;

        public InitCallback(ExampleUsage main) {
            this.main = main;
	    }

        public void onSuccess(int languageSetResultCode){
	        if (languageSetResultCode < 0) {
                Debug.Log("Failed to set the language, result code: " + languageSetResultCode.ToString());
	        }

            // Example
            if(main.androidTTS.SetLanguage("zh", "TW") < 0) {
                Debug.LogError("Required language not available");
	        }
        }

        public void onFailure() {}
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartInitialisation());
        inputField.onSubmit.AddListener(text => TextToSpeech(text));
    }

    private IEnumerator StartInitialisation() {
        yield return new WaitForEndOfFrame();
        if (engineName.Length > 0)
        {
            androidTTS = new AndroidTTS(new InitCallback(this), engineName);
        }
        else
        {
            androidTTS = new AndroidTTS(new InitCallback(this));
        }
    }

    public void TextToSpeech(string text) {
        Debug.Log("Speak: " + text);
        androidTTS.Speak(text);
    }

    private void OnDestroy()
    {
        // Call cleanup method
        if (androidTTS != null) {
            androidTTS.Dispose();
	    }
    }

}
