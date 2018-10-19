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
        private Dictionary<int, Image> newResources;
        private HashSet<string> resourcesRemoved;
        private HashSet<string> resourceRoster;
        private string resourcePath;

        internal ResourceHandler(string filePath)
        {
            this.newResources = new Dictionary<int, Image>();
            this.resourcePath = filePath + ResourceHandler.RESFILE_EXTENSION_NAME;
            this.resourcesRemoved = new HashSet<string>();
        }

        internal Image GetImage(int imageID)
        {
            Image resource;

            if (this.newResources.TryGetValue(imageID, out resource)) { return resource; }
            else
            {
                if (!this.resourcesRosterHasBeenCreated) { this.CreateResourceRoster(); }

                if (this.resourceRoster.Contains(imageID.ToString())) { return this.LoadResource(imageID); }
                else { return null; }
            }
        }

        internal void AddImage(Image image, int imageID)
        {
            this.newResources.Add(imageID, image);
        }

        internal void RemoveImage(int imageID)
        {
            string imageIDToRemove = imageID.ToString();

            if (!this.resourcesRemoved.Contains(imageIDToRemove)) { this.resourcesRemoved.Add(imageIDToRemove); }

        }

        internal void ReleaseResources()
        {
            foreach (KeyValuePair<int, Image> kvp in this.newResources.ToList()) { if (kvp.Value != null) { kvp.Value.Dispose(); } }

            this.newResources = new Dictionary<int, Image>();
        }

        internal void SaveResourceChanges()
        {
            if (this.newResources.IsEmpty()) { return; }

            ResourceReader reader = null;
            string tempFilePath = string.Empty;

            // check if a resource file exists already
            if (File.Exists(this.resourcePath))
            {
                tempFilePath = string.Format("{0}\\temp{1}", Path.GetDirectoryName(this.resourcePath), Path.GetFileName(this.resourcePath));

                File.Move(this.resourcePath, tempFilePath);

                reader = new ResourceReader(tempFilePath);
            }


            using (var writer = new ResourceWriter(this.resourcePath))
            {
                // transfer resources from existing resource file
                if (reader != null)
                {
                    var eResReader = reader.GetEnumerator();

                    while (eResReader.MoveNext()) { if (!this.resourcesRemoved.Contains(eResReader.Key as string)) { writer.AddResource(eResReader.Key as string, eResReader.Value); } }

                    reader.Close();
                    reader.Dispose();
                }
                

                if (tempFilePath != string.Empty) { File.Delete(tempFilePath); }

                // now, add new resources to the file
                var eNewResources = this.newResources.GetEnumerator();

                while (eNewResources.MoveNext())
                {
                    if (eNewResources.Current.Value != null && !this.resourcesRemoved.Contains(eNewResources.Current.Key.ToString()))
                    {
                        writer.AddResource(eNewResources.Current.Key.ToString(), eNewResources.Current.Value);
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
