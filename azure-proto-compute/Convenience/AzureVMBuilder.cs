﻿using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public class AzureVmModelBuilder : IAzureVmModelBuilder
    {
        // TODO: Azure or Ph model?
        // has to use generated model for now
        private VirtualMachine _model;

        internal AzureVmModelBuilder(TrackedResource resourceGroup, string name)
        {
            // TODO: Ph model should allow default constructor and property individually settable
            _model = new VirtualMachine(resourceGroup.Location);
        }

        public IAzureVmModelBuilder ConfigureNetworkInterface(ResourceIdentifier networkInterfaceId)
        {
            _model.NetworkProfile = new NetworkProfile
            {
                NetworkInterfaces = new[] { new NetworkInterfaceReference() { Id = networkInterfaceId } }
            };

            return this;
        }

        public IAzureVmModelBuilder AttachDataDisk(AzureEntity azureEntity)
        {
            throw new NotImplementedException();
        }

        public IAzureVmModelBuilder Location(Location location)
        {
            _model.Location = location;
            return this;
        }

        public IAzureVmModelBuilder Name(string name)
        {
            // Name is not settable !?
            // _model.Name = name;
            return this;
        }

        public IAzureVmModelBuilder UseWindowsImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public IAzureVmModelBuilder UseLinuxImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public AzureVm ToModel()
        {
            // TODO: Any model validation

            return _model;
        }

        public IAzureVmModelBuilder ConfigureWith(AzureEntity azureEntity)
        {
            throw new NotImplementedException();
        }

        IAzureModelBuilderBase<AzureVm> IAzureModelBuilderBase<AzureVm>.Location(Location location)
        {
            throw new NotImplementedException();
        }
    }
}
