namespace OpenFeature.Contrib.Providers.Gitlab.Options;

public class GitlabOptions
{
    public string ApiUrl { get; set; }
    public string InstanceId { get; set; } //Optional: If no proxy, no need to setup instance id
    public string ProjectName { get; set; } // Optional: If no proxy, no need to setup project name
    public bool isProxyActive { get; set; }
    public string ApiKey { get; set; }
}
