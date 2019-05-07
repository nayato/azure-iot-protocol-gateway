// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Devices.ProtocolGateway.Messaging
{
    using System.Threading.Tasks;

    public class MessageFeedbackChannel: TaskCompletionSource<MessageSendingOutcome>
    {
        public void Abandon() => this.TrySetResult(MessageSendingOutcome.Abandonded);

        public void Complete() => this.TrySetResult(MessageSendingOutcome.Completed);

        public void Reject() => this.TrySetResult(MessageSendingOutcome.Rejected);
    }
}