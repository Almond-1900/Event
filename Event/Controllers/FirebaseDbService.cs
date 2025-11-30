using Firebase.Database;
using Firebase.Database.Query;
using Google.Apis.Auth.OAuth2;

public class FirebaseDbService
{
    private readonly FirebaseClient client;

    public FirebaseDbService()
    {
        client = new FirebaseClient("https://eventaspnet-default-rtdb.firebaseio.com/");
    }
}
