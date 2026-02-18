/*
***********************************************
*  Copyright (c), Skyline Communications NV.  *
***********************************************

Revision History:

DATE		VERSION		AUTHOR				COMMENTS

18/02/2026	1.0.0.1		MOD, JST, Skyline	Initial version
****************************************************************************
*/

namespace SLCASSetCell
{
	using System;

	using Skyline.DataMiner.Automation;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
    {
        /// <summary>
        /// The script entry point.
        /// </summary>
        /// <param name="engine">Link with SLAutomation process.</param>
        public void Run(IEngine engine)
        {
            try
            {
                RunSafe(engine);
            }
            catch (ScriptAbortException)
            {
                // Catch normal abort exceptions (engine.ExitFail or engine.ExitSuccess)
                throw; // Comment if it should be treated as a normal exit of the script.
            }
            catch (ScriptForceAbortException)
            {
                // Catch forced abort exceptions, caused via external maintenance messages.
                throw;
            }
            catch (ScriptTimeoutException)
            {
                // Catch timeout exceptions for when a script has been running for too long.
                throw;
            }
            catch (InteractiveUserDetachedException)
            {
                // Catch a user detaching from the interactive script by closing the window.
                // Only applicable for interactive scripts, can be removed for non-interactive scripts.
                throw;
            }
            catch (Exception e)
            {
                engine.ExitFail("Run|Something went wrong: " + e);
            }
        }

        private static void RunSafe(IEngine engine)
		{
			var elementIdentifier = engine.GetScriptParam(10).Value.Trim().TrimStart('[', '"').TrimEnd('"', ']');
			if (String.IsNullOrWhiteSpace(elementIdentifier))
			{
				engine.ExitFail($"Invalid Element Identifier: '{elementIdentifier}'");
				return;
			}

			var columnIdString = engine.GetScriptParam(11).Value.Trim().TrimStart('[', '"').TrimEnd('"', ']');
			if (String.IsNullOrWhiteSpace(columnIdString) || Int32.TryParse(columnIdString, out int columnId))
			{
				engine.ExitFail($"Invalid Column ID: '{columnIdString}'");
				return;
			}

			var primaryKey = engine.GetScriptParam(12).Value.Trim().TrimStart('[', '"').TrimEnd('"', ']');
			if (String.IsNullOrWhiteSpace(primaryKey))
			{
				engine.ExitFail($"Invalid Primary Key: '{primaryKey}'");
				return;
			}

			var value = engine.GetScriptParam(13).Value.Trim().TrimStart('[', '"').TrimEnd('"', ']');

			Element element;
			if (elementIdentifier.Contains("/"))
			{
				// If the identifier contains a '/', we assume it's in the format "DataMinerID/ElementID".
				element = engine.FindElementByKey(elementIdentifier);
			}
			else
			{
				// If the identifier does not contain a '/', we assume it's an element name and try to get the element directly.
				element = engine.FindElement(elementIdentifier);
			}

			if (element == null)
			{
				engine.ExitFail($"Element not found: '{elementIdentifier}'");
				return;
			}

			if (!element.IsActive)
			{
				engine.ExitFail($"Element is not active: '{elementIdentifier}'");
				return;
			}

			element.SetParameterByPrimaryKey(columnId, primaryKey, value);
		}
    }
}
