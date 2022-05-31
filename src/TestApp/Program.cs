using Vonage;
using Vonage.Request;
using Microsoft.Extensions.Logging;
using Vonage.Verify;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Vonage.Request.ApiRequest", LogLevel.Debug)
        .AddConsole();
});
Vonage.Logger.LogProvider.SetLogFactory(loggerFactory);



var credentials = Credentials.FromApiKeyAndSecret("", "");
var vonageClient = new VonageClient(credentials);

// request
var verifyRequest = new VerifyRequest
{
    SenderId = "Matt Test",
    Brand = "Vonage",
    CodeLength = 6,
    WorkflowId = VerifyRequest.Workflow.SMS_SMS_TTS,
    Number = "44123456789",//obfuscated my number 
    NextEventWait = 900
};

VerifyResponse verifyResponse;
verifyResponse = await vonageClient.VerifyClient.VerifyRequestAsync(verifyRequest);

Console.Write("Enter code:");
var code = Console.ReadLine();

// check
var checkRequest = new VerifyCheckRequest
{
    Code = code,
    RequestId = verifyResponse.RequestId
};

var checkResponse = await vonageClient.VerifyClient.VerifyCheckAsync(checkRequest);



// var insightRequest = new AdvancedNumberInsightRequest();
// insightRequest.Number = "447866572428";
// insightRequest.Country = "GB";
// insightRequest.RealTimeData = true;
//
// var insightResponse = await vonageClient.NumberInsightClient.GetNumberInsightAdvancedAsync(insightRequest);

Console.ReadLine();