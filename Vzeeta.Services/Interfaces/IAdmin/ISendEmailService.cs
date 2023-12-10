namespace Vzeeta.Services.Interfaces.IAdmin
{
    public interface ISendEmailService
    {
        public Task sendEmail(string mailTo, string body);
    }
}
