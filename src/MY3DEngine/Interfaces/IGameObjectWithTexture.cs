﻿// <copyright file="IGameObjectWithTexture.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MY3DEngine.GraphicObjects;

namespace MY3DEngine.Interfaces
{
    public interface IGameObjectWithTexture
    {
        /// <summary>
        /// Gets or sets file name of the texture to use
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Gets or sets file path of the texture to use
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the building blocks of the object
        /// </summary>
        TextureVertex[] TextureVerticies { get; set; }
    }
}