using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace XpertGroup.Dominio.Util
{
    /// <summary>
    /// Manejador de la escritura de logs de la aplicacion.
    /// </summary>
    public class Trazabilidad
    {
        #region Atributos
        private static readonly Lazy<Trazabilidad> lazy = new Lazy<Trazabilidad>(() => new Trazabilidad());
        private readonly ILog _log;
        #endregion
        #region Propiedades
        public static Trazabilidad Instancia
        {
            get
            {
                return lazy.Value;
            }
        }
        public ILog LogArchivoPlano
        {
            get
            {
                return _log;
            }
        }
        #endregion
        #region constructor
        private Trazabilidad()
        {
            if (_log != null) return;

            var assembly = Assembly.GetEntryAssembly();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var configFile = GetConfigFile();

            // Configure Log4Net
            XmlConfigurator.Configure(logRepository, configFile);
            _log = LogManager.GetLogger(assembly, assembly.ManifestModule.Name.Replace(".dll", "").Replace(".", " "));
        }

        private static FileInfo GetConfigFile()
        {
            FileInfo configFile = null;

            // Search config file
            var configFileNames = new[] { "Config/log4net.config", "log4net.config", "./log4net.config" };

            foreach (var configFileName in configFileNames)
            {
                configFile = new FileInfo(configFileName);

                if (configFile.Exists) break;
            }

            if (configFile == null || !configFile.Exists) throw new NullReferenceException("Log4net config no encontrado.");

            return configFile;
        }
        #endregion
    }
}
