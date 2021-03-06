/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2021, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System;
using System.Linq;
using Xunit;
using magic.node;
using magic.node.extensions;

namespace magic.lambda.tests
{
    public class TypeTests
    {
        [Fact]
        public void TypeFromInt()
        {
            var lambda = Common.Evaluate(@"
.src:int:5
type:x:-");
            Assert.Equal("int", lambda.Children.Last().Value);
        }

        [Fact]
        public void TypeFromDecimal()
        {
            var lambda = Common.Evaluate(@"
.src:decimal:5
type:x:-");
            Assert.Equal("decimal", lambda.Children.Last().Value);
        }

        [Fact]
        public void TypeFromDate()
        {
            var lambda = Common.Evaluate(@"
.src:date:""2021-01-01T23:59:59""
type:x:-");
            Assert.Equal("date", lambda.Children.Last().Value);
        }
    }
}
