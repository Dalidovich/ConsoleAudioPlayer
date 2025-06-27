using ConsoleAudioPlayer.Buffers;
using Microsoft.Win32;

namespace ConsoleAudioPlayer.PlayerSettings
{
    public static class ContextMenuRegisrated
    {
        private static readonly string _appName = "ConsoleAudioPlayer";
        private static readonly string _commandName = $"Play with {_appName}";
        private static readonly string _keyPathForEntity = $@"WMP11.AssocFile.MP3\shell\{_appName}";
        private static readonly string _keyPathForFolder = $@"Directory\shell\{_appName}";

        public static void Init()
        {
            if (ValueBufferTemplate.ApplyContextMenu)
            {
                AddContextMenuEntry();
                AddFolderContextMenuEntry();
            }
            else
            {
                RemoveAllContextMenu();
            }
        }

        public static void AddContextMenuEntry()
        {
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(_keyPathForEntity))
            {
                key.SetValue("", $"{_commandName}");
                var pathApp = Path.Combine(AppContext.BaseDirectory, $"{_appName}.exe");
                using (RegistryKey commandKey = key.CreateSubKey("command"))
                {
                    commandKey.SetValue("", $"{pathApp} \"%1\"");
                }
            }
        }

        public static void AddFolderContextMenuEntry()
        {
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(_keyPathForFolder))
            {
                key.SetValue("", $"{_commandName}");
                var pathApp = Path.Combine(AppContext.BaseDirectory, $"{_appName}.exe");
                using (RegistryKey commandKey = key.CreateSubKey("command"))
                {
                    commandKey.SetValue("", $"{pathApp} \"%1\"");
                }
            }
        }

        public static void RemoveContextMenuEntry(string keyPath)
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(keyPath, false);
                Registry.ClassesRoot.DeleteSubKeyTree($@"SystemFileAssociations\.mp3\shell\{_appName}", false);
            }
            catch (ArgumentException ex)
            {
                //The key does not exist - we do nothing
            }
        }

        public static void RemoveAllContextMenu()
        {
            RemoveContextMenuEntry(_keyPathForFolder);
            RemoveContextMenuEntry(_keyPathForEntity);
        }
    }
}
