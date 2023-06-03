using System.Text.Json.Serialization;

namespace N5NowChallengue.DataService.Models.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEmployeePermission
    {
        Approved,
        Requested,
        Disapproved
    }
}