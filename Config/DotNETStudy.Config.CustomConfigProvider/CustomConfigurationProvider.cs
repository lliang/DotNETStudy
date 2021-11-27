using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DotNETStudy.Config.CustomConfigProvider
{
    internal class CustomConfigurationProvider : FileConfigurationProvider
    {
        public CustomConfigurationProvider(FileConfigurationSource source) : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            var nodeConnStrs = xmlDoc.SelectNodes("/configuration/connectionStrings/add");
            if (nodeConnStrs == null)
            {
                return;
            }
            foreach (var nodeConnStr in nodeConnStrs.Cast<XmlNode>() ?? new XmlNode[] { })
            {
                string name = nodeConnStr.Attributes["name"]?.Value ?? string.Empty;
                string connectionString = nodeConnStr.Attributes["connectionString"]?.Value ?? string.Empty;

                data[$"{name}:connectionString"] = connectionString;

                var providerNameProp = nodeConnStr.Attributes["providerName"];
                if (providerNameProp != null)
                {
                    string providerName = providerNameProp.Value;
                    data[$"{name}:providerName"] = providerName;
                }
            }

            var nodeAppSettings = xmlDoc.SelectNodes("/configuration/appSettings/add");
            if (nodeAppSettings == null)
            {
                return;
            }
            foreach (var nodeAppSetting in nodeAppSettings.Cast<XmlNode>() ?? new XmlNode[] { })
            {
                string key = nodeAppSetting.Attributes["key"]?.Value ?? string.Empty;
                key = key.Replace('.', ':');
                string value = nodeAppSetting.Attributes["value"]?.Value ?? string.Empty;

                data[key] = value;
            }

            Data = data;
        }
    }
}
