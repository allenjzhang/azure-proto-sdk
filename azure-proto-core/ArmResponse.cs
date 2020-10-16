﻿using Azure;

namespace azure_proto_core
{
    public abstract class ArmResponse<TOperations> : Response<TOperations>
    {
    }

    public class ArmVoidResponse : ArmResponse<Response>
    {
        private Response _response;

        public ArmVoidResponse(Response response)
        {
            _response = response;
        }

        public override Response Value => _response;

        public override Response GetRawResponse()
        {
            return _response;
        }
    }
}