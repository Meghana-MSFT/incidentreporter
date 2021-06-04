// <copyright file="IActivityHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Helpers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Bot.Schema;
    using Microsoft.Bot.Schema.Teams;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Providers;

    /// <summary>
    /// Interface that handles Bot activity helper methods.
    /// </summary>
    public interface IActivityHelper
    {
        /// <summary>
        /// Handle message activity in channel.
        /// </summary>
        /// <param name="message">A message in a conversation.</param>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="onCallSupportDetailSearchService">Provider to search on call support details in Azure Table Storage.</param>
        /// <param name="ticketDetailStorageProvider">Provider to store ticket details to Azure Table Storage.</param>
        /// <param name="cardConfigurationStorageProvider">Provider to search card configuration details in Azure Table Storage.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <param name="appBaseUrl">Represents the Application base Uri.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        Task OnMessageActivityInChannelAsync(
            IMessageActivity message,
            ITurnContext<IMessageActivity> turnContext,
            IOnCallSupportDetailSearchService onCallSupportDetailSearchService,
            ITicketDetailStorageProvider ticketDetailStorageProvider,
            ICardConfigurationStorageProvider cardConfigurationStorageProvider,
            ILogger logger,
#pragma warning disable CA1054 // Uri parameters should not be strings
            string appBaseUrl,
#pragma warning restore CA1054 // Uri parameters should not be strings
            IStringLocalizer<Strings> localizer,
            CancellationToken cancellationToken);

        /// <summary>
        /// Handle adaptive card submit in channel.
        /// Updates the ticket status based on the user submission.
        /// </summary>
        /// <param name="message">A message in a conversation.</param>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="ticketDetailStorageProvider">Provider to store ticket details to Azure Table Storage.</param>
        /// <param name="cardConfigurationStorageProvider">Provider to search card configuration details in Azure Table Storage.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <param name="appBaseUrl">Represents the Application base Uri.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        Task OnAdaptiveCardSubmitInChannelAsync(
            IMessageActivity message,
            ITurnContext<IMessageActivity> turnContext,
            ITicketDetailStorageProvider ticketDetailStorageProvider,
            ICardConfigurationStorageProvider cardConfigurationStorageProvider,
            ILogger logger,
#pragma warning disable CA1054 // Uri parameters should not be strings
            string appBaseUrl,
