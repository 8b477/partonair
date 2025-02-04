
namespace ApplicationLayer.partonair.Interfaces
{
    public interface IBCryptService
    {
        string HashPass(string passToHash, int workFactor = 13);
        bool VerifyPasswordMatch(string actualPass, string passHashed);
    }
}
