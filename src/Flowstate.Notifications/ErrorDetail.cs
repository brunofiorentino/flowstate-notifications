﻿using System;

namespace Flowstate.Notifications
{
    public struct ErrorDetail : IEquatable<ErrorDetail>
    {
        private readonly string _description;
        private readonly string _tag;
        private readonly int _hashCode;

        public ErrorDetail(string description, string tag = null)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(DescriptionCannotBeEmptyOrWhiteSpace, nameof(description));

            _description = description;
            _tag = tag;
            _hashCode = (_description, _tag).GetHashCode();
        }

        public string Description => _description;
        public string Tag => _tag ?? string.Empty;

        public override int GetHashCode() => _hashCode;

        public bool Equals(ErrorDetail other) =>
            string.Compare(_description, other._description, StringComparison.Ordinal) == 0
            && string.Compare(_tag, other._tag, StringComparison.Ordinal) == 0;


        public override bool Equals(object obj) =>
            obj is ErrorDetail other && Equals(other);


        internal static readonly string DescriptionCannotBeEmptyOrWhiteSpace =
            $"'description' cannot be empty or white space.";
    }
}