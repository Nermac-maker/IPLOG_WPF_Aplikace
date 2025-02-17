using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;




namespace IPLOG
{
    public static class DeviceParser
    {
        public static List<Device> ParseDeviceFile(string filePath)
        {
            var devices = new List<Device>();
            Device currentDevice = null;
            string currentSection = null;

            foreach (var line in File.ReadLines(filePath))
            {
                if (line.StartsWith("./bus"))
                {
                    var parts = line.Split('/');
                    if (parts.Length == 3)
                    {
                        currentDevice = new Device { BusId = parts[1], Id = parts[2] };
                        devices.Add(currentDevice);
                    }
                    else if (parts.Length > 3)
                    {
                        currentSection = parts[3];
                        if (currentSection == "i" && parts.Length > 4)
                        {
                            currentDevice.Inputs.Add(new Input { Name = parts[4] });
                        }
                        else if (currentSection == "o" && parts.Length > 4)
                        {
                            currentDevice.Outputs.Add(new Output { Name = parts[4] });
                        }
                        else if (currentSection == "if" && parts.Length > 4)
                        {
                            currentDevice.Interfaces.Add(new InterfaceModule { Name = parts[4] });
                        }
                    }
                }
                else if (currentDevice != null && currentSection != null)
                {
                    var parts = line.Split('/');
                    if (currentSection == "i" && parts.Length > 2)
                    {
                        currentDevice.Inputs[^1].Metadata[parts[^1]] = parts[^2];
                    }
                    else if (currentSection == "o" && parts.Length > 2)
                    {
                        currentDevice.Outputs[^1].Metadata[parts[^1]] = parts[^2];
                    }
                    else if (currentSection == "if" && parts.Length > 2)
                    {
                        currentDevice.Interfaces[^1].Metadata[parts[^1]] = parts[^2];
                    }
                }
            }

            return devices;
        }
    }
}
