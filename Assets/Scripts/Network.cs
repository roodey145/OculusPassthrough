using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{
    private string _mainCite = "https://linguatopmaster.com/P3/php/";
    private string _loginPath = "login.php";
    private string _signupPath = "signup.php";
    private string _filesHeaderPath = "filesHeader.php";
    private string _filePath = "fetchFile.php";
    private string _createMeetingPath = "createMeeting.php";
    private string _joinMeetingPath = "joinMeeting.php";

    private void Start()
    {
        string username = "Roodey145";
        string password = "Roodey145";
        StartCoroutine(_Login(username, password));
        StartCoroutine(_Fetch_Files_Header());
        StartCoroutine(_CreateMeeting("Admin", 2));
        StartCoroutine(_Fetch_File(2));
    }

    // The strings are immutable in c#
    private IEnumerator _Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);


        using (UnityWebRequest request = UnityWebRequest.Post(_mainCite + _loginPath, form))
        {
            yield return request.SendWebRequest();

            if (request.error != null)
            {
                print("Error");
            }
            else
            {
                print(request.downloadHandler.text);
            }
        }
    }


    private IEnumerator _Signup(string name, string username, string password, string email = "")
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("email", email);

        using (UnityWebRequest request = UnityWebRequest.Post(_mainCite + _signupPath, form))
        {
            yield return request.SendWebRequest();

            if (request.error != null)
            {
                print("Error");
            }
            else
            {
                print(request.downloadHandler.text);
            }
        }
    }

    private IEnumerator _Fetch_Files_Header()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest request = UnityWebRequest.Post(_mainCite + _filesHeaderPath, form))
        {

            yield return request.SendWebRequest();

            if (request.error != null)
            {
                print("Error");
            }
            else
            {
                print(request.downloadHandler.text);
            }
        }
    }


    private IEnumerator _Fetch_File(int fileId)
    {
        WWWForm form = new WWWForm();
        form.AddField("fileId", fileId);

        using (UnityWebRequest request = UnityWebRequest.Post(_mainCite + _filePath, form))
        {
            yield return request.SendWebRequest();

            print(request.downloadHandler.text);

            if (request.error != null)
            {
                print("Error");
            }
            else
            {
                UnityWebRequest www = new UnityWebRequest(_mainCite + request.downloadHandler.text);
                www.downloadHandler = new DownloadHandlerBuffer();
                yield return www.SendWebRequest();

                if (www.error != null)
                {
                    print("Error");
                }
                else
                {
                    print(Application.persistentDataPath);
                    using (Stream file = File.Open(Application.persistentDataPath + fileId + "roodey.fbx", FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(file, Encoding.UTF8, false))
                        {
                            bw.Write(www.downloadHandler.data);
                            bw.Close();
                        }
                    }
                }


                //StreamWriter sw = new StreamWriter("D:\\file.fbx");                
                //print(request.downloadHandler.text);
            }
        }
    }


    private IEnumerator _CreateMeeting(string meetingName, int fileId)
    {
        WWWForm form = new WWWForm();
        form.AddField("meetingName", meetingName);
        form.AddField("fileId", fileId);

        using (UnityWebRequest request = UnityWebRequest.Post(_mainCite + _createMeetingPath, form))
        {
            yield return request.SendWebRequest();

            // Check if any error occured
            if (request.error != null)
            { // An error has occured

            }
            else
            {
                // Print the meeting code
                print(request.downloadHandler.text);
                // TODO: The file which is associated with the meeting should be imported

            }
        }
    }

    private IEnumerator _JoinMeeting(string meetinCode)
    {
        WWWForm form = new WWWForm();
        form.AddField("meetingCode", meetinCode);

        using (UnityWebRequest request = UnityWebRequest.Post(_mainCite + _joinMeetingPath, form))
        {
            yield return request.SendWebRequest();

            if (request.error != null)
            {
                print("Error");
            }
            else
            {
                print(request.downloadHandler.text);
            }

        }
    }


    private UnityWebRequest _CreateRequest(string path, object data)
    {
        // Make sure the sent data is not null
        if (data == null || path == "")
            return null;

        UnityWebRequest request = new UnityWebRequest(path, "POST");

        // Encode the form
        var form = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
        request.uploadHandler = new UploadHandlerRaw(form);

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

}
