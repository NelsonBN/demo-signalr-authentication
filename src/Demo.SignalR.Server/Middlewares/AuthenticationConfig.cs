namespace Demo.SignalR.Server.Middlewares
{
    public class AuthenticationConfig
    {
        public string JWT_SALT { get; set; }
        public string JWT_AUDIENCE { get; set; }
        public string JWT_ISSUER { get; set; }
        public int JWT_VALIDATION_HOURS { get; set; }

        public AuthenticationConfig()
        {
            this.JWT_VALIDATION_HOURS = 3600;
        }
    }
}