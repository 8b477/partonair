using ApplicationLayer.partonair.Interfaces;

using BCrypt.Net;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;


namespace ApplicationLayer.partonair.Services
{
    public class BCryptService : IBCryptService
    {
        public string HashPass(string passToHash, int workFactor)
        {
            try
            {
                string passwordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(passToHash, workFactor);

                return passwordHashed;
            }
            catch (SaltParseException ex)
            {
                throw new ApplicationLayerException(ApplicationLayerErrorType.SaltParseBCryptException, $"{ex.Message}");
            }
        }

        public bool VerifyPasswordMatch(string actualPass, string passHashed)
        {
            try
            {
                bool check = BCrypt.Net.BCrypt.EnhancedVerify(actualPass, passHashed);

                return check;
            }
            catch (SaltParseException)
            {
                throw new ApplicationLayerException(ApplicationLayerErrorType.SaltParseBCryptException);
            }         
        }
        //e118f45e-a2db-47e1-846c-7a3250da8020
    }
}
