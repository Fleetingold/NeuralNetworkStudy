using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace NeuralNetwork
{
    public class DataSetHelper
    {
        public static List<DataSet> ImportDatasets()
        {
            //var dialog = new OpenFileDialog
            //{
            //    Multiselect = false,
            //    Title = "Open Dataset File",
            //    Filter = "Text File|*.text;"
            //};

            //using (dialog)
            //{
            //    if (dialog.ShowDialog() != DialogResult.OK)
            //        return null;

            string sFileName = "";// dialog.FileName;
                using (var file = File.OpenText(sFileName))
                {
                    return JsonConvert.DeserializeObject<List<DataSet>>(file.ReadToEnd());
                }
            //}
        }
    }
}
