using System;
using CustomExtensionMethods;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;

namespace EloSystem.ResourceManagement
{
    internal class ResourceHandler
    {
        private const string RESFILE_EXTENSION_NAME = ".res";

        internal int UnsavedImages
        {
            get
            {
                return this.newResources.Count();
            }
        }
        private bool resourcesRosterHasBeenCreated
        {
            get
            {
                return this.resourceRoster != null;
            }
        }
        private Dictionary<int, EloImage> newResources;
        private HashSet<string> resourcesRemoved;
        private HashSet<string> resourceRoster;
        private string resourcePath;

        internal ResourceHandler(string filePath)
        {
            this.newResources = new Dictionary<int, EloImage>();
            this.resourcePath = filePath + ResourceHandler.RESFILE_EXTENSION_NAME;
            this.resourcesRemoved = new HashSet<string>();
        }

        internal EloImage GetImage(int imageID)
        {
            EloImage resource;

            if (this.newResources.TryGetValue(imageID, out resource)) { return new EloImage(new Bitmap(resource.Image)); }
            else
            {
                if (!this.resourcesRosterHasBeenCreated) { this.CreateResourceRoster(); }

                if (this.resourceRoster.Contains(imageID.ToString())) { return new EloImage(this.LoadResource(imageID)); }
                else { return new EloImage(); }
            }
        }

        internal void AddImage(Image image, int imageID)
        {
            this.newResources.Add(imageID, new EloImage(image));
        }

        internal void RemoveImage(int imageID)
        {
            string imageIDToRemove = imageID.ToString();

            if (!this.resourcesRemoved.Contains(imageIDToRemove)) { this.resourcesRemoved.Add(imageIDToRemove); }

        }

        internal void ReleaseResources()
        {
            foreach (KeyValuePair<int, EloImage> kvp in this.newResources.ToList()) { if (kvp.Value.Image != null) { kvp.Value.Image.Dispose(); } }

            this.newResources = new Dictionary<int, EloImage>();
        }

        internal void SaveResourceChanges(string filePath)
        {
            if (this.resourcePath == filePath + ResourceHandler.RESFILE_EXTENSION_NAME && this.newResources.IsEmpty() && this.resourcesRemoved.IsEmpty()) { return; }

            ResourceReader reader = null;
            string tempFilePath = string.Empty;

            // check if a resource file exists already
            if (File.Exists(this.resourcePath))
            {
                tempFilePath = string.Format("{0}\\temp{1}", Path.GetDirectoryName(this.resourcePath), Path.GetFileName(this.resourcePath));

                File.Copy(this.resourcePath, tempFilePath);

                reader = new ResourceReader(tempFilePath);
            }

            this.resourcePath = filePath + ResourceHandler.RESFILE_EXTENSION_NAME;

            using (var writer = new ResourceWriter(this.resourcePath))
            {
                // transfer resources from existing resource file
                if (reader != null)
                {
                    var eResReader = reader.GetEnumerator();

                    while (eResReader.MoveNext())
                    {
                        if (!this.resourcesRemoved.Contains(eResReader.Key as string))
                        {
                            writer.AddResource(eResReader.Key as string, eResReader.Value);

                            if (eResReader.Value is IDisposable) { (eResReader.Value as IDisposable).Dispose(); }
                        }
                    }

                    reader.Dispose();
                }


                if (tempFilePath != string.Empty) { File.Delete(tempFilePath); }

                // now, add new resources to the file
                var eNewResources = this.newResources.GetEnumerator();

                while (eNewResources.MoveNext())
                {
                    if (eNewResources.Current.Value.Image != null && !this.resourcesRemoved.Contains(eNewResources.Current.Key.ToString()))
                    {
                        writer.AddResource(eNewResources.Current.Key.ToString(), eNewResources.Current.Value.Image);
                    }
                }

            }


            this.ReleaseResources();

            this.resourceRoster = null;
            this.resourcesRemoved = new HashSet<string>();
        }

        private Image LoadResource(int imageID)
        {
            Image image = null;

            if (File.Exists(this.resourcePath))
            {
                using (var reader = new ResourceReader(this.resourcePath))
                {
                    if (reader != null)
                    {
                        var eResReader = reader.GetEnumerator();

                        bool resourceLocated = false;

                        string resName = imageID.ToString();

                        while (eResReader.MoveNext() && !resourceLocated)
                        {
                            if (eResReader.Key as string == resName)
                            {
                                image = eResReader.Value as Bitmap;
                                resourceLocated = true;
                            }

                        }
                    }

                    reader.Close();
                }
            }

            return image;
        }

        private void CreateResourceRoster()
        {
            this.resourceRoster = new HashSet<string>();

            if (File.Exists(this.resourcePath))
            {
                using (var reader = new ResourceReader(this.resourcePath))
                {
                    if (reader != null)
                    {
                        var eResReader = reader.GetEnumerator();

                        while (eResReader.MoveNext()) { this.resourceRoster.Add(eResReader.Key as string); }
                    }
                }
            }
        }
    }
}
