using System;
using System.Collections.Generic;
using System.Linq;

namespace Flowstate.Notifications.Tests.References
{
    public class Result
    {
        internal static readonly IReadOnlyList<ErrorDetail> EmptyDetails = Array.Empty<ErrorDetail>();

        private bool _succeeded;
        private IReadOnlyList<ErrorDetail> _details;

        public bool Succeeded => _succeeded;
        public IReadOnlyList<ErrorDetail> Details => _details ?? EmptyDetails;

        public static Result Success() => new Result { _succeeded = true };

        public static Result Failure(IReadOnlyList<ErrorDetail> details = null) =>
            details?.Any(x => x.Equals(default)) ?? false
                ? throw new ArgumentException(ResultsErrorMessages.DetailsContainsUninitializedItems, nameof(details))
                : new Result { _succeeded = false, _details = details ?? EmptyDetails };


        public static Result Failure(params string[] details) =>
            Failure(details?.Select(x => new ErrorDetail(x)).ToArray());


        public void Deconstruct(out bool succeeded, out IReadOnlyList<ErrorDetail> details)
        {
            succeeded = Succeeded;
            details = Details;
        }

        public Result<TTarget> CastFailure<TTarget>() =>
            _succeeded
                ? throw new Exception(ResultsErrorMessages.CannotCastSucceededResultAsFailure)
                : Result<TTarget>.Failure(Details);


        public static implicit operator bool(Result @this) => @this.Succeeded;
    }
}