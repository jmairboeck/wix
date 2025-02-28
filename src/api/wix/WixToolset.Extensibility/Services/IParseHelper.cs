// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Extensibility.Services
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using WixToolset.Data;
    using WixToolset.Data.Symbols;
    using WixToolset.Data.WindowsInstaller;
    using WixToolset.Extensibility.Data;

    /// <summary>
    /// Interface provided to help compiler extensions parse.
    /// </summary>
    public interface IParseHelper
    {
        /// <summary>
        /// Creates a version 3 name-based UUID.
        /// </summary>
        /// <param name="namespaceGuid">The namespace UUID.</param>
        /// <param name="value">The value.</param>
        /// <returns>The generated GUID for the given namespace and value.</returns>
        string CreateGuid(Guid namespaceGuid, string value);

        /// <summary>
        /// Create an identifier by hashing data from the row.
        /// </summary>
        /// <param name="prefix">Three letter or less prefix for generated row identifier.</param>
        /// <param name="args">Information to hash.</param>
        /// <returns>The new identifier.</returns>
        Identifier CreateIdentifier(string prefix, params string[] args);

        /// <summary>
        /// Create an identifier based on passed file name
        /// </summary>
        /// <param name="filename">File name to generate identifier from</param>
        /// <returns>The new identifier.</returns>
        Identifier CreateIdentifierFromFilename(string filename);

        /// <summary>
        /// Append a suffix to the given name based on the current platform.
        /// If the current platform is not in the supported platforms, then it returns null.
        /// </summary>
        /// <param name="name">The base name for the identifier.</param>
        /// <param name="currentPlatform">The platform being compiled.</param>
        /// <param name="supportedPlatforms">The platforms for which there are specialized implementations.</param>
        /// <returns>The generated identifier value, or null if the current platform isn't supported.</returns>
        string CreateIdentifierValueFromPlatform(string name, Platform currentPlatform, BurnPlatforms supportedPlatforms);

        /// <summary>
        /// Creates a symbol in the section.
        /// </summary>
        /// <param name="section">Section to add the new symbol to.</param>
        /// <param name="sourceLineNumbers">Source and line number of current symbol.</param>
        /// <param name="symbolName">Name of symbol definition.</param>
        /// <param name="identifier">Optional identifier for the symbol.</param>
        /// <returns>New symbol.</returns>
        IntermediateSymbol CreateSymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, string symbolName, Identifier identifier = null);

        /// <summary>
        /// Creates a symbol in the section.
        /// </summary>
        /// <param name="section">Section to add the new symbol to.</param>
        /// <param name="sourceLineNumbers">Source and line number of current symbol.</param>
        /// <param name="symbolDefinition">Symbol definition to create from.</param>
        /// <param name="identifier">Optional identifier for the symbol.</param>
        /// <returns>New symbol.</returns>
        IntermediateSymbol CreateSymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, IntermediateSymbolDefinition symbolDefinition, Identifier identifier = null);

        /// <summary>
        /// Creates a directory row from a name.
        /// </summary>
        /// <param name="section">Section to add the new symbol to.</param>
        /// <param name="sourceLineNumbers">Source line information.</param>
        /// <param name="id">Optional identifier for the new row.</param>
        /// <param name="parentId">Optional identifier for the parent row.</param>
        /// <param name="name">Long name of the directory.</param>
        /// <param name="shortName">Optional short name of the directory.</param>
        /// <param name="sourceName">Optional source name for the directory.</param>
        /// <param name="shortSourceName">Optional short source name for the directory.</param>
        /// <returns>Identifier for the newly created row.</returns>
        Identifier CreateDirectorySymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, Identifier id, string parentId, string name, string shortName = null, string sourceName = null, string shortSourceName = null);

        /// <summary>
        /// Creates directories using the inline directory syntax.
        /// </summary>
        /// <param name="section">Section to add the new symbol to.</param>
        /// <param name="sourceLineNumbers">Source line information.</param>
        /// <param name="attribute">Attribute containing the inline syntax.</param>
        /// <param name="parentId">Optional identifier of parent directory.</param>
        /// <param name="inlineSyntax">Optional inline syntax to override attribute's value.</param>
        /// <param name="sectionCachedInlinedDirectoryIds">Mapping of inline directory syntax to ids for the section.</param>
        /// <returns>Identifier of the leaf directory created.</returns>
        string CreateDirectoryReferenceFromInlineSyntax(IntermediateSection section, SourceLineNumber sourceLineNumbers, XAttribute attribute, string parentId, string inlineSyntax, IDictionary<string, string> sectionCachedInlinedDirectoryIds);

        /// <summary>
        /// Creates a Registry symbol in the active section.
        /// </summary>
        /// <param name="section">Active section.</param>
        /// <param name="sourceLineNumbers">Source and line number of the current symbol.</param>
        /// <param name="root">The registry entry root.</param>
        /// <param name="key">The registry entry key.</param>
        /// <param name="name">The registry entry name.</param>
        /// <param name="value">The registry entry value.</param>
        /// <param name="componentId">The component which will control installation/uninstallation of the registry entry.</param>
        /// <param name="valueType">The registry value type. Default is string.</param>
        /// <param name="valueAction">The way to apply the registry value. Default is write.</param>
        Identifier CreateRegistrySymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, RegistryRootType root, string key, string name, string value, string componentId, RegistryValueType valueType = RegistryValueType.String, RegistryValueActionType valueAction = RegistryValueActionType.Write);

        /// <summary>
        /// Creates a numeric Registry symbol in the active section.
        /// </summary>
        /// <param name="section">Active section.</param>
        /// <param name="sourceLineNumbers">Source and line number of the current symbol.</param>
        /// <param name="root">The registry entry root.</param>
        /// <param name="key">The registry entry key.</param>
        /// <param name="name">The registry entry name.</param>
        /// <param name="value">The numeric registry entry value.</param>
        /// <param name="componentId">The component which will control installation/uninstallation of the registry entry.</param>
        Identifier CreateRegistrySymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, RegistryRootType root, string key, string name, int value, string componentId);

        /// <summary>
        /// Create a WixSimpleReference symbol in the active section.
        /// </summary>
        /// <param name="section">Active section.</param>
        /// <param name="sourceLineNumbers">Source line information for the row.</param>
        /// <param name="symbolName">The symbol name of the simple reference.</param>
        /// <param name="primaryKey">The primary key of the simple reference.</param>
        void CreateSimpleReference(IntermediateSection section, SourceLineNumber sourceLineNumbers, string symbolName, string primaryKey);

        /// <summary>
        /// Create a WixSimpleReference symbol in the active section.
        /// </summary>
        /// <param name="section">Active section.</param>
        /// <param name="sourceLineNumbers">Source line information for the row.</param>
        /// <param name="symbolName">The symbol name of the simple reference.</param>
        /// <param name="primaryKeys">The primary keys of the simple reference.</param>
        void CreateSimpleReference(IntermediateSection section, SourceLineNumber sourceLineNumbers, string symbolName, params string[] primaryKeys);

        /// <summary>
        /// Create a WixSimpleReference symbol in the active section.
        /// </summary>
        /// <param name="section">Active section.</param>
        /// <param name="sourceLineNumbers">Source line information for the row.</param>
        /// <param name="symbolDefinition">The symbol definition of the simple reference.</param>
        /// <param name="primaryKey">The primary key of the simple reference.</param>
        void CreateSimpleReference(IntermediateSection section, SourceLineNumber sourceLineNumbers, IntermediateSymbolDefinition symbolDefinition, string primaryKey);

        /// <summary>
        /// Create a WixSimpleReference symbol in the active section.
        /// </summary>
        /// <param name="section">Active section.</param>
        /// <param name="sourceLineNumbers">Source line information for the row.</param>
        /// <param name="symbolDefinition">The symbol definition of the simple reference.</param>
        /// <param name="primaryKeys">The primary keys of the simple reference.</param>
        void CreateSimpleReference(IntermediateSection section, SourceLineNumber sourceLineNumbers, IntermediateSymbolDefinition symbolDefinition, params string[] primaryKeys);

        /// <summary>
        /// Create a reference in the specified section for a custom action specialized for specific platforms,
        /// given standard prefixes for naming and suffixes for platforms.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information.</param>
        /// <param name="section">Section to create the reference in.</param>
        /// <param name="customAction">The custom action base name.</param>
        /// <param name="platform">The platform being compiled.</param>
        /// <param name="supportedPlatforms">The platforms for which there are specialized custom actions.</param>
        void CreateCustomActionReference(SourceLineNumber sourceLineNumbers, IntermediateSection section, string customAction, Platform platform, CustomActionPlatforms supportedPlatforms);

        /// <summary>
        /// Creates WixComplexReference and WixGroup symbols in the active section.
        /// </summary>
        /// <param name="section">Section to create the reference in.</param>
        /// <param name="sourceLineNumbers">Source line information.</param>
        /// <param name="parentType">The parent type.</param>
        /// <param name="parentId">The parent id.</param>
        /// <param name="parentLanguage">The parent language.</param>
        /// <param name="childType">The child type.</param>
        /// <param name="childId">The child id.</param>
        /// <param name="isPrimary">Whether the child is primary.</param>
        void CreateComplexReference(IntermediateSection section, SourceLineNumber sourceLineNumbers, ComplexReferenceParentType parentType, string parentId, string parentLanguage, ComplexReferenceChildType childType, string childId, bool isPrimary);

        /// <summary>
        /// A symbol in the WixGroup table is added for this child node and its parent node.
        /// </summary>
        /// <param name="section">Section to create the reference in.</param>
        /// <param name="sourceLineNumbers">Source line information for the row.</param>
        /// <param name="parentType">Type of child's complex reference parent.</param>
        /// <param name="parentId">Id of the parenet node.</param>
        /// <param name="childType">Complex reference type of child</param>
        /// <param name="childId">Id of the Child Node.</param>
        void CreateWixGroupSymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, ComplexReferenceParentType parentType, string parentId, ComplexReferenceChildType childType, string childId);

        /// <summary>
        /// Creates a symbol in the WixSearch table.
        /// </summary>
        /// <param name="section">Section to create the reference in.</param>
        /// <param name="sourceLineNumbers">Source line number for the search element.</param>
        /// <param name="elementName">Name of search element.</param>
        /// <param name="id">Identifier of the search.</param>
        /// <param name="variable">The Burn variable to store the result into.</param>
        /// <param name="condition">A condition to test before evaluating the search.</param>
        /// <param name="after">The search that this one will execute after.</param>
        /// <param name="bundleExtensionId">The id of the bundle extension that handles this search.</param>
        void CreateWixSearchSymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, string elementName, Identifier id, string variable, string condition, string after, string bundleExtensionId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="section">Section to create the reference in.</param>
        /// <param name="sourceLineNumbers">Source line number for the parent element.</param>
        /// <param name="id">Identifier of the search (key into the WixSearch table)</param>
        /// <param name="parentId">Identifier of the search that comes before (key into the WixSearch table)</param>
        /// <param name="attributes">Further details about the relation between id and parentId.</param>
        void CreateWixSearchRelationSymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, Identifier id, string parentId, int attributes);

        /// <summary>
        /// Checks if the string contains a property (i.e. "foo[Property]bar")
        /// </summary>
        /// <param name="possibleProperty">String to evaluate for properties.</param>
        /// <returns>True if a property is found in the string.</returns>
        bool ContainsProperty(string possibleProperty);

        /// <summary>
        /// Add the appropriate symbols to make sure that the given table shows up in the resulting output.
        /// </summary>
        /// <param name="section">Active section.</param>
        /// <param name="sourceLineNumbers">Source line numbers.</param>
        /// <param name="tableName">Name of the table to ensure existance of.</param>
        void EnsureTable(IntermediateSection section, SourceLineNumber sourceLineNumbers, string tableName);

        /// <summary>
        /// Add the appropriate symbols to make sure that the given table shows up in the resulting output.
        /// </summary>
        /// <param name="section">Active section.</param>
        /// <param name="sourceLineNumbers">Source line numbers.</param>
        /// <param name="tableDefinition">Definition of the table to ensure existance of.</param>
        void EnsureTable(IntermediateSection section, SourceLineNumber sourceLineNumbers, TableDefinition tableDefinition);

        /// <summary>
        /// Get an attribute value and displays an error if the value is empty by default.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <param name="emptyRule">A rule for the contents of the value. If the contents do not follow the rule, an error is thrown.</param>
        /// <returns>The attribute's value.</returns>
        string GetAttributeValue(SourceLineNumber sourceLineNumbers, XAttribute attribute, EmptyRule emptyRule = EmptyRule.CanBeWhitespaceOnly);

        /// <summary>
        /// Gets a bundle variable name identifier and displays an error for an illegal value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <returns>The attribute's identifier value or a special value if an error occurred.</returns>
        Identifier GetAttributeBundleVariableNameIdentifier(SourceLineNumber sourceLineNumbers, XAttribute attribute);

        /// <summary>
        /// Gets a bundle variable name value and displays an error for an illegal value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <param name="nameRule">A rule for the contents of the value. If the contents do not follow the rule, an error is thrown.</param>
        /// <returns>The attribute's value.</returns>
        string GetAttributeBundleVariableNameValue(SourceLineNumber sourceLineNumbers, XAttribute attribute, BundleVariableNameRule nameRule = BundleVariableNameRule.CanBeWellKnown | BundleVariableNameRule.CanHaveReservedPrefix);

        /// <summary>
        /// Get a guid attribute value and displays an error for an illegal guid value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <param name="generatable">Determines whether the guid can be automatically generated.</param>
        /// <param name="canBeEmpty">If true, no error is raised on empty value. If false, an error is raised.</param>
        /// <returns>The attribute's guid value or a special value if an error occurred.</returns>
        string GetAttributeGuidValue(SourceLineNumber sourceLineNumbers, XAttribute attribute, bool generatable = false, bool canBeEmpty = false);

        /// <summary>
        /// Get an identifier attribute value and displays an error for an illegal identifier value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <returns>The attribute's identifier value or a special value if an error occurred.</returns>
        Identifier GetAttributeIdentifier(SourceLineNumber sourceLineNumbers, XAttribute attribute);

        /// <summary>
        /// Get an identifier attribute value and displays an error for an illegal identifier value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <returns>The attribute's identifier value or a special value if an error occurred.</returns>
        string GetAttributeIdentifierValue(SourceLineNumber sourceLineNumbers, XAttribute attribute);

        /// <summary>
        /// Get an integer attribute value and displays an error for an illegal integer value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <param name="minimum">The minimum legal value.</param>
        /// <param name="maximum">The maximum legal value.</param>
        /// <returns>The attribute's integer value or a special value if an error occurred during conversion.</returns>
        int GetAttributeIntegerValue(SourceLineNumber sourceLineNumbers, XAttribute attribute, int minimum, int maximum);

        /// <summary>
        /// Get a long integral attribute value and displays an error for an illegal long value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <param name="minimum">The minimum legal value.</param>
        /// <param name="maximum">The maximum legal value.</param>
        /// <returns>The attribute's long value or a special value if an error occurred during conversion.</returns>
        long GetAttributeLongValue(SourceLineNumber sourceLineNumbers, XAttribute attribute, long minimum, long maximum);

        /// <summary>
        /// Gets a long filename value and displays an error for an illegal long filename value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <param name="allowWildcards">true if wildcards are allowed in the filename.</param>
        /// <param name="allowRelative">true if relative paths are allowed in the filename.</param>
        /// <returns>The attribute's long filename value.</returns>
        string GetAttributeLongFilename(SourceLineNumber sourceLineNumbers, XAttribute attribute, bool allowWildcards = false, bool allowRelative = false);

        /// <summary>
        /// Gets a RegistryRootType value and displays an error for an illegal value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <param name="allowHkmu">Whether HKMU is returned as -1 (true), or treated as an error (false).</param>
        /// <returns>The attribute's RegisitryRootType value.</returns>
        RegistryRootType? GetAttributeRegistryRootValue(SourceLineNumber sourceLineNumbers, XAttribute attribute, bool allowHkmu);

        /// <summary>
        /// Gets a version value or possibly a binder variable and displays an error for an illegal version value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <returns>The attribute's version value.</returns>
        string GetAttributeVersionValue(SourceLineNumber sourceLineNumbers, XAttribute attribute);

        /// <summary>
        /// Gets a yes/no value and displays an error for an illegal yes/no value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <returns>The attribute's YesNoType value.</returns>
        YesNoType GetAttributeYesNoValue(SourceLineNumber sourceLineNumbers, XAttribute attribute);

        /// <summary>
        /// Gets a yes/no/default value and displays an error for an illegal yes/no/default value.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="attribute">The attribute containing the value to get.</param>
        /// <returns>The attribute's YesNoType value.</returns>
        YesNoDefaultType GetAttributeYesNoDefaultValue(SourceLineNumber sourceLineNumbers, XAttribute attribute);

        /// <summary>
        /// Gets a source line number for an element.
        /// </summary>
        /// <param name="element">Element to get source line number.</param>
        /// <returns>Source line number.</returns>
        SourceLineNumber GetSourceLineNumbers(XElement element);

        /// <summary>
        /// Gets node's inner text and ensure's it is safe for use in a condition by trimming any extra whitespace.
        /// </summary>
        /// <param name="node">The node to ensure inner text is a condition.</param>
        /// <returns>The value converted into a safe condition.</returns>
        [Obsolete]
        string GetConditionInnerText(XElement node);

        /// <summary>
        /// Get an element's inner text and trims any extra whitespace.
        /// </summary>
        /// <param name="element">The element with inner text to be trimmed.</param>
        /// <returns>The node's inner text trimmed.</returns>
        [Obsolete]
        string GetTrimmedInnerText(XElement element);

        /// <summary>
        /// Validates that the element does not contain inner text.
        /// </summary>
        /// <param name="element">Element to check for inner text.</param>
        void InnerTextDisallowed(XElement element);

        /// <summary>
        /// Validates that the element does not contain inner text and suggests which attribute to use instead.
        /// </summary>
        /// <param name="element">Element to check for inner text.</param>
        /// <param name="attributeName">Name of attribute to use instead of inner text.</param>
        void InnerTextDisallowed(XElement element, string attributeName);

        /// <summary>
        /// Verifies that a value is a legal identifier.
        /// </summary>
        /// <param name="value">The value to verify.</param>
        /// <returns>true if the value is an identifier; false otherwise.</returns>
        bool IsValidIdentifier(string value);

        /// <summary>
        /// Verifies if an identifier is a valid loc identifier.
        /// </summary>
        /// <param name="identifier">Identifier to verify.</param>
        /// <returns>True if the identifier is a valid loc identifier.</returns>
        bool IsValidLocIdentifier(string identifier);

        /// <summary>
        /// Verifies if a filename is a valid long filename.
        /// </summary>
        /// <param name="filename">Filename to verify.</param>
        /// <param name="allowWildcards">true if wildcards are allowed in the filename.</param>
        /// <param name="allowRelative">true if relative paths are allowed in the filename.</param>
        /// <returns>True if the filename is a valid long filename</returns>
        bool IsValidLongFilename(string filename, bool allowWildcards = false, bool allowRelative = false);

        /// <summary>
        /// Verifies if a filename is a valid short filename.
        /// </summary>
        /// <param name="filename">Filename to verify.</param>
        /// <param name="allowWildcards">Indicates whether wildcards are allowed in the filename.</param>
        /// <returns>True if the filename is a valid short filename</returns>
        bool IsValidShortFilename(string filename, bool allowWildcards);

        /// <summary>
        /// Attempts to use an extension to parse the attribute.
        /// </summary>
        /// <param name="extensions"></param>
        /// <param name="intermediate">Parent intermediate.</param>
        /// <param name="section">Parent section.</param>
        /// <param name="element">Element containing attribute to be parsed.</param>
        /// <param name="attribute">Attribute to be parsed.</param>
        /// <param name="context">Extra information about the context in which this element is being parsed.</param>
        void ParseExtensionAttribute(IEnumerable<ICompilerExtension> extensions, Intermediate intermediate, IntermediateSection section, XElement element, XAttribute attribute, IDictionary<string, string> context = null);

        /// <summary>
        /// Attempts to use an extension to parse the element.
        /// </summary>
        /// <param name="extensions"></param>
        /// <param name="intermediate">Parent intermediate.</param>
        /// <param name="section">Parent section.</param>
        /// <param name="parentElement">Element containing element to be parsed.</param>
        /// <param name="element">Element to be parsed.</param>
        /// <param name="context">Extra information about the context in which this element is being parsed.</param>
        void ParseExtensionElement(IEnumerable<ICompilerExtension> extensions, Intermediate intermediate, IntermediateSection section, XElement parentElement, XElement element, IDictionary<string, string> context = null);

        /// <summary>
        /// Attempts to use an extension to parse the element, with support for setting component keypath.
        /// </summary>
        /// <param name="extensions"></param>
        /// <param name="intermediate">Parent intermediate.</param>
        /// <param name="section">Parent section.</param>
        /// <param name="parentElement">Element containing element to be parsed.</param>
        /// <param name="element">Element to be parsed.</param>
        /// <param name="context">Extra information about the context in which this element is being parsed.</param>
        IComponentKeyPath ParsePossibleKeyPathExtensionElement(IEnumerable<ICompilerExtension> extensions, Intermediate intermediate, IntermediateSection section, XElement parentElement, XElement element, IDictionary<string, string> context);

        /// <summary>
        /// Process all children of the element looking for extensions and erroring on the unexpected.
        /// </summary>
        /// <param name="extensions"></param>
        /// <param name="intermediate">Parent intermediate.</param>
        /// <param name="section">Parent section.</param>
        /// <param name="element">Element to parse children.</param>
        /// <param name="context">Extra information about the context in which this element is being parsed.</param>
        void ParseForExtensionElements(IEnumerable<ICompilerExtension> extensions, Intermediate intermediate, IntermediateSection section, XElement element, IDictionary<string, string> context = null);

        /// <summary>
        /// Schedules an action symbol.
        /// </summary>
        /// <param name="section">Section to add the symbol to.</param>
        /// <param name="sourceLineNumbers">Source line information about the owner element.</param>
        /// <param name="access">Access modifier for the scheduled action.</param>
        /// <param name="sequence">Sequence to add the action to.</param>
        /// <param name="name">Name of action.</param>
        /// <param name="condition">Optional condition of action.</param>
        /// <param name="beforeAction">Optional action to schedule before.</param>
        /// <param name="afterAction">Option action to schedule after.</param>
        /// <param name="overridable">Optional overridable flag.</param>
        WixActionSymbol ScheduleActionSymbol(IntermediateSection section, SourceLineNumber sourceLineNumbers, AccessModifier access, SequenceTable sequence, string name, string condition, string beforeAction, string afterAction, bool overridable = false);

        /// <summary>
        /// Called when the compiler encounters an unexpected attribute.
        /// </summary>
        /// <param name="element">Parent element that found unexpected attribute.</param>
        /// <param name="attribute">Unexpected attribute.</param>
        void UnexpectedAttribute(XElement element, XAttribute attribute);

        /// <summary>
        /// Called when the compiler encounters an unexpected child element.
        /// </summary>
        /// <param name="parentElement">Parent element that found unexpected child.</param>
        /// <param name="childElement">Unexpected child element.</param>
        void UnexpectedElement(XElement parentElement, XElement childElement);
    }
}
