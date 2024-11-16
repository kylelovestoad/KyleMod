using System;

namespace Celeste.Mod.KyleMod;

public class KyleModModule : EverestModule {
    public static KyleModModule Instance { get; private set; }

    public override Type SettingsType => typeof(KyleModModuleSettings);
    public static KyleModModuleSettings Settings => (KyleModModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(KyleModModuleSession);
    public static KyleModModuleSession Session => (KyleModModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(KyleModModuleSaveData);
    public static KyleModModuleSaveData SaveData => (KyleModModuleSaveData) Instance._SaveData;

    public KyleModModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(KyleModModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(KyleModModule), LogLevel.Info);
#endif
    }

    public override void Load() {
        // TODO: apply any hooks that should always be active
    }

    public override void Unload() {
        // TODO: unapply any hooks applied in Load()
    }
}