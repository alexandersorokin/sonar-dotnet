﻿/*
 * SonarAnalyzer for .NET
 * Copyright (C) 2015-2020 SonarSource SA
 * mailto: contact AT sonarsource DOT com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

extern alias csharp;
using csharp::SonarAnalyzer.Rules.CSharp;
using Moq;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SonarAnalyzer.Helpers;
using SonarAnalyzer.UnitTest.TestFramework;
using System.Linq;
using SonarAnalyzer.UnitTest.MetadataReferences;
using System.Collections.Generic;

namespace SonarAnalyzer.UnitTest.Rules
{
    [TestClass]
    public class XmlExternalEntityShouldNotBeParsedTest
    {
        [DataRow(NetFrameworkVersion.After452, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlDocument.cs")]
        [DataRow(NetFrameworkVersion.Probably35, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlDocument_Net35.cs")]
        [DataRow(NetFrameworkVersion.Between4And451, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlDocument_Net4.cs")]
        [DataRow(NetFrameworkVersion.Unknown, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlDocument_UnknownFrameworkVersion.cs")]
        [DataTestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_XmlDocument(NetFrameworkVersion version, string testFilePath) =>
            Verifier.VerifyAnalyzer(testFilePath,
                new XmlExternalEntityShouldNotBeParsed(GetVersionProviderMock(version)),
                additionalReferences: MetadataReferenceFacade.GetSystemXml()
                    .Concat(MetadataReferenceFacade.GetSystemData())
                    .Concat(MetadataReferenceFacade.GetSystemXmlLinq())
                    .Concat(NuGetMetadataReference.MicrosoftWebXdt())
                    .ToArray());

#if NET
        [TestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_XmlDocument_CSharp9() =>
            Verifier.VerifyAnalyzerFromCSharp9Console(@"TestCases\XmlExternalEntityShouldNotBeParsed_XmlDocument_CSharp9.cs",
                new XmlExternalEntityShouldNotBeParsed(GetVersionProviderMock(NetFrameworkVersion.After452)),
                MetadataReferenceFacade.GetSystemXml()
                    .Concat(MetadataReferenceFacade.GetSystemData())
                    .Concat(MetadataReferenceFacade.GetSystemXmlLinq())
                    .Concat(NuGetMetadataReference.MicrosoftWebXdt())
                    .ToArray());
#endif

        [DataRow(NetFrameworkVersion.After452, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlTextReader.cs")]
        [DataRow(NetFrameworkVersion.Probably35, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlTextReader_Net35.cs")]
        [DataRow(NetFrameworkVersion.Between4And451, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlTextReader_Net4.cs")]
        [DataRow(NetFrameworkVersion.Unknown, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlTextReader_UnknownFrameworkVersion.cs")]
        [DataTestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_XmlTextReader(NetFrameworkVersion version, string testFilePath) =>
            Verifier.VerifyAnalyzer(testFilePath, new XmlExternalEntityShouldNotBeParsed(GetVersionProviderMock(version)),
                ParseOptionsHelper.FromCSharp8,
                additionalReferences: MetadataReferenceFacade.GetSystemXml().ToArray());

#if NET
        [TestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_XmlTextReader_CSharp9() =>
            Verifier.VerifyAnalyzerFromCSharp9Console(@"TestCases\XmlExternalEntityShouldNotBeParsed_XmlTextReader_CSharp9.cs",
                new XmlExternalEntityShouldNotBeParsed(GetVersionProviderMock(NetFrameworkVersion.After452)),
                MetadataReferenceFacade.GetSystemXml().ToArray());
#endif

        [DataRow(NetFrameworkVersion.After452, @"TestCases\XmlExternalEntityShouldNotBeParsed_AlwaysSafe.cs")]
        [DataRow(NetFrameworkVersion.Unknown, @"TestCases\XmlExternalEntityShouldNotBeParsed_AlwaysSafe.cs")]
        [DataTestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_AlwaysSafe(NetFrameworkVersion version, string testFilePath) => VerifyRule(version, testFilePath);

        [DataRow(NetFrameworkVersion.Probably35, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlReader_Net35.cs")]
        [DataRow(NetFrameworkVersion.Between4And451, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlReader_Net4.cs")]
        [DataRow(NetFrameworkVersion.After452, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlReader_Net452.cs")]
        [DataRow(NetFrameworkVersion.Unknown, @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlReader_Net452.cs")]
        [DataTestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_XmlReader(NetFrameworkVersion version, string testFilePath) => VerifyRule(version, testFilePath);

#if NET
        [TestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_XmlReader_CSharp9() =>
            Verifier.VerifyAnalyzerFromCSharp9Console(@"TestCases\XmlExternalEntityShouldNotBeParsed_XmlReader_CSharp9.cs",
                                                      new XmlExternalEntityShouldNotBeParsed(GetVersionProviderMock(NetFrameworkVersion.After452)),
                                                      MetadataReferenceFacade.GetSystemXml()
                                                          .Concat(MetadataReferenceFacade.GetSystemData())
                                                          .Concat(MetadataReferenceFacade.GetSystemXmlLinq())
                                                          .ToArray());

        [TestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_XPathDocument_CSharp9() =>
            Verifier.VerifyAnalyzerFromCSharp9Console(@"TestCases\XmlExternalEntityShouldNotBeParsed_XPathDocument_CSharp9.cs",
                                                      new XmlExternalEntityShouldNotBeParsed(GetVersionProviderMock(NetFrameworkVersion.After452)),
                                                      MetadataReferenceFacade.GetSystemXml()
                                                          .Concat(MetadataReferenceFacade.GetSystemData())
                                                          .Concat(MetadataReferenceFacade.GetSystemXmlLinq())
                                                          .ToArray());
#endif

        [DataRow(NetFrameworkVersion.Probably35, @"TestCases\XmlExternalEntityShouldNotBeParsed_XPathDocument_Net35.cs")]
        [DataRow(NetFrameworkVersion.Between4And451, @"TestCases\XmlExternalEntityShouldNotBeParsed_XPathDocument_Net4.cs")]
        [DataRow(NetFrameworkVersion.After452, @"TestCases\XmlExternalEntityShouldNotBeParsed_XPathDocument_Net452.cs")]
        [DataRow(NetFrameworkVersion.Unknown, @"TestCases\XmlExternalEntityShouldNotBeParsed_XPathDocument_Net452.cs")]
        [DataTestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_XPathDocument(NetFrameworkVersion version, string testFilePath) => VerifyRule(version, testFilePath);

        [TestMethod]
        [TestCategory("Rule")]
        public void XmlExternalEntityShouldNotBeParsed_NoCrashOnExternalParameterUse() =>
            Verifier.VerifyAnalyzer(
                new[]
                {
                    @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlReader_ExternalParameter.cs",
                    @"TestCases\XmlExternalEntityShouldNotBeParsed_XmlReader_ParameterProvider.cs"
                },
                new XmlExternalEntityShouldNotBeParsed(GetVersionProviderMock(NetFrameworkVersion.After452)),
                additionalReferences: MetadataReferenceFacade.GetSystemXml());

        private static void VerifyRule(NetFrameworkVersion version, string testFilePath, OutputKind outputKind = OutputKind.DynamicallyLinkedLibrary, IEnumerable<ParseOptions> options = null) =>
            Verifier.VerifyAnalyzer(testFilePath,
                new XmlExternalEntityShouldNotBeParsed(GetVersionProviderMock(version)),
                additionalReferences: MetadataReferenceFacade.GetSystemXml()
                    .Concat(MetadataReferenceFacade.GetSystemData())
                    .Concat(MetadataReferenceFacade.GetSystemXmlLinq())
                    .ToArray(),
                outputKind: outputKind,
                options: options);

        private static INetFrameworkVersionProvider GetVersionProviderMock(NetFrameworkVersion version)
        {
            var versionProviderMock = new Mock<INetFrameworkVersionProvider>();
            versionProviderMock
                .Setup(vp => vp.GetDotNetFrameworkVersion(It.IsAny<Compilation>()))
                .Returns(version);

            return versionProviderMock.Object;
        }
    }
}

