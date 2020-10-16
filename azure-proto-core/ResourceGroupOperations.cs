﻿using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public class ResourceGroupOperations : ResourceOperationsBase<ResourceGroupOperations, PhResourceGroup>
    {
        public static readonly string AzureResourceType = "Microsoft.Resources/resourceGroups";

        internal ResourceGroupOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        internal ResourceGroupOperations(ArmClientContext context, Resource resource) : base(context, resource) { }

        public override ResourceType ResourceType => AzureResourceType;

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.Name, cancellationToken));
        }

        public override ArmResponse<ResourceGroupOperations> Get()
        {
            return new PhArmResponse<ResourceGroupOperations, ResourceGroup>(Operations.Get(Id.Name), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public async override Task<ArmResponse<ResourceGroupOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceGroupOperations, ResourceGroup>(await Operations.GetAsync(Id.Name, cancellationToken), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public override ArmOperation<ResourceGroupOperations> AddTag(string name, string value)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(Operations.Update(Id.Name, patch), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public async override Task<ArmOperation<ResourceGroupOperations>> AddTagAsync(string name, string value, CancellationToken cancellationToken = default)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceGroupOperations, ResourceGroup>(await Operations.UpdateAsync(Id.Name, patch, cancellationToken), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public ArmResponse<TOperations> CreateResource<TContainer, TOperations, TResource>(string name, TResource model, azure_proto_core.Location location = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
        {
            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResource(Id);
            }

            if (location != null)
            {
                myResource = new ArmResource(Id, location);
            }

            TContainer container;
            if (!ArmClient.Registry.TryGetContainer<TContainer, TrackedResource, TOperations, TResource>(this.ClientContext, myResource, out container))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(TResource)}' found.");
            }

            return container.Create(name, model);
        }

        public Task<ArmResponse<TOperations>> CreateResourceAsync<TContainer, TOperations, TResource>(string name, TResource model, azure_proto_core.Location location = default, CancellationToken token = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
        {

            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResource(Id);
            }

            if (location != null)
            {
                myResource = new ArmResource(Id, location);
            }

            TContainer container;
            if (!ArmClient.Registry.TryGetContainer<TContainer, TrackedResource, TOperations, TResource>(this.ClientContext, myResource, out container))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(TResource)}' found.");
            }

            return container.CreateAsync(name, model, token);
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(uri, Id.Subscription, creds))?.ResourceGroups;
    }
}