// <copyright file="AdaptiveElementHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdaptiveCards;
    using Microsoft.Teams.Apps.RemoteSupport.Cards;
    using Microsoft.Teams.Apps.RemoteSupport.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Helper class to convert JSON property into Adaptive card element
    /// </summary>
    public class AdaptiveElementHelper : IAdaptiveElementHelper
    {
        /// <summary>
        /// Helper that handles the card configuration.
        /// </summary>
        private readonly ICardHelper cardHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveElementHelper"/> class.
        /// </summary>
        /// <param name="cardHelper">Microsoft Application credentials for Bot/ME.</param>
        public AdaptiveElementHelper(ICardHelper cardHelper)
        {
            this.cardHelper = cardHelper;
        }

        /// <summary>
        /// Converts JSON property to adaptive card TextBlock element.
        /// </summary>
        /// <param name="cardElementTemplate">TextBlock item element json property.</param>
        /// <returns>Returns adaptive card TextBlock item element.</returns>
        public AdaptiveTextBlock ConvertToAdaptiveTextBlock(string cardElementTemplate)
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(cardElementTemplate);
            bool isVisible = true;
            if (!string.IsNullOrEmpty(this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "isVisible")))
            {
                bool status = bool.TryParse(this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "isVisible"), out isVisible);
            }

            string color = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "color");
            AdaptiveTextColor textColor;
            if (this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "id") == CardConstants.DateValidationMessageId)
            {
                textColor = AdaptiveTextColor.Attention;
            }
            else
            {
                textColor = string.IsNullOrEmpty(color) ? AdaptiveTextColor.Default : (AdaptiveTextColor)Enum.Parse(typeof(AdaptiveTextColor), color);
            }

            return new AdaptiveTextBlock()
            {
                Id = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "id"),
                Text = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "text"),
                IsVisible = isVisible,
                Color = textColor,
            };
        }

        /// <summary>
        /// Converts JSON property to adaptive card TextInput element.
        /// </summary>
        /// <param name="cardElementTemplate">TextInput item element json property.</param>
        /// <returns>Returns adaptive card TextInput item element.</returns>
        public AdaptiveTextInput ConvertToAdaptiveTextInput(string cardElementTemplate)
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(cardElementTemplate);
            int maxLength = 500;

            return new AdaptiveTextInput()
            {
                Id = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "id"),
                Placeholder = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "placeholder"),
                Value = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "value"),
                MaxLength = maxLength,
            };
        }

        /// <summary>
        /// Converts JSON property to adaptive card DateInput element.
        /// </summary>
        /// <param name="cardElementTemplate">DateInput item element json property.</param>
        /// <returns>Returns adaptive card DateInput item element.</returns>
        public AdaptiveDateInput ConvertToAdaptiveDateInput(string cardElementTemplate)
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(cardElementTemplate);

            return new AdaptiveDateInput()
            {
                Id = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "id"),
                Placeholder = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "placeholder"),
                Value = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "value"),
                Max = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "max"),
                Min = this.cardHelper.TryParseTicketDetailsKeyValuePair(result, "min"),
            };
        }

        /// <summary>
        /// Converts JSON property to adaptive card ChoiceSetInput element.
        /// </summary>
        /// <param name="cardElementTemplate">ChoiceSetInput item element json property.</param>
        /// <returns>Returns adaptive card ChoiceSetInput item element.</returns>
        public AdaptiveChoiceSetInput ConvertToAdaptiveChoiceSetInput(string cardElementTemplate)
        {
            var adpativeChoiceSetCard = JsonConvert.DeserializeObject<InputChoiceSet>(cardElementTemplate);
            List<AdaptiveChoice> choices = adpativeChoiceSetCard.Choices
                .Select(choice => new AdaptiveChoice()
                {
                    Title = choice.Title,
                    Value = choice.Value,
                })
                .ToList();

            return new AdaptiveChoiceSetInput()
            {
                IsMultiSelect = adpativeChoiceSetCard.IsMultiSelect,
                Choices = choices,
                Id = adpativeChoiceSetCard.Id,
                Style = adpativeChoiceSetCard.Style,
                Value = adpativeChoiceSetCard.Value,
            };
        }
    }
}