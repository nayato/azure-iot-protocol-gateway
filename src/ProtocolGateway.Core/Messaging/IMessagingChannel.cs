// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Devices.ProtocolGateway.Messaging
{
    using System;
    using System.Threading.Tasks;

    public interface IMessagingChannel
    {
        Task<MessageSendingOutcome> Handle(IMessage message);

        void Close(Exception cause);

        event EventHandler CapabilitiesChanged;
    }
}