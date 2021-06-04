// <copyright file="ISmeTicketCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Cards
{
    using System.Collections.Generic;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Localization;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Models;

    /// <summary>
    /// Represents an SME ticket used for both in place card update activity within SME channel
    /// when changing the ticket status and notification card when bot posts user question to SME channel.
    /// </summary>
    public interface ISmeTicketCard
    {
        /// <summary>
        /// Returns an attachment based on the state and information of the ticket.
        /// </summary>
        /// <param name="cardElementMapping">Represents Adaptive card item element {Id, display name} mapping.</param>
        /// <param name="ticketDetail"> ticket values entered by user.</param>
        /// <param name="applicationBasePath">Represents the Application base URI.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <returns>Returns the attachment that will be sent in a message.</returns>
        Attachment GetTicketDetailsForSMEChatCard(Dictionary<string, string> cardElementMapping, TicketDetail ticketDetail, string applicationBasePath, IStringLocalizer<Strings> localizer);
    }
}
