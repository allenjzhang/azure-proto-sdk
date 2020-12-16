// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    public abstract class ResourceOperationsBase<TOperations, TResource> : OperationsBase
        where TResource : Resource
        where TOperations : ResourceOperationsBase<TOperations, TResource>
    {
        public ResourceOperationsBase(ArmResourceOperations genericOperations)
            : this(genericOperations.ClientContext, genericOperations.Id, genericOperations.ClientOptions)
        {
        }

        public ResourceOperationsBase(AzureResourceManagerClientContext context, ResourceIdentifier id, AzureResourceManagerClientOptions clientOptions)
            : this(context, new ArmResource(id), clientOptions)
        {
        }

        public ResourceOperationsBase(AzureResourceManagerClientContext context, Resource resource, AzureResourceManagerClientOptions clientOptions)
            : base(context, resource, clientOptions)
        {
        }

        public abstract ArmResponse<TOperations> Get();

        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);
    }
}
