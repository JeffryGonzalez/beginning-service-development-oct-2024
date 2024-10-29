
namespace HelpDesk.Api.Status;

public interface ILookupEmergencyContacts
{
    Task<EmergencyContactInfo> GetCurrentContactAsync();
}
