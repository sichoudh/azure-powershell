﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Profile.Properties;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Management.Internal.Resources;
using Microsoft.Azure.Management.Internal.Resources.Models;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Profile.Default
{
    /// <summary>
    /// Cmdlet to clear default options. 
    /// </summary>
    [Cmdlet(VerbsCommon.Clear, "AzureRmDefault", DefaultParameterSetName = ResourceGroupParameterSet,
         SupportsShouldProcess = true)]
    public class ClearAzureRMDefaultCommand : AzureRMCmdlet
    {
        private const string ResourceGroupParameterSet = "ResourceGroup";
        private bool resourceGroup;

        [Parameter(ParameterSetName = ResourceGroupParameterSet, Mandatory = false, HelpMessage = "Resource Group", ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public SwitchParameter ResourceGroup
        {
            get { return resourceGroup; }
            set { resourceGroup = value; }
        }

        public override void ExecuteCmdlet()
        {
            IAzureContext context = AzureRmProfileProvider.Instance.Profile.DefaultContext;
            // If no parameters are specified, clear all defaults
            if (!ResourceGroup)
            {
                if (context.ExtendedProperties.ContainsKey(Resources.DefaultResourceGroupKey))
                {
                    if (ShouldProcess(string.Format(Resources.DefaultResourceGroupTarget), Resources.DefaultResourceGroupRemovalWarning))
                    {
                        context.ExtendedProperties.Remove(Resources.DefaultResourceGroupKey);
                    }
                }
            }

            // If any parameters are specified, show only defaults with switch parameters set to true
            if (ResourceGroup)
            {
                if (context.ExtendedProperties.ContainsKey(Resources.DefaultResourceGroupKey))
                {
                    if (ShouldProcess(string.Format(Resources.DefaultResourceGroupTarget), Resources.DefaultResourceGroupRemovalWarning))
                    {
                        context.ExtendedProperties.Remove(Resources.DefaultResourceGroupKey);
                    }
                }
            }
        }
    }
}