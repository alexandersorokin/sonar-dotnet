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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using csharp::SonarAnalyzer.Rules.CSharp;
using SonarAnalyzer.UnitTest.TestFramework;

namespace SonarAnalyzer.UnitTest.Rules
{
    [TestClass]
    public class VirtualEventFieldTest
    {
        [TestMethod]
        [TestCategory("Rule")]
        public void VirtualEventField() =>
            Verifier.VerifyAnalyzer(@"TestCases\VirtualEventField.cs", new VirtualEventField());

#if NET
        [TestMethod]
        [TestCategory("Rule")]
        public void VirtualEventField_CSharp9() =>
            Verifier.VerifyAnalyzerFromCSharp9Library(@"TestCases\VirtualEventField.CSharp9.cs", new VirtualEventField());

        [TestMethod]
        [TestCategory("CodeFix")]
        public void VirtualEventField_CSharp9_CodeFix() =>
            Verifier.VerifyCodeFix(@"TestCases\VirtualEventField.CSharp9.cs",
                                   @"TestCases\VirtualEventField.CSharp9.Fixed.cs",
                                   new VirtualEventField(),
                                   new VirtualEventFieldCodeFixProvider(),
                                   ParseOptionsHelper.FromCSharp9);
#endif

        [TestMethod]
        [TestCategory("CodeFix")]
        public void VirtualEventField_CodeFix() =>
            Verifier.VerifyCodeFix(
                @"TestCases\VirtualEventField.cs",
                @"TestCases\VirtualEventField.Fixed.cs",
                new VirtualEventField(),
                new VirtualEventFieldCodeFixProvider());
    }
}
