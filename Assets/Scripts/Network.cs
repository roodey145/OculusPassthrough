using System.Collections;
using System.Collections.Generic;
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
        Login(username, password);
        //FetchFilesHeader();
        //StartCoroutine(_FetchFilesHeader());
        //StartCoroutine(_CreateMeeting("Admin", 2));
        //StartCoroutine(_FetchFile(2));
    }


    #region Login
    public void Login(string username, string password)
    {
        StartCoroutine(_Login(username, password));
    }


    // The strings are immutatable in c#
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
                print("Error in the internet connection");
            }
            else
            {
                NetworkFeedBack feedBack = new NetworkFeedBack(request.downloadHandler.text);

                // Handle the login feedback which the server has sent.
                _LoginHandler(feedBack);
            }
        }
    }

    /// <summary>
    /// Will handles any error that might have occured while trying to login.
    /// </summary>
    /// <param name="loggedIn"></param>
    private void _LoginHandler(NetworkFeedBack feedback)
    {


        if (feedback.errors.Count > 0)
        { // Some error(s) have occured while processing the login request.
            for (int i = 0; i < feedback.errors.Count; i++)
            {
                // Handle each error individually
                _HandleNetworkError(feedback.errors[i]);
                //print("FeedBack: " + feedBack.errors[i].ToString());
            }
        }
        else
        { // The request was processed successfully and the user is logged in now
            // Create user info to indicate that the user has logged in
            UserInfo.CreateInstance(true);

            // Fetch the user files header
            FetchFilesHeader();

            // TODO: Not implemented yet
            // Send the user to the main scene
        }

    }
    #endregion

    #region Sign up
    public void Signup(string name, string username, string password, string email = "")
    {
        StartCoroutine(_Signup(name, username, password, email));
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
                // Automaticlly login --> Will create a UserInfo instance.
                Login(username, password);
            }
        }
    }


    private void _SingupHandler(bool signedUp, NetworkFeedBack feedBack)
    {
        if (signedUp)
        { // The user has successfully signed up

            // Send the user to the login scene / OR just sign ind immeditely
            
        }
        else
        { // An error occured while signing up

        }
    }

    #endregion

    #region fetch files header

    public void FetchFilesHeader()
    {
        if(UserInfo.instance != null && UserInfo.instance.loggedIn)
        {
            StartCoroutine(_FetchFilesHeader());
        }
    }


    private IEnumerator _FetchFilesHeader()
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
                NetworkFeedBack feedback = new NetworkFeedBack(request.downloadHandler.text);
                _FetchFilesHeaderHandler(request.downloadHandler.text, feedback);
                //print(request.downloadHandler.text);
            }
        }
    }

    private void _FetchFilesHeaderHandler(string filesHeaderRawData, NetworkFeedBack feedback)
    {

        // Check if any error has occured during the file header fetaching process
        if(feedback.errors.Count > 0)
        { // Some errors have occured while fetching the files header
            for(int i = 0; i < feedback.errors.Count; i++)
            {
                // Handle all the errors individually 
                _HandleNetworkError(feedback.errors[i]);
            }
        }
        else
        { // The files header were fetched successfully.
            // Seprate the files headers using the sperater sign ";"
            string[] headers = filesHeaderRawData.Split(';');

            List<FileHeader> filesHeader = new List<FileHeader>();

            // Retrive a header info
            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i] != "")
                {
                    filesHeader.Add(new FileHeader(headers[i]));
                }
            }
        }
    }

    #endregion

    #region Fetch file info


    private IEnumerator _FetchFile(int fileId)
    {
        WWWForm form = new WWWForm();
        form.AddField("fileId", fileId);

        using (UnityWebRequest request = UnityWebRequest.Post(_mainCite + _filePath, form))
        {
            yield return request.SendWebRequest();

            print(request.downloadHandler.text);

            // Create feedback from the received message
            NetworkFeedBack feedback = new NetworkFeedBack(request.downloadHandler.text);

            //// Iterate through all the errors and print them
            //for(int i = 0; i < feedback.errors.Count; i++)
            //{
            //    print("FeedBack Error: " + feedback.errors[i]);
            //}

            if (request.error != null || feedback.errors.Count > 0)
            { // Error occured while fetching the file
                _HandleFetchFile(feedback);
            }
            else
            {
                UnityWebRequest www = new UnityWebRequest(_mainCite + request.downloadHandler.text);
                www.downloadHandler = new DownloadHandlerBuffer();
                yield return www.SendWebRequest();

                if (www.error != null)
                {
                    print(www.error);
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


    private void _HandleFetchFile(NetworkFeedBack feedback)
    {
        if(feedback.errors.Count > 0)
        { // Failed to fetch the file

            // Handle the error accordingly 
            for(int i = 0; i < feedback.errors.Count; i++)
            {
                // Handle each error individually.
                _HandleNetworkError(feedback.errors[i]);
            }

        }
    }

    #endregion


    #region Create meeting
    public void CreateMeeting(string meetingName, int fileId)
    {
        StartCoroutine(_CreateMeeting(meetingName, fileId));
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


    private void _CreateMeetingHandler(bool created, NetworkFeedBack feedBack)
    {
        if(created)
        { // The meeting has been created

            // Enable the meeting information in this scene
        }
        else
        { // Some errors have occured

            // Handle the errors which could have occured

        }
    }

    #endregion

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

    


    private void _HandleNetworkError(NetworkFeedback feedback)
    {
        switch (feedback)
        {
            case NetworkFeedback.NOT_LOGGED_IND: 
                
                break;
            #region Password Errors
            case NetworkFeedback.PASSWORD: break;
            case NetworkFeedback.INCORRECT_PASSWORD:
                print("Incorrect Password");
                break;
            case NetworkFeedback.SHORT_PASSWORD: break;
            case NetworkFeedback.MISSING_PASSWORD:
                print("Missing Password");
                break;
            #endregion

            #region Username Errors
            case NetworkFeedback.USERNAME: break;
            case NetworkFeedback.USERNAME_ALREADY_EXISTS: break;
            case NetworkFeedback.USERNAME_DOES_NOT_EXIST: 
                print("Username Does Not Exist");
                break;
            case NetworkFeedback.SHORT_USERNAME: break;
            case NetworkFeedback.MISSING_USERNAME:
                print("Missing Username");
                break;
            #endregion
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
