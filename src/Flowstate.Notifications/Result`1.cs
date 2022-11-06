﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Flowstate.Notifications
{
    public struct Result<T>
    {
        internal static readonly IReadOnlyList<ErrorDetail> EmptyDetails = Array.Empty<ErrorDetail>();

        private bool _succeeded;
        private T _value;
        private IReadOnlyList<ErrorDetail> _details;

        public bool Succeeded => _succeeded;
        public T Value => _value;
        public IReadOnlyList<ErrorDetail> Details => _details ?? EmptyDetails;

        public static Result<T> Success(T value) => new Result<T> { _succeeded = true, _value = value };

        public static Result<T> Failure(IReadOnlyList<ErrorDetail> details = null) =>
            (details?.Any(x => x.Equals(default)) ?? false)
                ? throw new ArgumentException(ResultsErrorMessages.DetailsContainsUninitializedItems, nameof(details))
                : new Result<T> { _succeeded = false, _details = details ?? EmptyDetails };


        public static Result<T> Failure(params string[] details) =>
            Failure(details?.Select(x => new ErrorDetail(x)).ToArray());


        public void Deconstruct(out bool succeeded, out T value, out IReadOnlyList<ErrorDetail> details)
        {
            succeeded = Succeeded;
            value = Value;
            details = Details;
        }

        public Result<TTarget> CastFailure<TTarget>() =>
            _succeeded
                ? throw new Exception(ResultsErrorMessages.CannotCastSucceededResultAsFailure)
                : Result<TTarget>.Failure(Details);


        public Result CastFailure() =>
            _succeeded
                ? throw new Exception(ResultsErrorMessages.CannotCastSucceededResultAsFailure)
                : Result.Failure(Details);


        public static implicit operator bool(Result<T> @this) => @this.Succeeded;
    }
}