using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace In_Anh.Models
{
    public static class FirebaseHelper
    {
        static string filecf = @"{
  ""type"": ""service_account"",
  ""project_id"": ""jin-nie"",
  ""private_key_id"": ""eb219ce041e55c37a5bba2819cb1554baf7c5289"",
  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQCZfkBvJJzHiGCU\nh26t0qFeGpY6pJqdgCgtyBn3619r0dCCzp8YCw8/rn/ImzZtB33bCWSi0WUzsY0t\neYagSmnWvEGO6DvKmNF6dtuqsTjUqDoroezVTehqCup/Ykdk+K+S1oO/J//SPX4p\nfO9fcikn/JcsdKoO46al058oz7w48zGLx/GnIQCj3uQWJhSv4t5mT9lB1pBtupO2\n7lPiijbjhXgGdU+HSPpGdKkZR62qgWrBrsDhmaaUHI85ic2McHcGwULxZpt9UOHh\nMN0pkI8thuMraUgue4A0j3ZH3eppaHVP9JvSK8wwGiW0ykvyZbH7fLmgOxhWNP1x\nKiOQm7NVAgMBAAECggEAA7+RR8A2DZUElMP3kt+Z/gq6AJ3HQnw1dE7BZuw/dN+C\nDxuyx2XHviH0WrwC7ifYwzSoGZDG/vuW62rJu+2CnFJAa0K7vmS7zYOMYDn6iGOW\nThKizl+xbVuDDVAjVwbP/3mPwecyaFovk/L02S4YGAhN/HGEkJ9Zu7gerXgLNucc\naI+2aqP1MqARiH+98mL0E2NoiUpUBw3u1UPGXeEvYESbUGQruLUr+Udo2idzvFNy\nRu3/gcwA3k5RcEl3a3Mzs/bGjzAqzmIhDJ1FkGWT7b1GXELZlHJdd/G+aMJJ8ajz\nMgYSqMpwpr90LklG/7oTrBDJHYSNCIkvhkT5dpeSyQKBgQDWQUWvDwlTGXfajg7c\n9VknCKaiIuaX31yK0D2SjQSxwUw8taDtLyAlBfYiA9wxtcj+TPblKmqFHKixsuZd\nBfsgFsris5BaZHWQNrq1EhJpsQPk1wLd9m/nkLDtgJ0ahR0aHjnm/OJIDnpdhhEk\n5SWhpJqypautWERkFcJokRp9qQKBgQC3ZkTW88ud7nDE87ZD0y//vuQ9GdBQocgr\n+vl9EuQATyJ26fPCCEAvP+0oawrOmLqDZzKcGSgsOkdYLxAmyNJUth09+ezPL3Km\nf4ctTa6/lQhh/caP3kpyX7g/aQvHs7y/9Xm1FOGh6CfWg4IDvYMhEn6BFcemALDV\nKXjBZipbzQKBgQCaLS4v4uoa2mDc7QzVA9i7tgXy9ppo/1pgqaklJEUUhLyPk0PS\nae7/tWKA57Y0L1QLaubf0b4FO7JI7SvIFN25Ia2tpPljkpbmx/tjATYuyCq9Kdvu\nTcAJKp2myr5CrzdZ7BZOmftbTs/PLQJ5QkvqHcEUfRMlEjdkriPPi1s8wQKBgQCH\n56Q5ub340boFaSh2yGf4V8ggsyFYrxp75Oa+1aHZzMqYXjQBZEXT/cZLvk7q2+jS\n+UVawZWweJtn8LBCXWzn03CF/c+LlPicA0Lzwm4tkE3+96UN9ccrTF1nt/s/yKSl\nh03ib4/of/YNk7Rj+yrR9jeChtZE4Jwpyiqu/nuDPQKBgQCCyEgMuQMCMWRV6SQe\nEMOnGcggDGEp4s0TUtrRXD6LAXjcFYtI9yklQ0TpcxZM+6Tg6ZNFa0BHvwuJGbez\nNflNBVdQC1uS3IKLEa3hVO+pUI5zKgSjqjq7/rTtpL4tFaRWkBoUBJZHtz0BCPIH\n6+RUOq2ecZux1YUPqBrMZq1r/A==\n-----END PRIVATE KEY-----\n"",
  ""client_email"": ""firebase-adminsdk-3sj0n@jin-nie.iam.gserviceaccount.com"",
  ""client_id"": ""106430380984937396884"",
  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
  ""token_uri"": ""https://oauth2.googleapis.com/token"",
  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-3sj0n%40jin-nie.iam.gserviceaccount.com"",
  ""universe_domain"": ""googleapis.com""
}
";
        static string filePath = "";
        public static FirestoreDb? Database {  get; private set; }
        public static void SetEnviromentVar()
        {
            filePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
            File.WriteAllText(filePath, filecf);    
            File.SetAttributes(filePath, FileAttributes.Hidden);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",filePath);
            Database = FirestoreDb.Create("jin-nie");




            if (FirebaseApp.GetInstance("jin-nie") == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(filePath),
                },"jin-nie");
            }
            
            File.Delete(filePath);
        }


    }
}
