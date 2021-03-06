﻿// <copyright file="GameEngineLoad.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.BuildTools
{
    using My3DEngine.Utilities.Interfaces;
    using MY3DEngine.BuildTools.Models;
    using NLog;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;

    public static class GameEngineLoad
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        // TODO: UPDATE
        public static bool LoadLevel(string path, List<object> gameObjects)
        {
            Logger.Info($"Starting {nameof(GameEngineLoad)}.{nameof(LoadLevel)}");

            try
            {
                var contentsofFile = System.IO.File.ReadAllText(path);
                var jsonDeserializedData = JsonSerializer.Deserialize<IEnumerable>(contentsofFile);

                foreach (var item in jsonDeserializedData)
                {
                    var jsonSettings = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };
                    var gameObject = JsonSerializer.Deserialize(item.ToString(), typeof(object), jsonSettings);

                    if (gameObject != null)
                    {
                        gameObjects.Add(gameObject);
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                Logger.Error(exception, $"{nameof(GameEngineLoad)}.{nameof(LoadLevel)}");
            }

            Logger.Info($"Finished {nameof(GameEngineLoad)}.{nameof(LoadLevel)}");

            return false;
        }

        /// <summary>
        /// Load a game project
        /// </summary>
        /// <param name="folderLocation">The location for the game files</param>
        /// <returns>The load project result. <see cref="ToolsetGameModel"/></returns>
        /// <remarks>TODO: FINISH</remarks>
        public static ToolsetGameModel LoadProject(string folderLocation, IFileService fileIo)
        {
            if (fileIo == null)
            {
                throw new ArgumentNullException(nameof(fileIo));
            }

            Logger.Info($"Starting {nameof(GameEngineLoad)}.{nameof(LoadProject)}");

            ToolsetGameModel model;

            // The folder path passed in is empty or is not a valid directory
            if (string.IsNullOrWhiteSpace(folderLocation) || !fileIo.DirectoryExists(folderLocation))
            {
                model = new ToolsetGameModel(false);
            }
            else
            {
                model = new ToolsetGameModel(true)
                {
                    FolderLocation = folderLocation,
                };

                var files = fileIo.GetFiles(folderLocation, Constants.MainFileName);

                if (files.Any(x => x.ToUpperInvariant().Contains(Constants.MainFileName.ToUpperInvariant())))
                {
                    var mainFile = files.Single(x => x.ToUpperInvariant().Contains(Constants.MainFileName.ToUpperInvariant()));
                    model.MainFileFolderLocation = mainFile.Substring(0, mainFile.IndexOf(Constants.MainFileName, StringComparison.InvariantCultureIgnoreCase));
                    model.MainFileName = Constants.MainFileName;
                }

                files = fileIo.GetFiles(folderLocation, "settings".ToUpperInvariant());

                if (files.Any(x => x.ToUpperInvariant().Contains("settings".ToUpperInvariant())))
                {
                    model.Settings = fileIo.GetFileContent(files.First(x => x.ToUpperInvariant().Contains("settings".ToUpperInvariant())));
                }

                if (string.IsNullOrWhiteSpace(model.Settings))
                {
                    files = fileIo.GetFiles(folderLocation, "DefaultSettings.ini");

                    if (files.Any(x => x.ToUpperInvariant().Contains("DefaultSettings.ini".ToUpperInvariant())))
                    {
                        model.Settings = fileIo.GetFileContent(files.Single(x => x.ToUpperInvariant().Contains("DefaultSettings.ini".ToUpperInvariant())));
                    }
                }
            }

            Logger.Info($"Finished {nameof(GameEngineLoad)}.{nameof(LoadProject)}");

            return model;
        }
    }
}
