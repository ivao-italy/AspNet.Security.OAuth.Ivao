namespace AspNet.Security.OAuth.Ivao;

/// <summary>
/// Contains constants specific to the <see cref="GitLabAuthenticationHandler"/>.
/// </summary>
public static class IvaoAuthenticationConstants
{
    public static class Claims
    {
        public const string Division = "ivao:usr:division";
        public const string Country = "ivao:usr:country";
        public const string RatingAtc = "ivao:usr:rating-atc";
        public const string RatingPilot = "ivao:usr:rating-pilot";
        public const string HoursAtc = "ivao:usr:hours-atc";
        public const string HoursPilot = "ivao:usr:hours-pilot";
        public const string IsNpoMember = "ivao:usr:npo-member";
        public const string IsVaStaff = "ivao:usr:va-staff";
        public const string VaIds = "ivao:usr:va-ids";
        public const string VaIcaos = "ivao:usr:va-icaos";
        public const string StaffPositions = "ivao:usr:staffpositions";
        public const string VaMemberIds = "ivao:usr:va-member-ids";
    }
}
