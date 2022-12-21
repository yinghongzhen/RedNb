using RedNb.Core.Extensions;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using RedNb.Core.Domain.Shared;

namespace RedNb.Core.Util
{
    /// <summary>
    /// .Net加密解密帮助类
    /// </summary>
    public class JwtHelper
    {

        public const string Secret = "E8E4B29BD3F3C67D5B6808D5A1AA4433";

        public static string Decode(string token)
        {
            try
            {
                token = NetCryptoHelper.DecryptAes(token, NetCryptoHelper.AesKey);

                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var data = decoder.Decode(token, Secret, true);

                return data;
            }
            catch (TokenExpiredException)
            {
                throw new UserFriendlyException(EErrorCode.Timeout.GetDescription(), ((int)EErrorCode.Timeout).ToString());
            }
            catch (SignatureVerificationException)
            {
                throw new UserFriendlyException(EErrorCode.NotLogin.GetDescription(), ((int)EErrorCode.NotLogin).ToString());
            }
        }

        public static string CreateToken(Dictionary<string, object> payload)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, Secret);

            token = NetCryptoHelper.EncryptAes(token, NetCryptoHelper.AesKey);

            return token;
        }

    }
}