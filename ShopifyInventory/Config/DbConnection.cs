namespace ShopifyInventory.Config
{
    public static class DbConnection
    {
        public static string GetHerokuConnectionString()
        {
            string connectionUrl = Environment.GetEnvironmentVariable("URI")!;

            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
    }
}
