using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Nadobe;
using Nadobe.Common.Interfaces;
using Nadobe.Common.Models;
using Nadobe.Common.Util;
using Newtonsoft.Json;

namespace Altinn.Dan.Plugin.Digdir
{
    public class Main
    {
        private ILogger _logger;
        private readonly EvidenceSourceMetadata _metadata;

        public Main(IEvidenceSourceMetadata metadata)
        {
            _metadata = (EvidenceSourceMetadata)metadata;
        }

        [Function("TestConsentWithAccessMethod")]
        public async Task<IActionResult> TestConsentWithAccessMethod([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }

        [Function("TestConsentWithRequirement")]
        public async Task<IActionResult> TestConsentWithRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }

        [Function("TestConsentWithSoftConsentRequirement")]
        public async Task<IActionResult> TestConsentWithSoftConsentRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }

        [Function("TestConsentWithConsentAndSoftLegalBasisRequirement")]
        public async Task<IActionResult> TestConsentWithConsentAndSoftLegalBasisRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }

        [Function("TestConsentWithMultipleConsentAndSoftLegalBasisRequirements")]
        public async Task<IActionResult> TestConsentWithMultipleConsentAndSoftLegalBasisRequirements([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }


        [Function(Constants.EvidenceSourceMetadataFunctionName)]
        public HttpResponseData Metadata(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData req,
            FunctionContext context)
        {
            _logger = context.GetLogger(context.FunctionDefinition.Name);
            _logger.LogInformation($"Running metadata for {Constants.EvidenceSourceMetadataFunctionName}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            response.WriteString(JsonConvert.SerializeObject(_metadata.GetEvidenceCodes(), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore
            }));

            return response;
        }

        private async Task<List<EvidenceValue>> GetEvidenceValues()
        {
            return await Task.FromResult(new List<EvidenceValue>
            {
                new()
                {
                    EvidenceValueName = "field1",
                    Value = "somevalue"
                }
            });
        }
    }
}
