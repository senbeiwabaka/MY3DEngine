﻿using Newtonsoft.Json;
using NLog;
using System;

namespace MY3DEngine.Utilities
{
    public static class Deserialize
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static T DeserializeStringAsT<T>(string content) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(DeserializeStringAsT));
            }

            return new T();
        }

        public static T DeserializeFileAsT<T>(string path) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(FileIO.GetFileContent($"{path}"));
            }
            catch (Exception e)
            {
                logger.Error(e, nameof(DeserializeFileAsT));
            }

            return new T();
        }
    }
}