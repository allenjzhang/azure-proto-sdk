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
            : this(genericOperations.ClientContext, genericOperations.Id)
        {
        }

        public ResourceOperationsBase(AzureResourceManagerClientContext context, ResourceIdentifier id)
            : this(context, new ArmResource(id))
        {
        }

        public ResourceOperationsBase(AzureResourceManagerClientContext context, Resource resource)
            : base(context, resource)
        {
        }

        public abstract ArmResponse<TOperations> Get();

        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);
    }
}
