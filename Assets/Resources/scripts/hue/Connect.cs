using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using Hue.Persistence;

namespace Hue {
	public class Connect : MonoBehaviour {
        public Sprite bridge;
        public Sprite pressBridge;

		private string persistencePath;

		private Image image;

		private BridgeInfo bridgeInfo = null;
		private string username = null;
		
		void Start () {
			persistencePath = Application.persistentDataPath + "/hue.data";
				
			image = GetComponent<Image> ();

			StartCoroutine(Initialize());
		}

        IEnumerator Initialize()
        {
            yield return StartCoroutine(GetBridge());
            RequestUserId();
        }

        private void SetRegistered(bool registered)
        {
            if(registered)
            {
                image.sprite = bridge;
            } else
            {
                image.sprite = pressBridge;
            }

            Color color = image.color;
            image.color = new Color(color.r, color.g, color.b, .75f);

            image.enabled = true;

            Persistent.Lighting.SetUsername(username);
            Persistent.Lighting.SetHueIpaddress(bridgeInfo.internalipaddress);
        }

        public void RequestUserId() {
			if (bridgeInfo == null)
				return;
			
			if (File.Exists (persistencePath)) {
				using (FileStream file = File.OpenRead (persistencePath)) {
					Data data = (Data) new BinaryFormatter ().Deserialize (file);
					username = data.username;

                    SetRegistered(true);
				}
			} else {
				StartCoroutine (GetUser ());
			}
		}

		private IEnumerator GetUser() {
            SetRegistered(false);

            while (username == null)
            {
                using (UnityWebRequest request = new UnityWebRequest("http://" + bridgeInfo.internalipaddress + "/api"))
                {
                    request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes("{\"devicetype\":\"unity-test\"}"));
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.method = UnityWebRequest.kHttpVerbPOST;

                    yield return request.SendWebRequest();

                    NewUserAccepted accepted = JsonUtility.FromJson<NewUserAccepted>("{\"items\": " + request.downloadHandler.text + "}");
                    string username = accepted.items.ToArray()[0].success.username;
                    if (username != null)
                    {
                        using (FileStream file = File.Create(persistencePath))
                        {
                            Data data = new Data();
                            data.username = username;
                            new BinaryFormatter().Serialize(file, data);

                            this.username = username;
                            SetRegistered(true);
                        }
                    }
                }

                yield return new WaitForSeconds(5);
            }
		}

		private IEnumerator GetBridge() {
			using (UnityWebRequest request = UnityWebRequest.Get ("https://discovery.meethue.com")) {
				yield return request.SendWebRequest ();

				Bridges bridges = JsonUtility.FromJson<Bridges> ("{\"connected\":" + request.downloadHandler.text + "}");
				if (bridges.connected.Count > 0) {
					bridgeInfo = bridges.connected.ToArray () [0];
				}
			}
		}
	}
}