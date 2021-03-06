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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SonarAnalyzer.UnitTest.Helpers;
using SonarAnalyzer.UnitTest.TestFramework;

namespace SonarAnalyzer.UnitTest.Rules
{
    [TestClass]
    public class FunctionComplexityTest
    {
        [TestMethod]
        [TestCategory("Rule")]
        public void FunctionComplexity_CS() =>
            Verifier.VerifyAnalyzer(@"TestCases\FunctionComplexity.cs", new SonarAnalyzer.Rules.CSharp.FunctionComplexity { Maximum = 3 }, ParseOptionsHelper.FromCSharp8);

#if NET
        [TestMethod]
        [TestCategory("Rule")]
        public void FunctionComplexity_CS_CSharp9() =>
            Verifier.VerifyAnalyzerFromCSharp9Console(@"TestCases\FunctionComplexity.CSharp9.cs", new SonarAnalyzer.Rules.CSharp.FunctionComplexity { Maximum = 3 });
#endif

        [TestMethod]
        [TestCategory("Rule")]
        public void FunctionComplexity_InsufficientExecutionStack_CS()
        {
            if (!TestContextHelper.IsAzureDevOpsContext) // ToDo: Test doesn't work on Azure DevOps
            {
                Verifier.VerifyAnalyzer(@"TestCases\SyntaxWalker_InsufficientExecutionStackException.cs", new SonarAnalyzer.Rules.CSharp.FunctionComplexity { Maximum = 3 });
            }
        }

        [TestMethod]
        [TestCategory("Rule")]
        public void FunctionComplexity_VB() =>
            Verifier.VerifyAnalyzer(@"TestCases\FunctionComplexity.vb", new SonarAnalyzer.Rules.VisualBasic.FunctionComplexity { Maximum = 3 });
    }
}
