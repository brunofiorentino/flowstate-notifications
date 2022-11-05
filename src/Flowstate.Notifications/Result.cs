using System;
using System.Collections.Generic;
using System.Linq;

namespace Flowstate.Notifications
{
    public struct Result
    {
        internal static readonly IReadOnlyList<ErrorDetail> EmptyDetails = Array.Empty<ErrorDetail>();

        private bool _succeeded;
        private IReadOnlyList<ErrorDetail> _details;

        public bool Succeeded => _succeeded;
        public IReadOnlyList<ErrorDetail> Details => _details ?? EmptyDetails;

        public static Result Success() => new Result { _succeeded = true };

        public static Result Failure(IReadOnlyList<ErrorDetail> details = null) =>
            (details?.Any(x => x.Equals(default)) ?? false)
                ? throw new ArgumentException(DetailsContainsUninitializedItems, nameof(details))
                : new Result { _succeeded = false, _details = details ?? EmptyDetails };


        public static Result Failure(params string[] details) =>
            Failure(details?.Select(x => new ErrorDetail(x)).ToArray());


        public void Deconstruct(out bool succeeded, out IReadOnlyList<ErrorDetail> details)
        {
            succeeded = Succeeded;
            details = Details;
        }

        public static implicit operator bool(Result @this) => @this.Succeeded;

        internal static readonly string DetailsContainsUninitializedItems = "'details' contains uninitilized items.";
    }
}