using ApplicationLayer.partonair.Interfaces;


namespace ApplicationLayer.partonair.Services
{
    public class BCryptService : IBCryptService
    {
        public string HashPass(string passToHash, int workFactor)
        {
            string passwordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(passToHash, workFactor);

            return passwordHashed;
        }

        public bool VerifyPasswordMatch(string actualPass, string passHashed)
        {
            bool check = BCrypt.Net.BCrypt.EnhancedVerify(actualPass, passHashed);

            return check;
        }
    }
}
