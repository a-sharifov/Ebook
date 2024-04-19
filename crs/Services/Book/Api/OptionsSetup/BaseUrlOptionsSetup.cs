using Infrastructure.FileStorages.Options;
using Microsoft.Extensions.Options;

namespace Api.OptionsSetup;

internal sealed class BaseUrlOptionsSetup
     : IConfigureOptions<BaseUrlOptions>
{
    public void Configure(BaseUrlOptions options) => 
        options.BaseUrl = $"127.0.0.1:9000";
}
