using Galaxy.Serialization;
using MediatR; 
using Galaxy.Gateway.Shared.Commands;
using System; 
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Gateway.CommandHandlers
{
    public class ValidateRequestCommandHandler : IRequestHandler<ValidateRequestCommand,bool>
    {

        private readonly ISerializer _serializer;
        public ValidateRequestCommandHandler(ISerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task<bool> Handle(ValidateRequestCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.ContentType) 
                && request.ContentType.Contains("json"))
            {
                this.AssertJson(request.Body);
            } 
            return true;
        }

        private void AssertJson(string jsonString)
        { 
            if ((jsonString.StartsWith("{") && jsonString.EndsWith("}")) || 
                 (jsonString.StartsWith("[") && jsonString.EndsWith("]"))) 
            { 
                try
                { 
                    object obj = this._serializer.Deserialize<object>(jsonString);
                } 
                catch  
                {
                    throw new Exception($"Invalid Json string: {jsonString}");
                }
            }
            else
            {
                throw new Exception($"Invalid Json string: {jsonString}");
            }
        }
    }
}
