using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using FeatureFlags.ClientSdk;

using Microsoft.Extensions.Options;

using OpenFeature.Contrib.Providers.FeatureFlagger.Extensions;
using OpenFeature.Contrib.Providers.FeatureFlagger.Options;
using OpenFeature.Model;

namespace OpenFeature.Contrib.Providers.FeatureFlagger.Providers;

public sealed class FeatureFlaggerProvider : FeatureProvider
{
    private readonly Metadata _metadata = new Metadata("Feature Flagger Provider");
    private readonly FeaturesClient _featuresClient;

    public FeatureFlaggerProvider(IOptions<FlaggerOptions> options, HttpClient httpClient)
    {
        var settings = new FeaturesClientSettings()
        {
            AppName = options.Value.AppName,
            ProjectId = options.Value.ApplicationId,
            InstanceId = options.Value.InstanceId,
        };

        this._featuresClient = new FeaturesClient(settings, httpClient);
    }

    public override Metadata GetMetadata() => _metadata;

    public override async Task<ResolutionDetails<bool>> ResolveBooleanValueAsync(string flagKey,
        bool defaultValue,
        EvaluationContext context = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _featuresClient.IsEnabledAsync(flagKey, context.ToFlagContext()).ConfigureAwait(false);

        return new ResolutionDetails<bool>(flagKey, result);
    }

    public override Task<ResolutionDetails<string>> ResolveStringValueAsync(string flagKey, string defaultValue, EvaluationContext context = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public override Task<ResolutionDetails<int>> ResolveIntegerValueAsync(string flagKey, int defaultValue, EvaluationContext context = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public override Task<ResolutionDetails<double>> ResolveDoubleValueAsync(string flagKey, double defaultValue, EvaluationContext context = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public override Task<ResolutionDetails<Value>> ResolveStructureValueAsync(string flagKey, Value defaultValue, EvaluationContext context = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }
}
