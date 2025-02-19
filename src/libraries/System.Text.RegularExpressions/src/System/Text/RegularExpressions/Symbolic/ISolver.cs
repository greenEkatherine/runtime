﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace System.Text.RegularExpressions.Symbolic
{
    /// <summary>
    /// Provides support for operating over sets, including and/or/not operations, converting to/from
    /// <see cref="BDD"/> representations of those sets, and determining whether an element is in the set.
    /// </summary>
    internal interface ISolver<TSet>
    {
        /// <summary>Creates a set containing the single character <paramref name="c"/>.</summary>
        TSet CreateFromChar(char c);

        /// <summary>Creates a set from a <see cref="BDD"/> representation.</summary>
        TSet ConvertFromBDD(BDD set, CharSetSolver solver);

        /// <summary>Converts the set into a <see cref="BDD"/> representation.</summary>
        BDD ConvertToBDD(TSet set, CharSetSolver solver);

        /// <summary>Gets the minterms from the set.</summary>
        TSet[]? GetMinterms();

        /// <summary>Formats the contents of the specified set for human consumption.</summary>
        string PrettyPrint(TSet set, CharSetSolver solver);

        /// <summary>Gets a full set (one that contains all values).</summary>
        TSet Full { get; }

        /// <summary>Gets an empty set (one that contains no values).</summary>
        TSet Empty { get; }

        /// <summary>Intersects two sets to produce a new one that contains only the elements that's in both (conjunction).</summary>
        TSet And(TSet set1, TSet set2);

        /// <summary>Unions two sets to produce a new one that contains elements that are in either or both (disjunction).</summary>
        TSet Or(TSet set1, TSet set2);

        /// <summary>Unions all of the sets in <paramref name="sets"/> to produce a new one that contains elements that are in any of the sets (disjunction).</summary>
        TSet Or(ReadOnlySpan<TSet> sets);

        /// <summary>Negates the set, producing a new one that contains the elements and only the elements not in the original.</summary>
        TSet Not(TSet set);

        /// <summary>Gets whether the set contains no elements.</summary>
        bool IsEmpty(TSet set);

        /// <summary>Gets whether the set contains every element.</summary>
        bool IsFull(TSet set);

        /// <summary>
        /// Given an array of constraints {c_1, c_2, ..., c_n} where n&gt;=0.
        /// Enumerate all satisfiable Boolean combinations Tuple({b_1, b_2, ..., b_n}, c)
        /// where c is satisfisable and equivalent to c'_1 &amp; c'_2 &amp; ... &amp; c'_n,
        /// where c'_i = c_i if b_i = true and c'_i is Not(c_i) otherwise.
        /// If n=0 return Tuple({},True)
        /// </summary>
        /// <param name="constraints">constraints</param>
        /// <returns>constraints that are satisfiable</returns>
        List<TSet> GenerateMinterms(HashSet<TSet> constraints);
    }
}
