using EverybodyCodes.Infrastructure.Domain;
using EverybodyCodes.Infrastructure.Extension.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EverybodyCodes.Infrastructure.Extension
{
    public class FileDataContext : IDataContext
    {
        private string _filePath { get; set; }

        private ICollection<Camera> _cameras { get; set; }

        public IEnumerable<Camera> Cameras => _cameras;

        public FileDataContext(RepositoryConfig repositoryConfig)
        {
            if (string.IsNullOrEmpty(repositoryConfig.CameraFilePath))
            {
                throw new ArgumentNullException("CameraFilePath");
            }

            this._filePath = repositoryConfig.CameraFilePath;

            InitContext();
        }

        private void InitContext()
        {
            _cameras = new List<Camera>();

            if (File.Exists(_filePath))
            {
                using (var stream = new StreamReader(_filePath))
                {
                    var line = string.Empty;
                    var linesCount = 0;
                    while ((line = stream.ReadLine()) != null)
                    {
                        if (linesCount != 0)
                        {
                            var model = MapLineToCamera(line);
                            if (model != null)
                            {
                                _cameras.Add(model);
                            }
                        }
                        linesCount++;
                    }
                }
            }
        }


        private Camera MapLineToCamera(string line)
        {
            try
            {
                // Camera;Latitude;Longitude
                var split = line.Split(";");
                if (split.Length == 3)
                {
                    var id = GetCameraId(split[0]);
                    if (!id.HasValue)
                    {
                        return null;
                    }

                    decimal latitude = decimal.Parse(split[1], CultureInfo.InvariantCulture);
                    decimal longitude = decimal.Parse(split[2], CultureInfo.InvariantCulture);

                    return new Camera()
                    {
                        Id = id.Value,
                        Name = split[0],
                        Latitude = latitude,
                        Longitude = longitude,

                    };
                }
            }
            catch (Exception)
            {

                return null;
            }
            return null;
        }

        private long? GetCameraId(string text)
        {
            try
            {
                long id;
                // text sample: UTR-CM-501 Neude rijbaan voor Postkantoor
                var nameSplit = text.Split(" ");
                if (nameSplit.Length > 0)
                {
                    // if last part of the first field doesnt have a long will be ignored. Sample: UTR-CM-501
                    if (!long.TryParse(nameSplit[0].Split("-")[2], out id))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

                return id;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
