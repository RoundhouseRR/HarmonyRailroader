using Railroader.Modding;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HarmonyRailroader
{
    public class Loader : IModEntryPoint
    {
        private const string HarmonyName = "0Harmony";

        private Assembly _harmony;

        public void OnModLoad(IModContext modContext)
        {
            string harmonyPath = Path.Combine(modContext.ModDirectory, HarmonyName + ".dll");

            Assembly alreadyLoaded = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == HarmonyName);
            if (alreadyLoaded != null)
            {
                if (string.Equals(Path.GetFullPath(alreadyLoaded.Location), Path.GetFullPath(harmonyPath), StringComparison.OrdinalIgnoreCase))
                {
                    // A mod reload keeps the assembly loaded, so this is our own copy.
                    _harmony = alreadyLoaded;
                    AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
                    return;
                }

                modContext.Log.Warning($"Harmony {alreadyLoaded.GetName().Version} was already loaded from {alreadyLoaded.Location}, so this copy was not used.");
                _harmony = alreadyLoaded;
            }
            else
            {
                _harmony = Assembly.LoadFrom(harmonyPath);
                modContext.Log.Info($"Loaded Harmony {_harmony.GetName().Version} from {_harmony.Location}");
            }

            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        }

        public void OnModUnload()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= OnAssemblyResolve;
        }

        // Dependent mods don't ship 0Harmony.dll, so point their references at the shared copy.
        private Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return new AssemblyName(args.Name).Name == HarmonyName ? _harmony : null;
        }
    }
}
