namespace WordMix.Client.Clients;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Api.Contracts.TemplateEntity;
using Byndyusoft.ApiClient;
using Microsoft.Extensions.Options;
using Settings;

/// <inheritdoc cref="ITemplateClient" />
public class TemplateClient : BaseClient, ITemplateClient
{
    private const string ApiPrefix = "api/v1/templates";

    public TemplateClient(
        HttpClient httpClient,
        IOptions<TemplateApiSettings> apiSettings) : base(httpClient, apiSettings)
    {
    }

    public async Task<TemplateDto> GetTemplate(int templateId, CancellationToken cancellationToken)
    {
        var templateDto = await GetAsync<TemplateDto>($"{ApiPrefix}/{templateId}", cancellationToken);
        return templateDto;
    }
}