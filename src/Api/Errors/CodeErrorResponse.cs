using Newtonsoft.Json;

namespace Ecommerce.Api.Errors;


public class CodeErrorResponse
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string[]? Message { get; set; }

    public CodeErrorResponse(int statusCode, string[] message = null)
    {
        StatusCode = statusCode;
        if (message is null)
        {
            Message = new string[0];
            var text = GetdefaultMessageStatusCode(statusCode);
            Message[0] = text;

        }
        else
        {
            Message = message;
        }
    }


    private string GetdefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "El request enviado tiene errores",
            401 => "Sin autorización para este recurso",
            404 => "No se encontro el recurso encontrado",
            500 => "Se produjeron errores en el servidor",
            _ => string.Empty
        };
    }
}