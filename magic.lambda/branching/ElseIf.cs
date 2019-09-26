﻿/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System;
using System.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;

namespace magic.lambda.branching
{
    [Slot(Name = "else-if")]
    public class ElseIf : ISlot
    {
        public void Signal(ISignaler signaler, Node input)
        {
            if (input.Children.Count() != 2)
                throw new ApplicationException("Keyword [else-if] requires exactly two children nodes");

            var lambda = input.Children.Skip(1).First();
            if (lambda.Name != ".lambda")
                throw new ApplicationException("Keyword [else-if] requires its second child node to be [.lambda]");

            var previous = input.Previous;
            if (previous == null || (previous.Name != "if" && previous.Name != "else-if"))
                throw new ApplicationException("[else-if] must have an [if] or [else-if] before it");

            var evaluate = true;
            while (previous != null && (previous.Name == "if" || previous.Name == "else-if"))
            {
                if (previous.Children.First().GetEx<bool>())
                {
                    evaluate = false;
                    break;
                }
                previous = previous.Previous;
            }

            if (evaluate)
            {
                signaler.Signal("eval", input);

                if (input.Children.First().GetEx<bool>())
                    signaler.Signal("eval", lambda);
            }
        }
    }
}
