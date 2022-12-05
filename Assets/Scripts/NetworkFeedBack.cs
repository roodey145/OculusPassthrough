using System;
using System.Collections.Generic;

enum NetworkFeedback : byte
{
    NONE = 0,

    #region Errors

    NOT_LOGGED_IND, // = 1
    ALREADY_LOGGED_IND, // = 2

    NAME, // = 3
    SHORT_NAME, // = 4
    MISSING_NAME, // = 5

    USERNAME,
    SHORT_USERNAME,
    MISSING_USERNAME,
    USERNAME_DOES_NOT_EXIST,
    USERNAME_ALREADY_EXISTS,

    EMAIL,
    MISSING_EMAIL,

    PASSWORD,
    SHORT_PASSWORD,
    MISSING_PASSWORD,
    INCORRECT_PASSWORD,

    INCORRECT_ARRAY, // Indicates that the array whcih was feed to the server does not match the required array.
    TIME_OUT,
    FILE_ALREADY_EXISTS,

    FILE_TYPE,
    FILE_LENGTH,
    FILE_NAME,

    DELETING_TOKEN_FAILED,
    REGISTERING_TOKEN_FAILED,
    TOKEN,

    REGISTERING_NEW_USER_FAILED,

    DELETE_MEETING_FAILED,
    REGISTERING_MEETING_FAILED,
    ASSOCIATING_MEETING_FAILED,
    FETCHING_MEETING_INFO_FAILED,
    REGISTERING_ATTENDER_FAILED,
    INCORRECT_MEETING_CODE,

    NOT_ATTENDER_RETRIVING_MODEL_INFO,
    RETRIVE_MODEL_INFO_FAILED,

    SERVER_ERROR,
    INCORRECT_REQUEST_METHOD,
    #endregion

    #region Success
    LOGIN_SUCCEEDED,
    SIGNUP_SUCCEEDED
    #endregion
}

internal class NetworkFeedBack
{
    internal List<NetworkFeedback> errors = new List<NetworkFeedback>();
    internal string rawFeedback;

    /// <summary>
    /// This constructor takes one arrgument which expect the
    /// feedback to be arranged in the following manner
    /// feedback1;feedback2;...;feedbackN;
    /// </summary>
    /// <param name="feedback">The string which contains the name of the errors which have occured.</param>
    internal NetworkFeedBack(string feedback)
    {
        rawFeedback = feedback;

        string[] errorsString = feedback.Split(";");

        NetworkFeedback error = NetworkFeedback.NONE;
        // Convert the errors string to NetworkErrors
        for(int i = 0; i < errorsString.Length; i++)
        {
            error = _GetErrorType(errorsString[i]);

            // Check if the error is not NONE
            if(error != NetworkFeedback.NONE)
            { // This error represent an actual error

                // Add this error to the error list
                errors.Add(error);
            }
        }
    }

    public static void main(string[] args)
    {
      new NetworkFeedBack(""). _GetErrorType("");
    }


    /// <summary>
    /// Take a string representation of an error and convert it 
    /// to a NetworkError which the string represent.
    /// </summary>
    /// <param name="error">The error string</param>
    private NetworkFeedback _GetErrorType(string errorString)
    {
        NetworkFeedback error = NetworkFeedback.NONE;
        foreach(byte errIndex in Enum.GetValues(typeof(NetworkFeedback)))
        {
            if(errorString.ToLower() == ((NetworkFeedback)errIndex).ToString().ToLower())
            { // The error which the string represent has been found
                error = (NetworkFeedback)errIndex;
                break;
            }
        }

        return error;
    }


}