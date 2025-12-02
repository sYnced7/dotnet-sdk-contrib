using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenFeature.Contrib.Providers.Gitlab.Extensions;
using OpenFeature.Contrib.Providers.Gitlab.Options;
using OpenFeature.Model;
using Unleash;
using Unleash.ClientFactory;

namespace OpenFeature.Contrib.Providers.Gitlab;

public sealed class GitlabProvider : FeatureProvider
{
    private readonly Metadata _metadata = new Metadata("Gitlab Provider");
    private readonly UnleashSettings _unleashSettings;
    private readonly UnleashClientFactory _unleashClientFactory;

    public GitlabProvider(IOptions<GitlabOptions> options)
    {
        var settings = options.Value;
        this._unleashClientFactory = new UnleashClientFactory();

        if (settings.isProxyActive)
        {
            this._unleashSettings = new UnleashSettings
            {
                UnleashApi = new Uri(settings.ApiUrl),
                CustomHttpHeaders =
                {
                    { "Authorization", settings.ApiKey }
                }
            };

            return;
        }

        this._unleashSettings = new UnleashSettings
        {
            UnleashApi = new Uri(settings.ApiUrl),
            InstanceTag = settings.InstanceId,
            AppName = settings.ProjectName,
        };
    }

    public override Metadata GetMetadata() => _metadata;

    public override async Task<ResolutionDetails<bool>> ResolveBooleanValueAsync(string flagKey,
        bool defaultValue,
        EvaluationContext context = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var client = await _unleashClientFactory.CreateClientAsync(_unleashSettings).ConfigureAwait(false);

        var unleashResult = client.IsEnabled(flagKey, context.ToUnleashContext(), defaultValue);

        return new ResolutionDetails<bool>(flagKey, unleashResult);
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
