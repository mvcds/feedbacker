﻿using System;

namespace MVCDS.Feedbacker.Library
{
    /// <summary>
    /// It may triggers failure for a feedback
    /// </summary>
    public class Observation : Result
    {
        internal Observation(string message, bool isFailure)
            : this(message, () => isFailure)
        {
        }

        internal Observation(string message, Func<bool> isFailure)
        {
            MessageValidator.Assert(message);

            this.message = message.Trim();
            callback = isFailure;
        }

        /// <summary>
        /// The observation itself
        /// </summary>
        readonly private string message;
        /// <summary>
        /// The information itself
        /// </summary>
        public override string Message
        {
            get
            {
                return message;
            }
        }

        Func<bool> callback;
        /// <summary>
        /// As an observation, it may be contextual if it will trigger the feedback's failure or not
        /// </summary>
        public override bool TriggersFailure
        {
            get
            {
                return callback();
            }
        }
    }

    public class Observation<T> : Observation, IValue<T>
    {
        internal Observation(string message, bool isFailure, T value)
            : base(message, isFailure)
        {
            Value = value;
        }

        internal Observation(string message, Func<bool> isFailure, T value)
            : base(message, isFailure)
        {
            Value = value;
        }

        /// <summary>
        /// Information about the observation
        /// </summary>
        public T Value { get; private set; }
    }
}
