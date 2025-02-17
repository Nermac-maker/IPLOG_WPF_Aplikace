using Microsoft.Win32;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IPLOG
{
    
    public partial class MainWindow : Window
    {
                
            public MainWindow()
            {
                InitializeComponent();
            }

            private void LoadButton_Click(object sender, RoutedEventArgs e)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    var devices = DeviceParser.ParseDeviceFile(openFileDialog.FileName);

                    Inputs.Content = $"Inputs: {devices.Sum(d => d.Inputs.Count)}";
                    Outputs.Content = $"Outputs: {devices.Sum(d => d.Outputs.Count)}";
                    Interface.Content = $"If module: {devices.Sum(d => d.Interfaces.Count)}";

                

                    PopulateTreeView(devices);

                }
            }

            private void PopulateTreeView(List<Device> devices)
            {
                DeviceTreeView.Items.Clear();
                foreach (var device in devices)
                {
                    var rootItem = new TreeViewItem { Header = $"{device.BusId}/{device.Id}", Tag = device };

                    var inputsItem = new TreeViewItem { Header = "Inputs" };
                    foreach (var input in device.Inputs)
                    {
                        var inputItem = new TreeViewItem { Header = input.Name };
                        foreach (var metadata in input.Metadata)
                        {
                            inputItem.Items.Add(new TreeViewItem { Header = $"{metadata.Key}: {metadata.Value}" });
                        }
                        inputsItem.Items.Add(inputItem);
                    }

                    var outputsItem = new TreeViewItem { Header = "Outputs" };
                    foreach (var output in device.Outputs)
                    {
                        var outputItem = new TreeViewItem { Header = output.Name };
                        foreach (var metadata in output.Metadata)
                        {
                            outputItem.Items.Add(new TreeViewItem { Header = $"{metadata.Key}: {metadata.Value}" });
                        }
                        outputsItem.Items.Add(outputItem);
                    }

                    var interfacesItem = new TreeViewItem { Header = "Interfaces" };
                    foreach (var iface in device.Interfaces)
                    {
                        var ifaceItem = new TreeViewItem { Header = iface.Name };
                        foreach (var metadata in iface.Metadata)
                        {
                            ifaceItem.Items.Add(new TreeViewItem { Header = $"{metadata.Key}: {metadata.Value}" });
                        }
                        interfacesItem.Items.Add(ifaceItem);
                    }

                    rootItem.Items.Add(inputsItem);
                    rootItem.Items.Add(outputsItem);
                    rootItem.Items.Add(interfacesItem);

                    DeviceTreeView.Items.Add(rootItem);
                }
            }

            private void DeviceTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
            {
                if (DeviceTreeView.SelectedItem is TreeViewItem selectedItem && selectedItem.Tag is Device selectedDevice)
                {
                    Inputs.Content = $"Inputs: {selectedDevice.Inputs.Count}";
                    Outputs.Content = $"Outputs: {selectedDevice.Outputs.Count}";
                    Interface.Content = $"If module: {selectedDevice.Interfaces.Count}";
                }
            }
        }
    }
