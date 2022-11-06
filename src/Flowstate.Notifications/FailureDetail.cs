using System;

namespace Flowstate.Notifications
{
    public struct FailureDetail : IEquatable<FailureDetail>
    {
        private readonly string _description;
        private readonly string _tags;
        private readonly int _hashCode;

        public FailureDetail(string description, string tags = null)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(DescriptionCannotBeEmptyOrWhiteSpace, nameof(description));

            _description = description;
            _tags = tags;
            _hashCode = (_description, _tags).GetHashCode();
        }

        public string Description => _description;
        public string Tags => _tags ?? string.Empty;

        public override int GetHashCode() => _hashCode;

        public bool Equals(FailureDetail other) =>
            string.Compare(_description, other._description, StringComparison.Ordinal) == 0
            && string.Compare(_tags, other._tags, StringComparison.Ordinal) == 0;


        public override bool Equals(object obj) =>
            obj is FailureDetail other && Equals(other);

        public static implicit operator FailureDetail(string @this) => new FailureDetail(@this);
        public static implicit operator FailureDetail((string, string) @this) => new FailureDetail(@this.Item1, @this.Item2);

        internal static readonly string DescriptionCannotBeEmptyOrWhiteSpace =
            $"'description' cannot be empty or white space.";
    }
}