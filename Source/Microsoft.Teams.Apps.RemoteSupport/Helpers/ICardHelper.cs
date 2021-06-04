// <copyright file="ICardHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AdaptiveCards;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Bot.Schema;
    using Microsoft.Bot.Schema.Teams;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Models;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Providers;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Interface that handles the card configuration.
    /// </summary>
    public interface ICardHelper
    {
        /// <summary>
        /// Update request card in end user conversation.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="endUserUpdateCard"> End user request details card which is to be updated in end user conversation.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        Task<bool> UpdateRequestCardForEndUserAsync(ITurnContext turnContext, IMessageActivity endUserUpdateCard);

        /// <summary>
        /// Get task module response.
        /// </summary>
        /// <param name="applicationBasePath">Represents the Application base Uri.</param>
        /// <param name="customAPIAuthenticationToken">JWT token.</param>
        /// <param name="telemetryInstrumentationKey">The Application Insights telemetry client instrumentation key.</param>
        /// <param name="activityId">Task module activity Id.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <returns>Returns task module response.</returns>
        TaskModuleResponse GetTaskModuleResponse(string applicationBasePath, string customAPIAuthenticationToken, string telemetryInstrumentationKey, string activityId, IStringLocalizer<Strings> localizer);

        /// <summary>
        /// Gets edit ticket details adaptive card.
        /// </summary>
        /// <param name="cardConfigurationStorageProvider">Card configuration.</param>
        /// <param name="ticketDetail">Details of the ticket to be edited.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="existingTicketDetail">Existing ticket details.</param>
        /// <returns>Returns edit ticket adaptive card.</returns>
        TaskModuleResponse GetEditTicketAdaptiveCard(ICardConfigurationStorageProvider cardConfigurationStorageProvider, TicketDetail ticketDetail, IStringLocalizer<Strings> localizer, TicketDetail existingTicketDetail = null);

        /// <summary>
        /// Gets error message details adaptive card.
        /// </summary>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <returns>Returns edit ticket adaptive card.</returns>
        TaskModuleResponse GetClosedErrorAdaptiveCard(IStringLocalizer<Strings> localizer);

        /// <summary>
        /// Send card to SME channel and storage conversation details in storage.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="ticketDetail">Ticket details entered by user.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <param name="ticketDetailStorageProvider">Provider to store ticket details to Azure Table Storage.</param>
        /// <param name="applicationBasePath">Represents the Application base Uri.</param>
        /// <param name="cardElementMapping">Represents Adaptive card item element {Id, display name} mapping.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="teamId">Represents unique id of a Team.</param>
        /// <param name="microsoftAppCredentials">Microsoft Application credentials for Bot/ME.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Returns message in a conversation.</returns>
        Task<ConversationResourceResponse> SendRequestCardToSMEChannelAsync(
            ITurnContext<IMessageActivity> turnContext,
            TicketDetail ticketDetail,
            ILogger logger,
            ITicketDetailStorageProvider ticketDetailStorageProvider,
            string applicationBasePath,
            Dictionary<string, string> cardElementMapping,
            IStringLocalizer<Strings> localizer,
            string teamId,
            MicrosoftAppCredentials microsoftAppCredentials,
            CancellationToken cancellationToken);

        /// <summary>
        /// Send the given attachment to the specified team.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="cardToSend">The card to send.</param>
        /// <param name="teamId">Team id to which the message is being sent.</param>
        /// <param name="microsoftAppCredentials">Microsoft Application credentials for Bot/ME.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns><see cref="Task"/>That resolves to a <see cref="ConversationResourceResponse"/>Send a attachment.</returns>
        Task<ConversationResourceResponse> SendCardToTeamAsync(
            ITurnContext turnContext,
            Attachment cardToSend,
            string teamId,
            MicrosoftAppCredentials microsoftAppCredentials,
            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the email id's of the SME uses who are available for oncallSupport.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="onCallSupportDetailSearchService">Provider to search on call support details in Azure Table Storage.</param>
        /// <param name="teamId">Team id to which the message is being sent.</param>
        /// <param name="memoryCache">MemoryCache instance for caching oncallexpert details.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <returns>string with appended email id's.</returns>
        Task<string> GetOnCallSMEUserListAsync(ITurnContext<IInvokeActivity> turnContext, IOnCallSupportDetailSearchService onCallSupportDetailSearchService, string teamId, IMemoryCache memoryCache, ILogger<RemoteSupportActivityHandler> logger);

        /// <summary>
        /// Method updates experts card in team after modifying on call experts list.
        /// </summary>
        /// <param name="turnContext">Provides context for a turn of a bot.</param>
        /// <param name="onCallExpertsDetail">Details of on call support experts updated.</param>
        /// <param name="onCallSupportDetailSearchService">Provider to search on call support details in Azure Table Storage.</param>
        /// <param name="onCallSupportDetailStorageProvider"> Provider for fetching and storing information about on call support in storage table.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <returns>A task that sends notification in newly created channel and mention its members.</returns>
        Task UpdateManageExpertsCardInTeamAsync(ITurnContext<IInvokeActivity> turnContext, OnCallExpertsDetail onCallExpertsDetail, IOnCallSupportDetailSearchService onCallSupportDetailSearchService, IOnCallSupportDetailStorageProvider onCallSupportDetailStorageProvider, IStringLocalizer<Strings> localizer);

        /// <summary>
        /// Method to update the SME Card and gives corresponding notification.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="ticketDetail"> Ticket details entered by user.</param>
        /// <param name="messageActivity">Message activity of bot.</param>
        /// <param name="applicationBasePath"> Represents the Application base Uri.</param>
        /// <param name="cardElementMapping">Represents Adaptive card item element {Id, display name} mapping.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="logger">Telemetry logger.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>task that updates card.</returns>
        Task UpdateSMECardAsync(
            ITurnContext turnContext,
            TicketDetail ticketDetail,
            IMessageActivity messageActivity,
            string applicationBasePath,
            Dictionary<string, string> cardElementMapping,
            IStringLocalizer<Strings> localizer,
            ILogger logger,
            CancellationToken cancellationToken);

        /// <summary>
        /// Remove mapping elements from ticket additional details and validate input values of type 'DateTime'.
        /// </summary>
        /// <param name="additionalDetails">Ticket addition details.</param>
        /// <param name="timeSpan">>Local time stamp.</param>
        /// <returns>Adaptive card item element json string.</returns>
        string ValidateAdditionalTicketDetails(string additionalDetails, TimeSpan timeSpan);

        /// <summary>
        /// Converts json property to adaptive card element.
        /// </summary>
        /// <param name="elements">Adaptive item element Json object.</param>
        /// <returns>Returns adaptive card item element.</returns>
        List<AdaptiveElement> ConvertToAdaptiveCardItemElement(List<JObject> elements);

        /// <summary>
        /// Convert json template to Adaptive card.
        /// </summary>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="cardTemplate">Adaptive card template.</param>
        /// <param name="showDateValidation">true if need to show validation message else false.</param>
        /// <param name="ticketDetails">Ticket details key value pair.</param>
        /// <returns>Adaptive card item element json string.</returns>
        List<AdaptiveElement> ConvertToAdaptiveCard(IStringLocalizer<Strings> localizer, string cardTemplate, bool showDateValidation, Dictionary<string, string> ticketDetails = null);

        /// <summary>
        /// Check and convert to DateTime adaptive text if input string is a valid date time.
        /// </summary>
        /// <param name="inputText">Input date time string.</param>
        /// <returns>Adaptive card supported date time format else return sting as-is.</returns>
        string AdaptiveTextParseWithDateTime(string inputText);

        /// <summary>
        /// Get values from dictionary.
        /// </summary>
        /// <param name="ticketDetails">Ticket additional details.</param>
        /// <param name="key">Dictionary key.</param>
        /// <returns>Dictionary value.</returns>
        string TryParseTicketDetailsKeyValuePair(Dictionary<string, string> ticketDetails, string key);

        /// <summary>
        /// Remove item from dictionary.
        /// </summary>
        /// <param name="ticketDetails">Ticket details key value pair.</param>
        /// <param name="key">Dictionary key.</param>
        /// <returns>boolean value.</returns>
        bool RemoveMappingElement(Dictionary<string, string> ticketDetails, string key);

        /// <summary>
        /// Get adaptive card column set.
        /// </summary>
        /// <param name="title">Column title.</param>
        /// <param name="value">Column value.</param>
        /// <returns>AdaptiveColumnSet.</returns>
        AdaptiveColumnSet GetAdaptiveCardColumnSet(string title, string value);
    }
}
