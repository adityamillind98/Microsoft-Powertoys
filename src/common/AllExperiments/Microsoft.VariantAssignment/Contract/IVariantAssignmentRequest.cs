﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// The goal of this class is to just mock out the Microsoft.VariantAssignment close sourced objects
namespace Microsoft.VariantAssignment.Contract
{
    public interface IVariantAssignmentRequest
    {
        /// <summary>
        /// Gets inputs used for evaluating filters, assignment units, etc.
        /// </summary>
        IReadOnlyCollection<(string Key, string Value)> Parameters { get; }
    }
}
