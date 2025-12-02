using OpenFeature.Model;
using Unleash;

namespace OpenFeature.Contrib.Providers.Gitlab.Extensions;

public static class UnleashContextExtensions
{
    public static UnleashContext ToUnleashContext(this EvaluationContext? evaluationContext)
    {
        var unleashContext = new UnleashContext();

        if (evaluationContext == null)
        {
            return unleashContext;
        }

        // For now it works only with user id so no other properties are mapped
        unleashContext.UserId = evaluationContext.TargetingKey;
        return unleashContext;
    }
}
