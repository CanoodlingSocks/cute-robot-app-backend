using Service.DTO;

namespace project_nebula_backend.JWTAuthentication
{
    public interface ITokenService
    {
        public string CreateJWTToken(SuccessfulLoginDTO result);
    }
}
