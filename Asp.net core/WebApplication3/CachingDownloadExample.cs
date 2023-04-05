using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;

public class CachingDownloadExample : MonoBehaviour
{
    public string serverPath; //서버의 경로
    public string localPath;
    public string[] bundleName;

    private void Start()
    {
        StartCoroutine(InstantiateObject());
    }
    /* 오브젝트를 번들로부터 생성하는 코루틴 함수 */
    IEnumerator InstantiateObject()
    {
        if (!Directory.Exists(localPath)) //폴더가 존재하지 않으면
        {
            Directory.CreateDirectory(localPath); //폴더 생성
        }

        foreach (string name in bundleName) //모든 에셋을 탐색하면서
        {
            if (!File.Exists(localPath + name)) //번들이 로컬에 존재하지 않으면 => 로컬에 번들 다운로드
            {
                UnityWebRequest request = UnityWebRequest.Get(serverPath + "/" + name); //서버로부터 번들 요청 생성

                yield return request.SendWebRequest(); //요청이 완료될 때까지 대기
                File.WriteAllBytes(localPath + "/" + name, request.downloadHandler.data); //파일 입출력으로 서버의 번들을 로컬에 저장
            }
            var bundle = AssetBundle.LoadFromFile(localPath + "/" + name); //로컬로부터 번들 로드

            GameObject[] assets = bundle.LoadAllAssets<GameObject>(); //번들에서 모든 에셋 로드
            foreach (GameObject clone in assets) //모든 에셋을 탐색하면서
            {
                Instantiate(clone); //오브젝트 생성
            }

            bundle.Unload(false); //true이면 에셋번들 언로드
        }
    }
}



