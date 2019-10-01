﻿/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System;
using System.Linq;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;

namespace magic.lambda.loops
{
    /// <summary>
    /// [while] slot that will evaluate its lambda object as long as its condition is true.
    /// </summary>
    [Slot(Name = "while")]
    public class While : ISlot
    {
        /// <summary>
        /// Implementation of signal
        /// </summary>
        /// <param name="signaler">Signaler used to signal</param>
        /// <param name="input">Parameters passed from signaler</param>
        public void Signal(ISignaler signaler, Node input)
        {
            if (input.Children.Count() != 2)
                throw new ApplicationException("Keyword [while] requires exactly two child nodes");

            while(true)
            {
                // Making sure we can reset back to original nodes after every single iteration.
                var old = input.Children.Select(x => x.Clone()).ToList();

                // This will evaluate the condition.
                signaler.Signal("eval", input);

                // Verifying we're supposed to proceed into body of [while].
                if (!input.Children.First().GetEx<bool>())
                    break;

                // Retrieving [.lambda] node and doing basic sanity check.
                var lambda = input.Children.Skip(1).First();
                if (lambda.Name != ".lambda")
                    throw new ApplicationException("Keyword [while] requires its second child to be [.lambda]");

                // Evaluating "body" lambda of [while].
                signaler.Signal("eval", lambda);

                // Resetting back to original nodes.
                input.Clear();

                // Notice, cloning in case we've got another iteration, to avoid changing original nodes' values.
                input.AddRange(old.Select(x => x.Clone()));
            }
        }
    }
}
