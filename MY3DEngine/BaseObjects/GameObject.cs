﻿using Newtonsoft.Json;
using SharpDX.Direct3D11;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MY3DEngine.BaseObjects
{
    public abstract class GameObject : IGameObject, IDisposable, INotifyPropertyChanged
    {
        private string name;

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsCube { get; set; } = false;

        public bool IsPrimitive { get; set; } = false;

        public bool IsSelected { get; set; } = false;

        public bool IsTriangle { get; set; } = false;

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;

                this.NotifyPropertyChanged(nameof(this.Name));
            }
        }

        //[XmlIgnore]
        //public MeshClass MeshObject { get; protected set; }

        [JsonIgnore]
        public PixelShader PixelShader { get; set; }

        [JsonIgnore]
        public VertexShader VertextShader { get; set; }

        [JsonIgnore]
        protected virtual SharpDX.Direct3D11.Buffer buffer { get; set; }

        [JsonIgnore]
        protected virtual InputLayout inputLayout { get; set; }

        #region Constructors

        /// <summary>
        /// blank constructor
        /// </summary>
        public GameObject() { }

        /// <summary>
        /// Constructor for building a .x object
        /// </summary>
        /// <param name="fileName">The file name of the object</param>
        /// <param name="path">The path of the object</param>
        public GameObject(string fileName = "", string path = "")
        {
            //MeshObject = new MeshClass(path, fileName);
            FileName = fileName;
            FilePath = path;
        }

        public GameObject(string type = "Cube")
        {
            //if (type.ToLower().Equals("triangle"))
            //{
            //    MeshObject = new MeshClass(MeshType.Triangle);
            //}
            //else
            //{
            //    MeshObject = new MeshClass(MeshType.Cube);
            //}

            Name = type;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Constructors

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Load the content of the object
        /// </summary>
        public virtual void LoadContent() { }

        /// <summary>
        /// Render the objects content to the screen
        /// </summary>
        public virtual void Render() { }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name}";

        /// <summary>
        /// Disposes of the object's mesh
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.buffer?.Dispose();
                this.inputLayout?.Dispose();
                this.VertextShader?.Dispose();
                this.PixelShader?.Dispose();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}