#pragma warning restore CA1054 // Uri parameters should not be strings
            IStringLocalizer<Strings> localizer,
            CancellationToken cancellationToken);

        /// <summary>
        /// Handle when a message is addressed to the bot in personal scope.
        /// </summary>
        /// <param name="message">Message activity of bot.</param>
        /// <param name="turnContext">The turn context.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <param name="cardConfigurationStorageProvider">Provider to search card configuration details in Azure Table Storage.</param>
        /// <param name="ticketGenerateStorageProvider">Provider to get ticket id to Azure Table Storage.</param>
        /// <param name="ticketDetailStorageProvider">Provider to store ticket details to Azure Table Storage.</param>
        /// <param name="microsoftAppCredentials">Microsoft Application credentials for Bot/ME.</param>
        /// <param name="appBaseUrl">Represents the Application base Uri.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns> A task that represents the work queued to execute for user message activity to bot.</returns>
        Task OnMessageActivityInPersonalChatAsync(
            IMessageActivity message,
            ITurnContext<IMessageActivity> turnContext,
            ILogger logger,
            ICardConfigurationStorageProvider cardConfigurationStorageProvider,
            ITicketIdGeneratorStorageProvider ticketGenerateStorageProvider,
            ITicketDetailStorageProvider ticketDetailStorageProvider,
            MicrosoftAppCredentials microsoftAppCredentials,
#pragma warning disable CA1054 // Uri parameters should not be strings
            string appBaseUrl,
#pragma warning restore CA1054 // Uri parameters should not be strings
            IStringLocalizer<Strings> localizer,
            CancellationToken cancellationToken);

        /// <summary>
        /// Method Handle adaptive card submit in 1:1 chat and Send new ticket details to SME team.
        /// </summary>
        /// <param name="message">Message activity of bot.</param>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="ticketGenerateStorageProvider">Provider to get ticket id to Azure Table Storage.</param>
        /// <param name="ticketDetailStorageProvider">Provider to store ticket details to Azure Table Storage.</param>
        /// <param name="cardConfigurationStorageProvider">Provider to search card configuration details in Azure Table Storage.</param>
        /// <param name="microsoftAppCredentials">Microsoft Application credentials for Bot/ME.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <param name="appBaseUrl">Represents the Application base Uri.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>A task that handles submit action in 1:1 chat.</returns>
        Task OnAdaptiveCardSubmitInPersonalChatAsync(
            IMessageActivity message,
            ITurnContext<IMessageActivity> turnContext,
            ITicketIdGeneratorStorageProvider ticketGenerateStorageProvider,
            ITicketDetailStorageProvider ticketDetailStorageProvider,
            ICardConfigurationStorageProvider cardConfigurationStorageProvider,
            MicrosoftAppCredentials microsoftAppCredentials,
            ILogger logger,
#pragma warning disable CA1054 // Uri parameters should not be strings
            string appBaseUrl,
#pragma warning restore CA1054 // Uri parameters should not be strings
            IStringLocalizer<Strings> localizer,
            CancellationToken cancellationToken);

        /// <summary>
        /// Handle members added conversationUpdate event in team.
        /// </summary>
        /// <param name="membersAdded">Channel account information needed to route a message.</param>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="microsoftAppCredentials">Microsoft Application credentials for Bot/ME.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <param name="appBaseUrl">Represents the Application base Uri.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        Task OnMembersAddedToTeamAsync(
           IList<ChannelAccount> membersAdded,
           ITurnContext<IConversationUpdateActivity> turnContext,
           MicrosoftAppCredentials microsoftAppCredentials,
           ILogger logger,
#pragma warning disable CA1054 // Uri parameters should not be strings
           string appBaseUrl,
#pragma warning restore CA1054 // Uri parameters should not be strings
           IStringLocalizer<Strings> localizer,
           CancellationToken cancellationToken);

        /// <summary>
        /// Handle 1:1 chat with members who started chat for the first time.
        /// </summary>
        /// <param name="membersAdded">Channel account information needed to route a message.</param>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <param name="appBaseUrl">Represents the Application base Uri.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        Task OnMembersAddedToPersonalChatAsync(
            IList<ChannelAccount> membersAdded,
            ITurnContext<IConversationUpdateActivity> turnContext,
            ILogger logger,
#pragma warning disable CA1054 // Uri parameters should not be strings
            string appBaseUrl,
#pragma warning restore CA1054 // Uri parameters should not be strings
            IStringLocalizer<Strings> localizer);

        /// <summary>
        /// Method mentions user in respective channel of which they are part after modifying experts list.
        /// </summary>
        /// <param name="onCallExpertsObjectIds">Collection of on call expert objectIds.</param>
        /// <param name="turnContext">Provides context for a turn of a bot.</param>
        /// <param name="logger">Sends logs to the Application Insights service.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="memoryCache">MemoryCache instance for caching oncallexpert details</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A task that sends notification in newly created channel and mention its members.</returns>
        Task<Activity> SendMentionActivityAsync(
            List<string> onCallExpertsObjectIds,
            ITurnContext<IInvokeActivity> turnContext,
            ILogger logger,
            IStringLocalizer<Strings> localizer,
            IMemoryCache memoryCache,
            CancellationToken cancellationToken);

        /// <summary>
        /// Get the account details of the user in a 1:1 chat with the bot.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        Task<TeamsChannelAccount> GetUserDetailsInPersonalChatAsync(
          ITurnContext<IMessageActivity> turnContext,
          CancellationToken cancellationToken);

        /// <summary>
        /// Verify if the tenant Id in the message is the same tenant Id used when application was configured.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="tenantId">Represents unique id of a Tenant.</param>
        /// <returns>True if context is from expected tenant else false.</returns>
        bool IsActivityFromExpectedTenant(ITurnContext turnContext, string tenantId);
    }
}
