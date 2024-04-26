using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiCoreExamenCubos.Helpers
{
    public class HelperActionServicesOAuth
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }



        public HelperActionServicesOAuth(IConfiguration configuration)
        {
            var KeyVault = configuration.GetValue<string>("KeyVault:VaultUri");
            var secretClient = new SecretClient(new Uri(KeyVault), new DefaultAzureCredential());

            Issuer = GetSecretsToken(secretClient, "secretoIssuer");
            Audience = GetSecretsToken(secretClient, "secretoAudience");
            SecretKey = GetSecretsToken(secretClient, "secretoSecretKey");

        }

        private string GetSecretsToken(SecretClient secretClient, string secretName)
        {
            KeyVaultSecret secret = secretClient.GetSecret(secretName);
            return secret.Value;
        }

        public SymmetricSecurityKey GetKeyToken()
        {
            byte[] data = Encoding.UTF8.GetBytes(this.SecretKey);
            return new SymmetricSecurityKey(data);
        }

        public Action<JwtBearerOptions> GetJwtBearerOptions()
        {
            Action<JwtBearerOptions> options = new Action<JwtBearerOptions>(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = this.Issuer,
                    ValidAudience = this.Audience,
                    IssuerSigningKey = this.GetKeyToken()
                };
            });
            return options;
        }

        public Action<AuthenticationOptions> GetAuthenticateSchema()
        {
            Action<AuthenticationOptions> options = new Action<AuthenticationOptions>(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            return options;
        }
    }
}
