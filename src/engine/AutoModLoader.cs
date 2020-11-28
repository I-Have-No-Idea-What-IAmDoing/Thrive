using System.Collections.Generic;
using Godot;

/// <summary>
///   Autoloads Mods from the Settings and handles the mods that failed to load
///   so that we can show a popup for the player
/// </summary>
public class AutoModLoader : Node
{
    private List<ModInfo> failedToLoadMods;

    private AutoModLoader()
    {
        ModLoader loader = new ModLoader();
        loader.LoadAutoLoadedModsList();

        var autoLoadedModList = ModLoader.AutoLoadedMods.ToArray();
        failedToLoadMods = loader.LoadModFromList(autoLoadedModList, false, false, false);
    }

    public void OpenModErrorPopup(AcceptDialog errorBox)
    {
        errorBox.DialogText += "\n\n";

        if (failedToLoadMods.Count != 0)
        {
            errorBox.DialogText += GetFailedMods();
            errorBox.PopupCenteredMinsize();
        }
    }

    public string GetFailedMods()
    {
        if (failedToLoadMods.Count != 0)
        {
            string text = string.Empty;
            foreach (ModInfo currentModInfo in failedToLoadMods)
            {
                text += currentModInfo.Name + "\n";
            }

            return text;
        }

        return string.Empty;
    }
}
