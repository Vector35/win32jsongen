﻿// <copyright file="Api.cs" company="https://github.com/marlersoft">
// Copyright (c) https://github.com/marlersoft. All rights reserved.
// </copyright>

namespace JsonWin32Generator
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.Metadata;

    internal class Api
    {
        internal Api(string @namespace)
        {
            Enforce.Data(
                @namespace.StartsWith(Metadata.WindowsWin32NamespacePrefix, StringComparison.Ordinal) ||
                @namespace.StartsWith(Metadata.WindowsWdkNamespacePrefix, StringComparison.Ordinal),
                Fmt.In($"unexpected namespace '{@namespace}', expected it to start with '{Metadata.WindowsWin32NamespacePrefix}'"));
            this.Name = @namespace[Metadata.WindowsWin32NamespacePrefix.Length..];
        }

        internal string Name { get; }

        internal TypeGenInfoSet TopLevelTypes { get; } = new TypeGenInfoSet();

        internal Dictionary<string, string> TypeNameFqnMap { get; } = new Dictionary<string, string>();

        internal FieldDefinitionHandleCollection? Constants { get; set; }

        internal MethodDefinitionHandleCollection? Funcs { get; set; }

        internal void AddTopLevelType(TypeGenInfo typeInfo)
        {
            Enforce.Invariant(!typeInfo.IsNested);
            this.TopLevelTypes.Add(typeInfo);
            if (this.TypeNameFqnMap.TryGetValue(typeInfo.Name, out string? fqn))
            {
                Enforce.Data(typeInfo.Fqn == fqn);
            }
            else
            {
                this.TypeNameFqnMap.Add(typeInfo.Name, typeInfo.Fqn);
            }
        }
    }
}
