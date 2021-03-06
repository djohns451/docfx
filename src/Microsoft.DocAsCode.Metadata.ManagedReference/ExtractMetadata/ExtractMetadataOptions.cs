﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DocAsCode.Metadata.ManagedReference
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    using Newtonsoft.Json;

    internal class ExtractMetadataOptions
    {
        public bool ShouldSkipMarkup { get; set; }

        public bool PreserveRawInlineComments { get; set; }

        public string FilterConfigFile { get; set; }

        public Dictionary<string, string> MSBuildProperties { get; set; }

        [JsonIgnore]
        public IReadOnlyDictionary<Compilation, IEnumerable<IMethodSymbol>> ExtensionMethods { get; set; }

        public bool HasChanged(IncrementalCheck check, bool careMSBuildProperties)
        {
            return check.BuildInfo.Options == null ||
                check.BuildInfo.Options.ShouldSkipMarkup != ShouldSkipMarkup ||
                check.BuildInfo.Options.PreserveRawInlineComments != PreserveRawInlineComments ||
                check.BuildInfo.Options.FilterConfigFile != FilterConfigFile ||
                check.IsFileModified(FilterConfigFile) ||
                (careMSBuildProperties && check.MSBuildPropertiesUpdated(MSBuildProperties));
        }
    }
}
