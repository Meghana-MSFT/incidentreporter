// <copyright file="IAdaptiveElementHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Helpers
{
    using AdaptiveCards;

    /// <summary>
    /// Helper to convert JSON property into Adaptive card element
    /// </summary>
    public interface IAdaptiveElementHelper
    {
        /// <summary>
        /// Converts JSON property to adaptive card TextBlock element.
        /// </summary>
        /// <param name="cardElementTemplate">TextBlock item element json property.</param>
        /// <returns>Returns adaptive card TextBlock item element.</returns>
        AdaptiveTextBlock ConvertToAdaptiveTextBlock(string cardElementTemplate);

        /// <summary>
        /// Converts JSON property to adaptive card TextInput element.
        /// </summary>
        /// <param name="cardElementTemplate">TextInput item element json property.</param>
        /// <returns>Returns adaptive card TextInput item element.</returns>
        AdaptiveTextInput ConvertToAdaptiveTextInput(string cardElementTemplate);

        /// <summary>
        /// Converts JSON property to adaptive card DateInput element.
        /// </summary>
        /// <param name="cardElementTemplate">DateInput item element json property.</param>
        /// <returns>Returns adaptive card DateInput item element.</returns>
        AdaptiveDateInput ConvertToAdaptiveDateInput(string cardElementTemplate);

        /// <summary>
        /// Converts JSON property to adaptive card ChoiceSetInput element.
        /// </summary>
        /// <param name="cardElementTemplate">ChoiceSetInput item element json property.</param>
        /// <returns>Returns adaptive card ChoiceSetInput item element.</returns>
        AdaptiveChoiceSetInput ConvertToAdaptiveChoiceSetInput(string cardElementTemplate);
    }
}
