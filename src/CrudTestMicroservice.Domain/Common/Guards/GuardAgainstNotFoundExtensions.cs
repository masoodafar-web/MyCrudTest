
namespace CrudTestMicroservice.Domain.Common.Guards
{
    public static partial class GuardClauseExtensions
    {
        public static T NotFound<T>(this IGuardClause guardClause, T? aggregate, string? message = null) where T : class
        {
            if (aggregate == null)
            {
                GuardClauseExtensions.NotFound(message ?? "Not found");
            }
            return aggregate!;
        }
    }
}
