namespace Shopping.Web.Models.Ordering
{
    public record AddressModel(
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string City,
    string State,
    string ZipCode,
    string Country
    );
}
