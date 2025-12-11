using FeatureFlags.ClientSdk.Evaluation.Models;
using OpenFeature.Model;

namespace OpenFeature.Contrib.Providers.FeatureFlagger.Extensions;

public static class FlagContextExtensions
{
    public static FlagContext ToFlagContext(this EvaluationContext? evaluationContext)
    {
        var flagContext = new FlagContext();

        if (evaluationContext == null)
        {
            return flagContext;
        }

        // For now it works only with user id so no other properties are mapped
        flagContext.UserId = evaluationContext.TargetingKey;
        return flagContext;
    }
}
