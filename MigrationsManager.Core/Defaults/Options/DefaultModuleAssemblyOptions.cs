using Microsoft.Extensions.Configuration;
using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Defaults;
using MigrationsManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults.Options
{
    public class DefaultModuleAssemblyOptions : DictionaryBase<Assembly, IAssemblyOptions>, 
        IModuleAssemblyOptions, 
        IModuleAssemblyLocatorOptions
    {
        
        private void AddAssemblyByConfiguration(IModuleConfiguration moduleConfiguration)
        {
            if (moduleConfiguration.Enabled)
            {
                AddAssembly(moduleConfiguration.AssemblyName ?? moduleConfiguration.FileName, moduleConfiguration.Options);
            }
        }

        public IModuleAssemblyOptions AddAssembly(Assembly assembly, IAssemblyOptions assemblyOptions)
        {
            Add(assembly, assemblyOptions);
            return this;
        }

        public IModuleAssemblyOptions AddAssembly<T>(IAssemblyOptions assemblyOptions)
        {
            return AddAssembly(typeof(T).Assembly, assemblyOptions);
        }

        public IModuleAssemblyLocatorOptions AddAssembly(string assemblyNameorFilePath, IAssemblyOptions assemblyOptions)
        {
            AddAssembly(File.Exists(assemblyNameorFilePath) 
                ? Assembly.LoadFrom(assemblyNameorFilePath) 
                : Assembly.Load(assemblyNameorFilePath), assemblyOptions);
            return this;
        }

        public IModuleAssemblyLocatorOptions AddAssembly(string appSettingsSection, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "modules.json";
            }

            using (var streamReader = File.OpenText(fileName))
            {
                var json = streamReader.ReadToEnd();
                IModulesConfiguration configuration = JsonSerializer.Deserialize<DefaultModulesConfiguration>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var modules = configuration.Modules;
                
                modules.ForEach(AddAssemblyByConfiguration);
            }
           
            return this;
        }

        public IEnumerable<Assembly> GetAssemblies(Func<IAssemblyOptions, bool> filterOptions)
        {
            return dictionary
                .Where((k) => filterOptions(k.Value))
                .Select(a => a.Key);
        }
    }
}
